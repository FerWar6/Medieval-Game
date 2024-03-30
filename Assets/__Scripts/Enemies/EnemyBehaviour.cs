using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Color testColor;
    [SerializeField] bool lockEnemyForTesting = false;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform player;

    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] int health;

    //Patroling
    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    //Attacking
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;
    [SerializeField] GameObject projectile;

    //states
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = LookForPlayer();
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!lockEnemyForTesting)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }

        if (playerInSightRange)
        {
            testColor = Color.red;
        }
        else
        {
            testColor = Color.green;
        }
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // walkpoint reachted
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        // calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //make sure enemy doesnt move
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (!alreadyAttacked)
        {
            // attack player
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Rigidbody rb = Instantiate(projectile, newPos, Quaternion.identity).GetComponent<Rigidbody>();

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
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.DrawRay(middleOfEnemy, transform.forward * sightRange);

    }
    void DrawRaycast(Vector3 direction, Color color)
    {
        Debug.DrawRay(transform.position, direction * sightRange, color);
    }

    bool LookForPlayer2()
    {
        Vector3 middleOfEnemy = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        Vector3 forwardDirection = transform.forward;

        // Cast a ray in the forward direction
        RaycastHit hitInfo;
        if (Physics.Raycast(middleOfEnemy, forwardDirection, out hitInfo, sightRange, whatIsPlayer))
        {
            Debug.Log("Raycast hit: " + hitInfo.collider.gameObject.name);
            Debug.DrawRay(middleOfEnemy, forwardDirection * sightRange, testColor);
            return true; // Hit something
        }

        // No hit
        Debug.DrawRay(middleOfEnemy, forwardDirection * sightRange, testColor);
        return false;
    }
    bool LookForPlayer()
    {
        Vector3 middleOfEnemy = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);

        Vector3[] directions = {
        transform.forward,
        Quaternion.Euler(0, -45f, 0) * transform.forward,
        Quaternion.Euler(0, -33.75f, 0) * transform.forward,
        Quaternion.Euler(0, -22.5f, 0) * transform.forward,
        Quaternion.Euler(0, -11.25f, 0) * transform.forward,
        Quaternion.Euler(0, 45f, 0) * transform.forward,
        Quaternion.Euler(0, 33.75f, 0) * transform.forward,
        Quaternion.Euler(0, 22.5f, 0) * transform.forward,
        Quaternion.Euler(0, 11.25f, 0) * transform.forward
    };

        foreach (Vector3 direction in directions)
        {
            if (Physics.Raycast(middleOfEnemy, direction, sightRange, whatIsPlayer))
            {
                return true;
            }
        }

        return false;
    }

}
