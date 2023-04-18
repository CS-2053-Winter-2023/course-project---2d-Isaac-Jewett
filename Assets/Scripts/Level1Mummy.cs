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
    public GameObject Mummy;
    public bool MummyExists = false;
    public int MummyLevel;
    public int MummyStatic;

    void Start()
    {
        Mummy.GetComponent<Renderer>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        MummyStatic = MummyLevel;
        animator.SetTrigger("StartPatrol");
    }

    void Update()
    {
        if (!MummyExists && MummyLevel == 0)    {
            MummyLevel = MummyStatic;
        }

        if (MummyExists)    {
            if (MummyLevel == 0)    {
                Mummy.GetComponent<Renderer>().enabled = true;
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