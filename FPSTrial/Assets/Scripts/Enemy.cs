/*using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;              //Get the NavMesh agent from Enemy

    public Transform player;                //Position of Player

    public LayerMask Ground;                //Get ground layer
    public LayerMask Player;                //Get Player layer

    public float health;                    //Health of Enemy
    public float startHealth;               //Start health to calculate stages

    public int stage;                       //Stages in fight according to health
    public float newSpeed;                  //Speed increase when in Stage 3 of the fight

    //Patroling
    public Vector3 walkPoint;               //To hold new walking point
    bool walkPointSet;                      //To check if there is a walking point
    public float walkPointRange;            //To hold the distance of current position to walking point position

    //Attacking
    public float timeBetweenAttacks;        //Time interval between attacks
    bool alreadyAttacked;                   //Check if has attacked
    public GameObject bullet;               //To hold bullets
    public Transform attackPoint;           //To hold instantiation point for bullets

    //States
    public float sightRange;                //Sight range
    public float attackRangeMelee;          //Melee attack range
    public float attackRangeRange;          //Range attack range
    public bool playerInSightRange;         //Check if Player is in sight
    public bool playerInAttackRangeMelee;   //Check if Player is in melee range
    public bool playerInAttackRangeRange;   //Check if Player is in range attack range

    public AudioSource Shotgunfire;
    public AudioClip Shotgun;
    public AudioSource paHurts;
    public AudioClip hurtSound;
    public AudioClip paDeath;

    //Sort of Like start
    private void Awake()
    {
        //player = GameObject.Find("Player").transform;   //Get Player
        agent = GetComponent<NavMeshAgent>();           //Get the NavMesh agent
        stage = 1;                                      //Start at stage 1 for fight
        startHealth = health;                           //Set startHealth with the starting health
        paHurts.clip = hurtSound;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);               //Chekcs if Player is in sight range
        playerInAttackRangeMelee = Physics.CheckSphere(transform.position, attackRangeMelee, Player);   //Checks if Player is in melee range
        playerInAttackRangeRange = Physics.CheckSphere(transform.position, attackRangeRange, Player);   //Checks if Player is in range attack range

        //Player nowhere in sight
        if (!playerInSightRange && !playerInAttackRangeMelee && !playerInAttackRangeRange)
        {
            Patroling();    //Wander around
        }

        //Player in sight but not in attacking range
        if (playerInSightRange && !playerInAttackRangeMelee && !playerInAttackRangeRange)
        {
            ChasePlayer();  //Chase down Player
        }

        //If in sight, in range attack range, and is able to attack
        if (playerInSightRange && !playerInAttackRangeMelee && playerInAttackRangeRange)// && !alreadyAttacked)
        {
            AttackPlayer(); //Attack Player
            ChasePlayer();
        }

        *//*//If in sight, in range attack range, but NOT able to attack
        if (playerInSightRange && !playerInAttackRangeMelee && playerInAttackRangeRange && alreadyAttacked)
        {
            ChasePlayer();  //Chase Player
        }*//*

        //In sight and in melee range
        if (playerInSightRange && playerInAttackRangeMelee)
        {
            AttackPlayer();
        }
    }

    //Method for wandering around
    private void Patroling()
    {
        if (!walkPointSet)                      //Check if there is a walkpoint set, else
        {
            SearchWalkPoint();                  //Make a walkpoint
        }

        if (walkPointSet)                       //If one was made
        {
            agent.SetDestination(walkPoint);    //Set destination to the walkpoint
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;   //Now walk to the walkpoint

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)     //If already at walkpoint
        {
            walkPointSet = false;                   //Set false so we can make a new one to walk to
        }
    }

    //Method to look for a new walk point
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //Make the Vector for it
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Check if walkpoint is valid, dont' want them walking where they shouldn't
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
        {
            walkPointSet = true;        //valid walk point
        }
    }

    //Method to chase the Player
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);  //Set destination to the Player, forget walking point
    }

    //Method to attack the player
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move....NOTWORKING-_-
        agent.SetDestination(transform.position);

        //We want it to face us when fighting
        //transform.LookAt(player);
        Vector3 look = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z);

        //Check if can attack
        if (!alreadyAttacked)
        {
            ///Attack code here*****************************************************************************************************************************************
            if (stage == 1)
            {
                if (playerInAttackRangeMelee)
                {
                    Debug.Log("Stage 1 --- Kick");
                }

                else
                {
                    //Shotgunfire.Play();
                    //Calculate direction from attackPoint to targetPoint
                    Vector3 directionOfBullet = player.position - attackPoint.position; //added to enemy script

                    Rigidbody rb = Instantiate(bullet, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                    
                    rb.transform.forward = directionOfBullet.normalized; //added to enemy script

                    rb.AddForce(directionOfBullet * 32f, ForceMode.Impulse);
                    rb.AddForce(transform.up * 8f, ForceMode.Impulse);
                   

                    *//*//Calculate direction from attackPoint to targetPoint
                    Vector3 directionOfBullet = targetPoint - attackPoint.position;

                    //Instantiate bullet/projectile
                    GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
                                                                                                               //Rotate bullet to shoot direction
                    currentBullet.transform.forward = directionOfBullet.normalized;

                    //Add forces to bullet
                    currentBullet.GetComponent<Rigidbody>().AddForce(directionOfBullet.normalized * shootForce, ForceMode.Impulse);
                    currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);*//*

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
            //Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //End of attack code*********************************************************************************************************************************************************

            alreadyAttacked = true;                          //Attacked already
            Invoke(nameof(ResetAttack), timeBetweenAttacks); //Start time interval between attacks
        }
    }

    //Method to reset ability to attack
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //Method to take damage
    public void TakeDamage(int damage)
    {
        health -= damage;   //decrement health by damage taken
        //paHurts.Play();
        //For stage 2
        if (health <= ((startHealth / 3) * 2))
        {
            stage = 2;
        }

        //For state 3
        if (health <= (startHealth / 3))
        {
            stage = 3;
            agent.speed = newSpeed;
        }

        //For dying
        if (health <= 0) 
        {
            paHurts.clip = paDeath;
        }
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    //DEAD
    private void DestroyEnemy()
    {
        //Destroy(gameObject);
        Debug.Log("Pa DIED!");
    }
}
*/

using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;              //Get the NavMesh agent from Enemy

    public Transform player;                //Position of Player
    public PlayerInput actualPlayer;        //To get script of player for FIXES!

    public LayerMask Ground;                //Get ground layer
    public LayerMask Player;                //Get Player layer

    public float health;                    //Health of Enemy
    public float startHealth;               //Start health to calculate stages
    public bool died = false;

    public int stage;                       //Stages in fight according to health
    public float newSpeed;                  //Speed increase when in Stage 3 of the fight

    public GameObject finishTree;           //The tree blocking the finish point

    //Patroling
    public Vector3 walkPoint;               //To hold new walking point
    bool walkPointSet;                      //To check if there is a walking point
    public float walkPointRange;            //To hold the distance of current position to walking point position

    //Attacking
    public float timeBetweenAttacks;        //Time interval between attacks
    bool alreadyAttacked;                   //Check if has attacked
    public GameObject bullet;               //To hold bullets
    public Transform attackPoint;           //To hold instantiation point for bullets
    //public GameObject axe;                  //To hold axe

    //States
    public float sightRange;                //Sight range
    public float attackRangeMelee;          //Melee attack range
    public float attackRangeRange;          //Range attack range
    public bool playerInSightRange;         //Check if Player is in sight
    public bool playerInAttackRangeMelee;   //Check if Player is in melee range
    public bool playerInAttackRangeRange;   //Check if Player is in range attack range

    //Sort of Like start
    private void Awake()
    {
        //player = GameObject.Find("Player").transform;   //Get Player
        agent = GetComponent<NavMeshAgent>();           //Get the NavMesh agent
        stage = 1;                                      //Start at stage 1 for fight
        startHealth = health;                           //Set startHealth with the starting health
    }

    private void Update()
    {
        if (died)
            return;
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);               //Chekcs if Player is in sight range
        playerInAttackRangeMelee = Physics.CheckSphere(transform.position, attackRangeMelee, Player);   //Checks if Player is in melee range
        playerInAttackRangeRange = Physics.CheckSphere(transform.position, attackRangeRange, Player);   //Checks if Player is in range attack range

        //Player nowhere in sight
        if (!playerInSightRange && !playerInAttackRangeMelee && !playerInAttackRangeRange)
        {
            Patroling();    //Wander around
        }

        //Player in sight but not in attacking range
        if (playerInSightRange && !playerInAttackRangeMelee && !playerInAttackRangeRange)
        {
            ChasePlayer();  //Chase down Player
        }

        //If in sight, in range attack range, and is able to attack
        if (playerInSightRange && !playerInAttackRangeMelee && playerInAttackRangeRange)// && !alreadyAttacked)
        {
            AttackPlayer(); //Attack Player
            //ChasePlayer();
        }

        /*//If in sight, in range attack range, but NOT able to attack
        if (playerInSightRange && !playerInAttackRangeMelee && playerInAttackRangeRange && alreadyAttacked)
        {
            ChasePlayer();  //Chase Player
        }*/

        //In sight and in melee range
        if (playerInSightRange && playerInAttackRangeMelee)
        {
            AttackPlayer();
        }
    }

    //Method for wandering around
    private void Patroling()
    {
        if (!walkPointSet)                      //Check if there is a walkpoint set, else
        {
            SearchWalkPoint();                  //Make a walkpoint
        }

        if (walkPointSet)                       //If one was made
        {
            agent.SetDestination(walkPoint);    //Set destination to the walkpoint
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;   //Now walk to the walkpoint

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)     //If already at walkpoint
        {
            walkPointSet = false;                   //Set false so we can make a new one to walk to
        }
    }

    //Method to look for a new walk point
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //Make the Vector for it
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Check if walkpoint is valid, dont' want them walking where they shouldn't
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
        {
            walkPointSet = true;        //valid walk point
        }
    }

    //Method to chase the Player
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);  //Set destination to the Player, forget walking point
    }

    //Method to attack the player
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move....NOTWORKING-_-
        agent.SetDestination(transform.position);

        //We want it to face us when fighting
        //transform.LookAt(player);
        Vector3 look = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z);

        //Check if can attack
        if (!alreadyAttacked)
        {
            ///Attack code here*****************************************************************************************************************************************
            if (stage == 1)
            {
                if (playerInAttackRangeMelee)
                {
                    Debug.Log("Stage 1 --- Kick");
                }

                else
                {
                    //Calculate direction from attackPoint to targetPoint
                    Vector3 directionOfBullet = player.position - attackPoint.position; //added to enemy script

                    Rigidbody rb = Instantiate(bullet, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

                    rb.transform.forward = directionOfBullet.normalized; //added to enemy script
                    //rb.transform.LookAt(player);

                    rb.AddForce(directionOfBullet * 15f, ForceMode.Impulse);
                    rb.AddForce(transform.up * 1f, ForceMode.Impulse);

                    Debug.Log(player.position);

                    //ONLY BECAUSE THERE IS A BUG WE CAN'T SEEM TO FIND, PRETTY SURE SOMETHING WRONG IN THE ENGINE, HAPPENED TWICE BEFORE, TO MANY OF 
                    //OTHER PEOPLE CHANGES TO GO BACK NOW T-T
                    actualPlayer.TakeDamage(10);

                    /*//Calculate direction from attackPoint to targetPoint
                    Vector3 directionOfBullet = targetPoint - attackPoint.position;

                    //Instantiate bullet/projectile
                    GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
                                                                                                               //Rotate bullet to shoot direction
                    currentBullet.transform.forward = directionOfBullet.normalized;

                    //Add forces to bullet
                    currentBullet.GetComponent<Rigidbody>().AddForce(directionOfBullet.normalized * shootForce, ForceMode.Impulse);
                    currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);*/

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
                    //Calculate direction from attackPoint to targetPoint
                    Vector3 directionOfBullet = player.position - attackPoint.position; //added to enemy script

                    Rigidbody rb = Instantiate(bullet, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

                    rb.transform.forward = directionOfBullet.normalized; //added to enemy script
                    //rb.transform.LookAt(player);

                    rb.AddForce(directionOfBullet * 15f, ForceMode.Impulse);
                    rb.AddForce(transform.up * 1f, ForceMode.Impulse);

                    Debug.Log(player.position);

                    //ONLY BECAUSE THERE IS A BUG WE CAN'T SEEM TO FIND, PRETTY SURE SOMETHING WRONG IN THE ENGINE, HAPPENED TWICE BEFORE, TO MANY OF 
                    //OTHER PEOPLE CHANGES TO GO BACK NOW T-T
                    actualPlayer.TakeDamage(10);
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
                    //Calculate direction from attackPoint to targetPoint
                    Vector3 directionOfBullet = player.position - attackPoint.position; //added to enemy script

                    Rigidbody rb = Instantiate(bullet, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

                    rb.transform.forward = directionOfBullet.normalized; //added to enemy script
                    //rb.transform.LookAt(player);

                    rb.AddForce(directionOfBullet * 15f, ForceMode.Impulse);
                    rb.AddForce(transform.up * 1f, ForceMode.Impulse);

                    Debug.Log(player.position);

                    //ONLY BECAUSE THERE IS A BUG WE CAN'T SEEM TO FIND, PRETTY SURE SOMETHING WRONG IN THE ENGINE, HAPPENED TWICE BEFORE, TO MANY OF 
                    //OTHER PEOPLE CHANGES TO GO BACK NOW T-T
                    actualPlayer.TakeDamage(10);

                }
                //Debug.Log("sTaGE 3!!");
            }
            //Debug.Log("IM HIT IM HIT!");
            //Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //End of attack code*********************************************************************************************************************************************************

            alreadyAttacked = true;                          //Attacked already
            Invoke(nameof(ResetAttack), timeBetweenAttacks); //Start time interval between attacks
        }
    }

    //Method to reset ability to attack
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //Method to take damage
    public void TakeDamage(int damage)
    {
        health -= damage;   //decrement health by damage taken

        //For stage 2
        if (health <= ((startHealth / 3) * 2))
        {
            stage = 2;
        }

        //For state 3
        if (health <= (startHealth / 3))
        {
            stage = 3;
            agent.speed = newSpeed;
        }

        //For dying
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    //DEAD
    private void DestroyEnemy()
    {
        //Destroy(gameObject);
        Destroy(finishTree);
        transform.Translate(new Vector3(0f, -10f, 0f));
        died = true;
        agent.isStopped = true;
        Debug.Log("Pa DIED!");
        Destroy(gameObject);
    }

    public void PlayerDied()
    {
        died = true;
        agent.isStopped = true;
    }
}
