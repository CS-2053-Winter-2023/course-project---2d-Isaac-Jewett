using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;

    private SpriteRenderer rend;
    private Animator anim;
    public float speed = 2.0f;
    public Camera camera;
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // calculate location of screen borders
        // this will make more sense after we discuss vectors and 3D
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        //get the width of the object
        float width = rend.bounds.size.x;
        float height = rend.bounds.size.y;

        //set the direction based on  input
        //note this is a simplified version //in assignment 1 we used a the Input Handler System
        if (Input.GetKey("left"))
        {
            velocity = new Vector3(-1f, 0f, 0f);
          //  anim.Play("PacManLeft");
        }
        else if (Input.GetKey("right"))
        {
            velocity = new Vector3(1f, 0f, 0f);
           // anim.Play("PacManRight");
        }
        else if (Input.GetKey("down"))
        {
            velocity = new Vector3(0f, -1f, 0f);
           // anim.Play("PacManDown");
        }
        else if (Input.GetKey("up"))
        {
            velocity = new Vector3(0f, 1f, 0f);
           // anim.Play("PacManUp");
        }
        else
        {
            velocity = new Vector3(0f, 0f, 0f);
        }

        //make sure the obect is inside the borders... if edge is hit reverse direction
        if ((transform.position.x <= leftBorder + width / 2.0) && velocity.x < 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.x >= rightBorder - width / 2.0) && velocity.x > 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.y <= bottomBorder + height / 2.0) && velocity.y < 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.y >= topBorder - height / 2.0) && velocity.y > 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        transform.position = transform.position + velocity * Time.deltaTime * speed;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    }
}