using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] string attackAnimBool;
    [SerializeField] string walkAnimBool;
    [SerializeField] string idleAnimBool;
    [SerializeField] string chaseAnimBool;


    [Header("Config")]
    [SerializeField] private string playerName;
    public NavMeshAgent agent;

    [SerializeField] float timeToNextPoint;
    [SerializeField] float timeSinceLastPoint;

    [SerializeField] EnemyHealth health;

    public Transform player;
    public float newSightRange;

    public LayerMask whatIsFloor;
    public LayerMask whatIsPlayer;

    //patrol
    [Header("Patrol")]
    public Vector3 walkPoint;
    bool walkPointIsSet;
    public float walkPointRange;

    [Header("Attack")]
    //Attack
    public float attackRate;
    bool attackedAlready;

    [Header("States")]
    // State handling
    public float sightRange;
    public float attackRange;
    public bool playerInSight, playerInAttackRange;

    [Header("Idling")]
    // Idle state
    public bool suspicious = false;
    [SerializeField] float suspicionTime;
    [SerializeField] float suspicionTimePassed;
    public bool waiting;

    private void Awake()
    {
        if (waiting)
        {
            suspicious = false;
        }
        player = GameObject.Find(playerName).transform;
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (waiting)
        {
            Idle();
        }

        else
        {
            if (health.alive)
            {
                //Check Sight and Atk range
                RangeChecks();

                if (!playerInSight && !playerInAttackRange)
                {
                    if (suspicious)
                    {
                        Idle();
                    }
                    else
                    {
                        Patrol();
                    }
                }
                if (playerInSight && !playerInAttackRange) Chasing();
                if (playerInSight && playerInAttackRange) Attack();

            }
        }
        if(!health.alive)
        {
            agent.SetDestination(transform.position);
        }
        
    }

    private void RangeChecks()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    }

    private void Idle()
    {
        RangeChecks();

        if (!suspicious)
        {
            walkPoint = transform.position;
            IdleBools();
        }

        if (suspicious)
        {
            IdleBools();
            waiting = false;
            walkPoint = transform.position;
            suspicionTimePassed += Time.deltaTime;
            if (suspicionTime <= suspicionTimePassed)
            {
                suspicious = false;
            }
        }

        if (playerInSight || playerInAttackRange)
        {
            waiting = false;
            suspicious = true;
        }
    }

    private void IdleBools()
    {
        animator.SetBool(idleAnimBool, true);
        animator.SetBool(attackAnimBool, false);
        animator.SetBool(walkAnimBool, false);
        animator.SetBool(chaseAnimBool, false);
    }

    private void Patrol()
    {
        timeSinceLastPoint += Time.deltaTime;
        animator.SetBool(walkAnimBool, true);
        animator.SetBool(attackAnimBool, false);
        animator.SetBool(idleAnimBool, false);
        animator.SetBool(chaseAnimBool, false);

        if (!walkPointIsSet || timeSinceLastPoint >= timeToNextPoint)
        {
            SearchForWalkPoint();
            timeSinceLastPoint = 0;
        }

        if (walkPointIsSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToPoint = transform.position - walkPoint;

        // reaching walkpoint

        if (distanceToPoint.magnitude < 1f)
        {
            walkPointIsSet = false;
        }

    }

    private void SearchForWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsFloor))
        {
            walkPointIsSet = true;
        }


    }

    private void Chasing()
    {
        suspicious = true;
        agent.SetDestination(player.position);
        animator.SetBool(walkAnimBool, false); 
        animator.SetBool(idleAnimBool, false);
        animator.SetBool(attackAnimBool, false);
        animator.SetBool(chaseAnimBool, true);
    }

    private void Attack()
    {
        //Stop enemy from moving while attacking and make sure is facing player
        agent.SetDestination(transform.position);

        animator.SetBool(idleAnimBool, false);
        animator.SetBool(walkAnimBool, false);
        animator.SetBool(attackAnimBool, true);
        animator.SetBool(chaseAnimBool, false);

        transform.LookAt(player);

        if(!attackedAlready)
        {
            // Insert animation invoke
            Debug.Log("Attack");
            attackedAlready = true;
            Invoke(nameof(ResetAttack), attackRate);
        }
    }

    private void ResetAttack()
    {
        attackedAlready = false;
    }

    public void LoseSuspicion()
    {
        suspicious = false;
        Idle();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }

    public void newTargetPosition()
    {
        sightRange = newSightRange;
        animator.SetBool(idleAnimBool, false);
        waiting = false;
        walkPointIsSet = true;
        walkPoint = player.transform.position;
    }
}
