using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Color testColor;

    [SerializeField] EnemyUI enemyUI;

    [SerializeField] bool lockEnemyForTesting = false;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform player;

    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] int health;

    //Patroling
    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;
    private float timeToReachTarget;
    float proximityThreshold = 1f;
    private bool cooldownActive = false;
    [SerializeField] float cooldownDuration = 2f;
    private float cooldownTimer = 0f;


    //Attacking
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;
    [SerializeField] GameObject projectile;

    //states
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;

    //UI
    private bool canChangeExclamation = true;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = LookForPlayer(sightRange);
        playerInAttackRange = LookForPlayer(attackRange);

        if (!lockEnemyForTesting)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
    }


    private void Patroling()
    {
        SetExclamationMark(false);

        if (!walkPointSet)
        {
            // Check if the search cooldown is active
            if (!cooldownActive)
            {
                SearchWalkPoint();
                timeToReachTarget = 0f;
            }
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            timeToReachTarget += Time.deltaTime;

            // Check if the enemy got stuck
            if (timeToReachTarget > 4f)
            {
                Debug.Log("Enemy got stuck");
                walkPointSet = false;
                cooldownActive = true; // Activate search cooldown
                return; // Enemy remains still during cooldown
            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
                cooldownActive = true; // Activate search cooldown
            }
        }

        // Check if the search cooldown is active
        if (cooldownActive)
        {
            // Wait for the search cooldown duration
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownDuration)
            {
                cooldownActive = false;
                cooldownTimer = 0f;
            }
        }
    }

    private void SearchWalkPoint()
    {
        // calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround) && IsNearNavMesh(walkPoint, 0.25f))
            walkPointSet = true;
    }
    public bool IsNearNavMesh(Vector3 position, float minDistanceToEdge)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, proximityThreshold, NavMesh.AllAreas))
        {
            if (NavMesh.FindClosestEdge(position, out hit, NavMesh.AllAreas))
            {
                return hit.distance >= minDistanceToEdge;
            }
        }
        return false;
    }

    private void ChasePlayer()
    {
        SetExclamationMark(true);
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        SetExclamationMark(true);
        //make sure enemy doesnt move
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (!alreadyAttacked)
        {
            // attack player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
            GameObject newProjectile = Instantiate(projectile, newPos, Quaternion.identity);

            newProjectile.transform.rotation = Quaternion.LookRotation(directionToPlayer);

            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            rb.AddForce(transform.up * 3f, ForceMode.Impulse);
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) DestroyEnemy();

    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 middleOfEnemy = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(middleOfEnemy, transform.forward * sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(middleOfEnemy, transform.forward * attackRange);

    }
    void DrawRaycast(Vector3 direction, Color color)
    {
        Debug.DrawRay(transform.position, direction * sightRange, color);
    }
    bool LookForPlayer(float range)
    {
        Vector3 middleOfEnemy = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);

        Vector3[] directions = {
        transform.forward,
        Quaternion.Euler(0, -45f, 0) * transform.forward,
        Quaternion.Euler(0, -37.5f, 0) * transform.forward,
        Quaternion.Euler(0, -30f, 0) * transform.forward,
        Quaternion.Euler(0, -22.5f, 0) * transform.forward,
        Quaternion.Euler(0, -15f, 0) * transform.forward,
        Quaternion.Euler(0, -7.5f, 0) * transform.forward,
        Quaternion.Euler(0, 45f, 0) * transform.forward,
        Quaternion.Euler(0, 37.5f, 0) * transform.forward,
        Quaternion.Euler(0, 30f, 0) * transform.forward,
        Quaternion.Euler(0, 22.5f, 0) * transform.forward,
        Quaternion.Euler(0, 15f, 0) * transform.forward,
        Quaternion.Euler(0, 7.5f, 0) * transform.forward
    };

        foreach (Vector3 direction in directions)
        {
            if (Physics.Raycast(middleOfEnemy, direction, range, whatIsPlayer))
            {
                return true;
            }
        }

        return false;
    }
    void SetExclamationMark(bool active)
    {
        if (canChangeExclamation)
        {
            StartCoroutine(ChangeExclamationDelay(active));
        }
    }
    private IEnumerator ChangeExclamationDelay(bool active)
    {
        canChangeExclamation = false;
        yield return new WaitForSeconds(0.25f);
        enemyUI.SetExclamationMark(active);
        canChangeExclamation = true;
    }
}
