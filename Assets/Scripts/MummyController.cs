using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{
    public float MummySpeed;
    public float visionRange;
    public float chaseSpeed;
    public float fieldOfView;

    public Transform frontDirection; // Child object for the front direction

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isChasing;

    public Vector2[] patrolPoints;
    private int currentPatrolPointIndex = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("StartPatrol");
    }

    void Update()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(frontDirection.up, directionToPlayer); // Use child object's forward direction
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Debug.DrawRay(transform.position, directionToPlayer * visionRange, Color.red);

        if (isChasing)
        {
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            if (angle > 180 || angle < -180)
            {
                transform.localScale = new Vector3(6, -6, 1); // Flip the object horizontally
            }
            else
            {
                transform.localScale = new Vector3(6, 6, 1); // Reset the object's scale
            }

            rb.velocity = directionToPlayer * chaseSpeed;
        }
        else
        {
            // Raycast to detect if there is a wall between the mummy and the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, visionRange, LayerMask.GetMask("Default"));

            if (hit.collider != null)
            {

                // If the raycast hits the player and the player is within field of view, start chasing
                if (hit.collider.CompareTag("Player") && angleToPlayer < fieldOfView / 2)
                {   
                    Debug.Log("Chasing player");
                    isChasing = true;
                    animator.SetTrigger("StartChase");
                }
            }

            if (!isChasing)
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
                    transform.localScale = new Vector3(6, 6, 1); // Reset the object's scale
                }
                else if (rb.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-6, 6, 1); // Flip the object horizontally
                }
            }
        }
    }
}