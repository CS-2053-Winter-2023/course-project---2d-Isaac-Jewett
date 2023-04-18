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
    // Flip the sprite based on the velocity
    if (rb.velocity.x > 0)
    {
        transform.localScale = new Vector3(6, 6, 1); // Reset the object's scale
    }
    else if (rb.velocity.x < 0)
    {
        transform.localScale = new Vector3(-6, 6, 1); // Flip the object horizontally
    }

    Vector2 directionToPlayer = (player.position - transform.position).normalized;
    float angleToPlayer = Vector2.Angle(frontDirection.up, directionToPlayer); // Use child object's forward direction
    float distanceToPlayer = Vector2.Distance(transform.position, player.position);

    // Draw a line between the mummy and player to check for walls
    Debug.DrawLine(transform.position, player.position, Color.blue);

    // Use LayerMask to only detect walls
    int layerMask = LayerMask.GetMask("Wall");

    // Check for walls between mummy and player

    if ((rb.velocity.x <= 0.1 && rb.velocity.x >= -0.1) || (rb.velocity.y <= 0.1 && rb.velocity.y >= -0.1))
    {
        isChasing = false;
        animator.SetTrigger("EndChase");
    }
    else
    {
        // If there is no wall between mummy and player, start chasing if player is within field of view
        if (angleToPlayer < fieldOfView / 2 && distanceToPlayer < visionRange)
        {
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
    }
    else
    {
        rb.velocity = directionToPlayer * chaseSpeed;
    }
}
}