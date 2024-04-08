using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // ScriptableObject
    [SerializeField] EnemyDataScriptableObject enemyData;

    // UI
    [SerializeField] EnemyUI enemyUI;

    // Testing Variables
    [SerializeField] bool lockEnemyForTesting = false;

    // General Variables
    private int health;

    // Cooldown Between Pathing
    [SerializeField] float cooldownDuration;
    [SerializeField] bool cooldownActive = false;
    [SerializeField] float cooldownTimer = 0f;

    private Transform player;

    [SerializeField] NavMeshAgent agent;

    //Patroling
    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;
    [SerializeField] float timeToReachTarget;
    float proximityThreshold = 1f;
    
    //Attacking
    private float timeBetweenAttacks;
    private bool alreadyAttacked;
    private GameObject projectile;

    //Investigating
    [SerializeField] Vector3 investigationPoint;
    bool investigationPointSet;

    //States
    private float sightRange, attackRange, alertRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange, alertSourceInRange;

    //UI
    private bool canChangeExclamation = true;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer, whatIsAlert;

    private void Start()
    {
        health = enemyData.health;
        cooldownDuration = enemyData.pathingCooldown;
        timeBetweenAttacks = enemyData.attackCooldown;
        sightRange = enemyData.sightRange;
        attackRange = enemyData.attackRange;
        alertRange = enemyData.alertRange;
        projectile = enemyData.projectile;
        player = PlayerData.instance.playerPos;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //check for sight, attack and investigate range
        alertSourceInRange = AlertPointInRange();
        playerInSightRange = LookForPlayer(sightRange);
        playerInAttackRange = LookForPlayer(attackRange);

        if (!lockEnemyForTesting)
        {
            if (!playerInSightRange && !playerInAttackRange && !alertSourceInRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
            if (!playerInAttackRange && !playerInSightRange && alertSourceInRange) Investigate();
        }
    }


    private void Patroling()
    {
        SetUI(0);

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
                cooldownActive = true;
                return;
            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
                cooldownActive = true;
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
    public void SetWalkPoint(Vector3 destination)
    {
        walkPoint = destination;
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
        SetUI(2);
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        SetUI(2);
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
    private void Investigate()
    {
        SetUI(1);
        GameObject currentAlertManager = SearchForAlertPointRecent();
        Vector3 alertSource = currentAlertManager.transform.position;
        agent.SetDestination(alertSource);
        transform.LookAt(alertSource);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        Vector3 distanceToWalkPoint = transform.position - new Vector3(alertSource.x, 0, alertSource.z);

        if (distanceToWalkPoint.magnitude < 1f)
        {
            Debug.Log("destination reached");
            AlertManager.instance.ForceReturnAlertSource(currentAlertManager);
            alertSource = currentAlertManager.transform.position;
        }
    }
    private GameObject SearchForAlertPointRecent()
    {
        GameObject closestAlertSource = null;
        float shortestTimeExisted = float.MaxValue;

        for (int i = 0; i < AlertManager.instance.activeAlertSourceList.Count; i++)
        {
            GameObject alertSource = AlertManager.instance.activeAlertSourceList[i];

            float distance = Vector3.Distance(transform.position, alertSource.transform.position);
            float timeExisted = alertSource.GetComponent<AlertSourceManager>().timeExisted;

            if (distance < alertRange)
            {
                if (timeExisted < shortestTimeExisted)
                {
                    shortestTimeExisted = timeExisted;
                    closestAlertSource = alertSource;
                }
            }
        }
        return closestAlertSource;
    }
    private GameObject SearchForAlertPointDistance()
    {
        GameObject closestAlertSource = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < AlertManager.instance.activeAlertSourceList.Count; i++)
        {
            GameObject alertSource = AlertManager.instance.activeAlertSourceList[i];

            float distance = Vector3.Distance(transform.position, alertSource.transform.position);

            if (distance < alertRange)
            {
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestAlertSource = alertSource;
                }
            }
        }
        return closestAlertSource;
    }



    private bool AlertPointInRange()
    {
        if(AlertManager.instance.activeAlertSourceList != null)
        {
            for (int i = 0; i < AlertManager.instance.activeAlertSourceList.Count; i++)
            {
                GameObject alertSource = AlertManager.instance.activeAlertSourceList[i];

                float distance = Vector3.Distance(transform.position, alertSource.transform.position);

                if (distance < alertRange)
                {
                    return true;
                }

            }
        }
        return false;
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

    void DrawRaycast(Vector3 direction, Color color)
    {
        Debug.DrawRay(transform.position, direction * sightRange, color);
    }
    bool LookForPlayer(float range)
    {
        Vector3 middleOfEnemy = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        Vector3 aboveEnemy = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

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
            if (Physics.Raycast(aboveEnemy, direction, range, whatIsPlayer))
            {
                return true;
            }
        }

        return false;
    }
    void SetUI(int markIndex)
    {
        if (canChangeExclamation)
        {
            StartCoroutine(ChangeUIDelay(markIndex));
        }
    }
    private IEnumerator ChangeUIDelay(int markIndex)
    {
        canChangeExclamation = false;
        yield return new WaitForSeconds(0.15f);
        switch (markIndex)
        {
            case 0:
                enemyUI.UIOff();
                break;
            case 1:
                enemyUI.SetQuestionMark();
                break;
            case 2:
                enemyUI.SetExclamationMark();
                break;
        }
        canChangeExclamation = true;
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 middleOfEnemy = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(middleOfEnemy, transform.forward * sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(middleOfEnemy, transform.forward * attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(middleOfEnemy, alertRange);

    }
}
