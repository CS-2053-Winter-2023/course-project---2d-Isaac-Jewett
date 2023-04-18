using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{
    public float MummySpeed;
    public float visionRange;
    public float chaseSpeed;
    public float fieldOfView;
    private float originalSpeed;
    private float originalChase;

    public Transform frontDirection; // Child object for the front direction

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isChasing;
    public AudioClip Stun;

    public Vector2[] patrolPoints;
    private int currentPatrolPointIndex = 0;

    private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("StartPatrol");
        audioSource = GetComponent<AudioSource>();
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
            audioSource.Play();
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

    public void StunMummy(Vector3 playerPosition)
    {
        if (Vector3.Distance(transform.position, playerPosition) <= 5f)
        {
            if (IsInCone(playerPosition, transform.position))
            {
                audioSource.PlayOneShot(Stun);
                StartCoroutine(SlowDownForSeconds(5f));
            }
        }
    }

    IEnumerator SlowDownForSeconds(float duration)
    {
        // Store original values
        originalSpeed = MummySpeed;
        originalChase = chaseSpeed;

        // Set the speed to 0
        MummySpeed = 0f;
        chaseSpeed = 0f;

        // Disable hitbox
        rb.simulated = false;

        // Pauses animation
        animator.speed = 0;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Set the speeds back to the original values
        MummySpeed = originalSpeed;
        chaseSpeed = originalChase;

        // Resume animation
        animator.speed = 1;

        //Re-enable hibox
        rb.simulated = true;
    }

    private bool IsInCone(Vector3 coneOrigin, Vector3 position)
    {
        //Calculate direction of cone using mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        Vector3 directionToMouse = mousePos - coneOrigin;

        Vector3 coneDirection = directionToMouse.normalized;

        // Calculate the direction from the cone origin to the position
        Vector3 directionToPosition = position - coneOrigin;

        // Calculate the angle between the direction and the cone's forward vector
        float angle = Vector3.Angle(directionToPosition, coneDirection);

        // Check if the angle is less than the cone angle and the position is within the outer radius
        if (angle < 40.05f / 2f && directionToPosition.magnitude < 5.330492f)
        {
                return true;
        }

        return false;
    }
}