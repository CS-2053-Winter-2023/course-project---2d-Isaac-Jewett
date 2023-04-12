using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;

    public GameObject loreEntry;
    public GameObject exitTrigger;
    private LoreController loreController;

    private SpriteRenderer rend;
    private Animator anim;
<<<<<<< Updated upstream
    public float speed = 2.0f;
    new public Camera camera;
=======
    string currentState;
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RIGHT = "Player_Right";
    const string PLAYER_LEFT = "Player_Left";
    const string PLAYER_UP = "Player_Up";
    const string PLAYER_DOWN = "Player_Down";
    Level1Mummy[] myScriptReferences;


>>>>>>> Stashed changes
    void Start()
    {
        loreController = loreEntry.GetComponent<LoreController>();
        if(SceneManager.GetActiveScene().name == "Room1"){
            exitTrigger.SetActive(false);
        }
      
        velocity = new Vector3(0f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
<<<<<<< Updated upstream
        anim = GetComponent<Animator>();
=======
        anim = gameObject.GetComponent<Animator>();
        myScriptReferences = FindObjectsOfType<Level1Mummy>();
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
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

        transform.position = transform.position + velocity * Time.deltaTime * speed;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lore")) {
            loreController.SetLoreActive();
        }
        if (other.CompareTag("Exit"))
        {
            if (SceneManager.GetActiveScene().name == "Room1")
            {
                SceneManager.LoadScene("Room2");
            }
            if (SceneManager.GetActiveScene().name == "Room2")
            {
                SceneManager.LoadScene("Room3");
            }
            if (SceneManager.GetActiveScene().name == "Room3")
            {
                SceneManager.LoadScene("Room4");
            }
            if (SceneManager.GetActiveScene().name == "Room4")
            {
                SceneManager.LoadScene("Room5");
            }
            if (SceneManager.GetActiveScene().name == "Room5")
            {
                SceneManager.LoadScene("Room6");
            }
            if (SceneManager.GetActiveScene().name == "Room6")
            {
                SceneManager.LoadScene("Room7");
            }
            if (SceneManager.GetActiveScene().name == "Room7")
            {
                SceneManager.LoadScene("Room8");
            }
            if (SceneManager.GetActiveScene().name == "Room8")
            {
                SceneManager.LoadScene("End");
            }
        }

        if (other.CompareTag("TopT"))  {
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


    }
}