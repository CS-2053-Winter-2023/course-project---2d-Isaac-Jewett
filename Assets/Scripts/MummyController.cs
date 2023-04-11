using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{
    public float MummySpeed;
    public float visionRange;
    public float chaseSpeed;
    public float fieldOfView;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isChasing;

    public Vector2[] patrolPoints;
    private int currentPatrolPointIndex = 0;

    private Transform MummySprite;

    void Start()
    {
        MummySprite = transform.Find("MummySprite");

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(transform.up, directionToPlayer);

        if (Vector2.Distance(transform.position, player.position) < visionRange && angleToPlayer < fieldOfView / 2 && !isChasing)
        {
            isChasing = true;
            animator.SetBool("isChasing", true);
        }

        if (isChasing)
        {
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            MummySprite.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (angle > 180 || angle < -180)
            {
                MummySprite.GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                MummySprite.GetComponent<SpriteRenderer>().flipY = false;
            }

            rb.velocity = directionToPlayer * chaseSpeed;
        }
        else
        {
            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPointIndex]) < 0.1f)
            {
                currentPatrolPointIndex++;
                if (currentPatrolPointIndex >= patrolPoints.Length)
                {
                    currentPatrolPointIndex = 0;
                }
            }

            Vector2 directionToPatrolPoint = (patrolPoints[currentPatrolPointIndex] - (Vector2)transform.position).normalized;
            rb.velocity = directionToPatrolPoint * MummySpeed;

            if (rb.velocity.x > 0)
            {
                MummySprite.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (rb.velocity.x < 0)
            {
                MummySprite.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (!isChasing)
        {
            animator.SetBool("isChasing", false);
        }

        if (angleToPlayer >= fieldOfView / 2)
        {
            isChasing = false;
            animator.SetBool("isChasing", false);
        }
    }
}