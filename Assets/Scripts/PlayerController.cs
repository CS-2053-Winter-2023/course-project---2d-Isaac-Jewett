using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;

    public GameObject loreEntry;
    private LoreController loreController;

    private SpriteRenderer rend;
    private Animator anim;
    public float speed = 2.0f;
    public Camera camera;

    Level1Mummy[] myScriptReferences;

    void Start()
    {
        loreController = loreEntry.GetComponent<LoreController>();
        velocity = new Vector3(0f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        myScriptReferences = FindObjectsOfType<Level1Mummy>();

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
        velocity = new Vector3(0f, 0f, 0f);
        if (Input.GetKey("a"))
        {
            velocity = new Vector3(velocity.x -1f * speed, velocity.y, velocity.z);
          //  anim.Play("PacManLeft");
        }
        if (Input.GetKey("d"))
        {
            velocity = new Vector3(velocity.x + 1f * speed, velocity.y, velocity.z);
            // anim.Play("PacManRight");
        }
        if (Input.GetKey("s"))
        {
            velocity = new Vector3(velocity.x, velocity.y - 1f * speed, velocity.z);
            // anim.Play("PacManDown");
        }
        if (Input.GetKey("w"))
        {
            velocity = new Vector3(velocity.x, velocity.y + 1f * speed, velocity.z);
            // anim.Play("PacManUp");
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
        if (other.CompareTag("Lore")) {
            loreController.SetLoreActive();
        }   
        else if (other.CompareTag("TopT"))  {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 3)   {
                    myScript.MummyLevel = 0;
                }
            }
        }   
        else if (other.CompareTag("MidT"))  {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 2)   {
                    myScript.MummyLevel = 0;
                }
            }            
        }   
        else if (other.CompareTag("LowT"))  {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 1)   {
                    myScript.MummyLevel = 0;
                }
            }            
        }
        else
        {
            velocity = new Vector3(-velocity.x, -velocity.y, -velocity.z);
        }

    }
}