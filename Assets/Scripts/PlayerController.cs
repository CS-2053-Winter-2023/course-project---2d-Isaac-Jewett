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
    private Animator animDoor;
    string currentState;
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RIGHT = "Player_Right";
    const string PLAYER_LEFT = "Player_Left";
    const string PLAYER_UP = "Player_Up";
    const string PLAYER_DOWN = "Player_Down";
    const string DOOR_OPEN = "DoorOpening";
    

    // Audio
    private AudioSource audioSource;
    public float stepInterval = 1f;
    private float stepTimer = 0.0f;

    // Hide Mechanics
    public bool isHidden;
    private Vector2 tempLocation;
    private bool canHide;
    private Vector3 sarLocation;
    private SpriteRenderer sarRend;
    private string sarOrientation;
    public Sprite sarHClosed;
    public Sprite sarVClosed;
    public Sprite sarHOpen;
    public Sprite sarVOpen;

    //lights
    public LightController lightController;
    public GameObject sarLight;

    //Key stuff
    private bool gotKey;

    Level1Mummy[] myScriptReferences;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        loreController = loreEntry.GetComponent<LoreController>();
        if(SceneManager.GetActiveScene().name == "Room1"){
            exitTrigger.SetActive(false);
        }
        rend = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        myScriptReferences = FindObjectsOfType<Level1Mummy>();

        audioSource = GetComponent<AudioSource>();
        isHidden = false;
        canHide = false;
        gotKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            speed = 10f;
        }
            if (Input.GetKeyDown("space") && canHide && !isHidden)
        {
            isHidden = true;
            lightController.isHidden = true;
            speed = 0f;
            tempLocation = gameObject.transform.position;
            camera.transform.position = new Vector3(sarLocation.x, sarLocation.y, -10f);
            gameObject.transform.position = new Vector3(1000000000f, 10000000f, 1f);
            sarLight.transform.position = new Vector3(sarLocation.x, sarLocation.y, 1f);
            if (sarOrientation == "H")
            {
                sarRend.sprite = sarHClosed;
            }
            else
            {
                sarRend.sprite = sarVClosed;
            }
        }
        else if (Input.GetKeyDown("space") && isHidden)
        {
            canHide = true;
            isHidden = false;
            lightController.isHidden = false;
            speed = 2.0f;
            gameObject.transform.position = tempLocation;
            sarLight.transform.position = new Vector3(1000000000f, 10000000f, 1f);
            if (sarOrientation == "H")
            {
                sarRend.sprite = sarHOpen;
            }
            else
            {
                sarRend.sprite = sarVOpen;
            }
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
        if (other.CompareTag("TopT"))   {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 3)    {
                    myScript.MummyLevel = 0;
                }
            }            
        }
        if (other.CompareTag("MidT"))   {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 2)    {
                    myScript.MummyLevel = 0;
                }
            }
        }
        if (other.CompareTag("LowT"))   {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 1)    {
                    myScript.MummyLevel = 0;
                }
            }
        }
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
                EntryController.currentEntry = EntryController.CurrentEntry.entry9;
                SceneManager.LoadScene("JournalEntries");
            }
        }
        if (other.CompareTag("Door") && gotKey)
        {
            animDoor = other.GetComponent<Animator>();
            animDoor.Play(DOOR_OPEN);
            speed = 0;
            StartCoroutine(WaitAndExecute());
            IEnumerator WaitAndExecute()
            {
                yield return new WaitForSeconds(0.3f); // Wait for 2 seconds
                Destroy(other.gameObject); // Code to execute after waiting
                speed = 2.0f;
            }
        }
        if (other.CompareTag("Key"))
        {
            gotKey = true;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("SarcophagusH"))
        {
            sarRend = other.GetComponent<SpriteRenderer>();
            canHide = true;
            isHidden = false;
            sarLocation = new Vector3 (other.transform.position.x, other.transform.position.y, -10f) ;
            sarOrientation = "H";
        }
        if (other.CompareTag("SarcophagusV"))
        {
            sarRend = other.GetComponent<SpriteRenderer>();
            canHide = true;
            isHidden = false;
            sarLocation = new Vector3(other.transform.position.x, other.transform.position.y, -10f);
            sarOrientation = "V";
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SarcophagusH"))
        {
            canHide = false;
        }
        if (other.CompareTag("SarcophagusV"))
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