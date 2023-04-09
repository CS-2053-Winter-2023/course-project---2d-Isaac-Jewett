using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{

    public float MummySpeed = 2f;
    public float visionRange = 5f;
    public float chaseSpeed = 5f;
    public float fieldOfView = 45f;
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isChasing;
    public Vector2[] patrolPoints;
    private int currentPatrolPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame 
    void Update()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(transform.up, directionToPlayer);

        if (Vector2.Distance(transform.position, player.position) < visionRange && angleToPlayer < fieldOfView / 2 && !isChasing)   {
            isChasing = true;
            animator.SetBool("isChasing", true);
        }

        if (isChasing)
        {
            transform.up = directionToPlayer;
            rb.velocity = directionToPlayer * chaseSpeed;
        }   else    {
            Vector2 directionToPatrolPoint = (patrolPoints[currentPatrolPointIndex] - (Vector2)transform.position).normalized;
            transform.up = directionToPatrolPoint;
            rb.velocity = directionToPatrolPoint;
            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPointIndex]) < 0.1f) {
                currentPatrolPointIndex++;
                if (currentPatrolPointIndex >= patrolPoints.Length) {
                    currentPatrolPointIndex = 0;
                }
            }
        }

        if (!isChasing) {
            animator.SetBool("isChasing", false);
        }

        if (angleToPlayer >= fieldOfView / 2)   {
            isChasing = false;
            animator.SetBool("isChasing", false);
        }

    }
}
