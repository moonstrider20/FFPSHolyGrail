using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask Ground, Player;

    public float health;
    public float startHealth;

    public int stage;

    public float newSpeed;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange;
    public float attackRangeMelee;
    public float attackRangeRange;
    public bool playerInSightRange;
    public bool playerInAttackRangeMelee;
    public bool playerInAttackRangeRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        stage = 1;
        startHealth = health;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRangeMelee = Physics.CheckSphere(transform.position, attackRangeMelee, Player);
        playerInAttackRangeRange = Physics.CheckSphere(transform.position, attackRangeRange, Player);

        if (!playerInSightRange && !playerInAttackRangeMelee && !playerInAttackRangeRange)
        {
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRangeMelee && !playerInAttackRangeRange)
        {
            ChasePlayer();
        }

        if (playerInSightRange && !playerInAttackRangeMelee && playerInAttackRangeRange)
        {
            AttackPlayer();
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRangeMelee)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move....NOTWORKING-_-
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            if (stage == 1)
            {
                if (playerInAttackRangeMelee)
                {
                    Debug.Log("Stage 1 --- Kick");
                }

                else
                {
                    Debug.Log("Stage 1 --- Gun");
                }
                //Debug.Log("Hit in stage 1");
            }

            else if (stage == 2)
            {
                if (playerInAttackRangeMelee)
                {
                    Debug.Log("STAGE 2 --- Axe");
                }

                else
                {
                    Debug.Log("STAGE 2 --- Gun");
                }
                //Debug.Log("HIT STAGE 2");
            }

            else
            {
                if (playerInAttackRangeMelee)
                {
                    Debug.Log("sTaGE 3 --- Axe");
                }

                else
                {
                    Debug.Log("sTaGE 3 --- Throwing Axe");
                }
                //Debug.Log("sTaGE 3!!");
            }
            //Debug.Log("IM HIT IM HIT!");
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

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

        if (health <= ((startHealth / 3) * 2))
        {
            stage = 2;
        }

        if (health <= (startHealth / 3))
        {
            stage = 3;
            agent.speed = newSpeed;
        }

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }*/
}
