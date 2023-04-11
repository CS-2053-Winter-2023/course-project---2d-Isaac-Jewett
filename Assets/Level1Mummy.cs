using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Mummy : MonoBehaviour
{
    public float MummySpeed;

    private Rigidbody2D rb;
    private Animator animator;
    public Vector2[] patrolPoints;
    private int currentPatrolPointIndex = 0;

    private Transform MummySprite;

    void Start()
    {
        MummySprite = transform.Find("MummySprite");

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPointIndex]) < 0.1f)
        {
            currentPatrolPointIndex++;
            if (currentPatrolPointIndex == patrolPoints.Length)  {
                rb.velocity = Vector2.zero;
                return;
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
    
}