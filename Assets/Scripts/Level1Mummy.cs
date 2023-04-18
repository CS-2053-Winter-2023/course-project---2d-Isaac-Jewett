using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class Level1Mummy : MonoBehaviour
{
    public float MummySpeed;
    public float chaseSpeed;

    public Rigidbody2D rb;
    private Animator animator;
    public Vector2[] patrolPoints;
    private int currentPatrolPointIndex = 0;
    public GameObject Mummy;
    public bool MummyExists = false;
    public int MummyLevel;
    public int MummyStatic;
    private Transform player;
    private AudioSource audioSource;
    public bool oneTimeBool;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Mummy.GetComponent<Renderer>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        animator = GetComponent<Animator>();
        MummyStatic = MummyLevel;
        animator.SetTrigger("StartPatrol");
        audioSource = GetComponent<AudioSource>();
        oneTimeBool = true;
    }

    void Update()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(6, 6, 1); // Reset the object's scale
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-6, 6, 1); // Flip the object horizontally
        }
        if (currentPatrolPointIndex == patrolPoints.Length)  {
            rb.velocity = Vector2.zero;
            MummyLevel = 2;
        }
        if (!MummyExists && MummyLevel == 0)    {
            MummyLevel = MummyStatic;
        }

        if (MummyExists)    {
            rb.simulated = true;
            if (MummyLevel == 0)    {
                Mummy.GetComponent<Renderer>().enabled = true;
                if (oneTimeBool)    {
                    audioSource.Play();
                    oneTimeBool = false;
                }

                if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPointIndex]) < 0.1f)
                {
                    currentPatrolPointIndex++;
                    if (currentPatrolPointIndex == patrolPoints.Length)  {
                        rb.velocity = Vector2.zero;
                        MummyLevel = 2;
                    }
                }

                    Vector2 directionToPatrolPoint = (patrolPoints[currentPatrolPointIndex] - (Vector2)transform.position).normalized;
                    rb.velocity = directionToPatrolPoint * MummySpeed;


            }
        }

        if (SceneManager.GetActiveScene().name == "Room8" && MummyLevel == 0)  {
            rb.velocity = directionToPlayer * chaseSpeed;

        }

    }
}