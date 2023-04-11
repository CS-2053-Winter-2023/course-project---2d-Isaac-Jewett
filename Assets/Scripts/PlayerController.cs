using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{

    //Components
    Rigidbody2D rb;

    public GameObject loreEntry;
    public GameObject exitTrigger;
    private LoreController loreController;
    
    //Movement
    public float speed = 2.0f;
    public float speedLimiter = 0.7f;
    private float inputHorizontal;
    private float inputVertical;

    new public Camera camera;

    // Animations and states
    private SpriteRenderer rend;
    private Animator anim;
    string currentState;
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RIGHT = "Player_Right";
    const string PLAYER_LEFT = "Player_Left";
    const string PLAYER_UP = "Player_Up";
    const string PLAYER_DOWN = "Player_Down";

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        loreController = loreEntry.GetComponent<LoreController>();
        if(SceneManager.GetActiveScene().name == "Room1"){
            exitTrigger.SetActive(false);
        }
        rend = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            rb.velocity = new Vector2(inputHorizontal * speed, inputVertical * speed);

            if (inputHorizontal > 0)
            {
                ChangeAnimationState(PLAYER_RIGHT);
            }
            else if (inputHorizontal < 0)
            {
                ChangeAnimationState(PLAYER_LEFT);
            }
            else if (inputVertical > 0)
            {
                ChangeAnimationState(PLAYER_UP);
            }
            else if (inputVertical < 0)
            {
                ChangeAnimationState(PLAYER_DOWN);
            }
        }
        else
        {
            rb.velocity = new Vector2(0,0);
            ChangeAnimationState(PLAYER_IDLE);
        }
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

    }

    //Animation state changer
    void ChangeAnimationState(string newState)
    {

        //Stop animation from interupting itself
        if (currentState == newState) return;

        //Play new animation
        anim.Play(newState);

        // Update current state
        currentState = newState;
    }
}