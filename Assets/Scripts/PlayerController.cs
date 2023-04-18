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
    

    // Audio
    private AudioSource audioSource;
    public float stepInterval = 1f;
    private float stepTimer = 0.0f;

    // Hide Mechanics
    private bool isHidden;
    private Vector2 tempLocation;
    private bool canHide;
    private Vector3 sarLocation;

    //lights
    public LightController lightController;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        loreController = loreEntry.GetComponent<LoreController>();
        if(SceneManager.GetActiveScene().name == "Room1"){
            exitTrigger.SetActive(false);
        }
        rend = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isHidden = false;
        canHide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && canHide && !isHidden)
        {
            isHidden = true;
            lightController.isHidden = true;
            speed = 0f;
            tempLocation = gameObject.transform.position;
            camera.transform.position = new Vector3(sarLocation.x, sarLocation.y, -10f);
            gameObject.transform.position = new Vector3(1000000000f, 10000000f, 1f);
        }
        else if (Input.GetKeyDown("space") && isHidden)
        {
            isHidden = false;
            lightController.isHidden = false;
            speed = 2.0f;
            gameObject.transform.position = tempLocation;
        }
    }

    void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                audioSource.Play();
                stepTimer = 0.0f;
            }

            if (inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            rb.velocity = new Vector2(inputHorizontal * speed, inputVertical * speed);

            if (inputHorizontal > 0)
            {
                ChangeAnimationState(PLAYER_RIGHT);
                rend.flipX = false;
            }
            else if (inputHorizontal < 0)
            {
                ChangeAnimationState(PLAYER_RIGHT);
                rend.flipX = true;
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
            stepTimer = 0.0f;
        }
        if (!isHidden)
        {
            camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lore")) {
            loreController.SetLoreActive();
        }
        if (other.CompareTag("Mummy"))
        {
            //Death
        }
        if (other.CompareTag("Exit"))
        {
            if (SceneManager.GetActiveScene().name == "Room1")
            {
                EntryController.nextLevel = "Room2";
                EntryController.currentEntry = EntryController.CurrentEntry.entry2;
                SceneManager.LoadScene("JournalEntries");
            }
            if (SceneManager.GetActiveScene().name == "Room2")
            {
                EntryController.nextLevel = "Room3";
                EntryController.currentEntry = EntryController.CurrentEntry.entry3;
                SceneManager.LoadScene("JournalEntries");
            }
            if (SceneManager.GetActiveScene().name == "Room3")
            {
                EntryController.nextLevel = "Room4";
                EntryController.currentEntry = EntryController.CurrentEntry.entry4;
                SceneManager.LoadScene("JournalEntries");
            }
            if (SceneManager.GetActiveScene().name == "Room4")
            {
                EntryController.nextLevel = "Room5";
                EntryController.currentEntry = EntryController.CurrentEntry.entry5;
                SceneManager.LoadScene("JournalEntries");
            }
            if (SceneManager.GetActiveScene().name == "Room5")
            {
                EntryController.nextLevel = "Room6";
                EntryController.currentEntry = EntryController.CurrentEntry.entry6;
                SceneManager.LoadScene("JournalEntries");
            }
            if (SceneManager.GetActiveScene().name == "Room6")
            {
                EntryController.nextLevel = "Room7";
                EntryController.currentEntry = EntryController.CurrentEntry.entry7;
                SceneManager.LoadScene("JournalEntries");
            }
            if (SceneManager.GetActiveScene().name == "Room7")
            {
                EntryController.nextLevel = "Room8";
                EntryController.currentEntry = EntryController.CurrentEntry.entry8;
                SceneManager.LoadScene("JournalEntries");
            }
            if (SceneManager.GetActiveScene().name == "Room8")
            {
                SceneManager.LoadScene("End");
            }
        }
        if (other.CompareTag("Sarcophagus"))
        {
            canHide = true;
            sarLocation = new Vector3 (other.transform.position.x, other.transform.position.y, -10f) ;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sarcophagus"))
        {
            canHide = false;
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