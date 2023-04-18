using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering.Universal;


public class PlayerController : MonoBehaviour
{

    //Components
    Rigidbody2D rb;

    public GameObject loreEntry;
    public GameObject exitTrigger;
    public GameObject waypoint;
    private LoreController loreController;
    private MummyController mummyController;
    private Level1Mummy level1MummyController;

    //Movement
    public float speed = 2.0f;
    public float speedLimiter = 0.7f;
    private float inputHorizontal;
    private float inputVertical;

    new public Camera camera;

    // Animations and states
    private bool isDying;
    private SpriteRenderer rend;
    private Animator anim;
    private Animator animDoor;
    string currentState;
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RIGHT = "Player_Right";
    const string PLAYER_UP = "Player_Up";
    const string PLAYER_DOWN = "Player_Down";
    const string DOOR_OPEN = "DoorOpening";
    const string MUMMY_HIT = "MummyAttack";
    const string DEATH = "Player_Death";
    

    // Audio
    private AudioSource audioSource;
    public AudioClip deathSFX;
    public AudioClip keySFX;
    public AudioClip loreSFX;
    public AudioClip StonePush;
    public AudioClip Sarcophagus;
    public AudioClip Waypoint;
    public AudioClip CanUseWay;
    public AudioClip Flash1;
    public AudioClip Flash2;
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
    public Light2D flashLight;

    //Key stuff
    private bool gotKey;

    //Upgrade stuff
    private bool gotUpgrade;
    private bool canBeam;

    //Waypoint stuff
    private bool gotWaypoints;
    private int count;

    Level1Mummy[] myScriptReferences;
    public GameObject[] mummies;

    void Start()
    {
        isDying = false;
        flashLight = flashLight.GetComponent<Light2D>();
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
        gotUpgrade = false;
        gotWaypoints = false;
        count = 0;
        if (SceneManager.GetActiveScene().name == "Room6" || SceneManager.GetActiveScene().name == "Room7" || SceneManager.GetActiveScene().name == "Room8")
        {
            gotWaypoints = true;
        }
        if (SceneManager.GetActiveScene().name == "Room7" || SceneManager.GetActiveScene().name == "Room8")
        {
            gotUpgrade = true;
        }
        canBeam = true;
        mummies = GameObject.FindGameObjectsWithTag("Mummy");

    }

    // Update is called once per frame
    void Update()
    {
        //Speed hack for devs
        if (Input.GetKeyDown("0"))
        {
            speed = 10f;
        }

        //Sarcophagus hiding 
        if (Input.GetKeyDown("space") && canHide && !isHidden)
        {
            audioSource.PlayOneShot(Sarcophagus);
            isHidden = true;
            lightController.isHidden = true;
            speed = 0f;
            tempLocation = gameObject.transform.position;
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
            audioSource.PlayOneShot(Sarcophagus);
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
        if (Input.GetMouseButtonDown(0) && gotUpgrade && canBeam)
        {
            audioSource.PlayOneShot(Flash2);
            StartCoroutine(FlashRed());
            IEnumerator FlashRed()
            {
                //Change flashLight colour to red
                flashLight.color = Color.red;
                flashLight.intensity = 3f;
                canBeam = false;
                foreach (GameObject mummy in mummies)
                {
                    mummyController =  mummy.GetComponent<MummyController>();
                    mummyController.StunMummy(transform.position);
                }

                    // Wait for the specified duration
                    yield return new WaitForSeconds(0.5f);

                // Set the colour back to white
                flashLight.color = Color.white;
                flashLight.intensity = 1;

                // Wait for the specified duration
                yield return new WaitForSeconds(10f);
                canBeam = true;

            }
        }
        if (Input.GetKeyDown("r") && gotWaypoints)
        {
            audioSource.PlayOneShot(Waypoint);
            GameObject newWaypoint = Instantiate(waypoint, transform.position, waypoint.transform.rotation);
        }
    }

    void FixedUpdate()
    {

        //Player movement
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        if (!isDying)
        {
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
                rb.velocity = new Vector2(0, 0);
                ChangeAnimationState(PLAYER_IDLE);
                stepTimer = 0.0f;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

        //Camera control
        if (!isHidden)
        {
            camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //Mummy stuff
        if (other.CompareTag("TopT"))   {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 3)    {
                    myScript.MummyLevel = 0;
                    myScript.rb.simulated = true;

                }
            }            
        }
        if (other.CompareTag("MidT"))   {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 2)    {
                    myScript.MummyLevel = 0;
                    myScript.rb.simulated = true;

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

        if (other.CompareTag("Collider 1"))   {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 1)    {
                    Debug.Log("Test");
                    myScript.MummyLevel = 0;
                    myScript.MummyExists = true;
                    myScript.rb.simulated = true;

                }
            }
        }
        if (other.CompareTag("Collider 2"))   {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 1)    {
                    myScript.MummyLevel = 0;
                    myScript.MummyExists = true;
                    myScript.rb.simulated = true;

                }
            }
        }
        if (other.CompareTag("Collider 3"))   {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 1)    {
                    myScript.MummyLevel = 0;
                    myScript.MummyExists = true;
                    myScript.rb.simulated = true;

                }
            }
        }
        if (other.CompareTag("Collider 4"))   {
            foreach (Level1Mummy myScript in myScriptReferences)
            {
                if (myScript.MummyLevel == 1)    {
                    myScript.MummyLevel = 0;
                    myScript.MummyExists = true;
                    myScript.rb.simulated = true;

                }
            }
        }
        //Lore pickups
        if (other.CompareTag("Lore")) {
            audioSource.PlayOneShot(loreSFX);
            loreController.SetLoreActive();
        }

        //Death handling
        if (other.CompareTag("Mummy"))
        {
            //Stop player movement
            speed = 0;
            isDying = true;

            //Stop mummy movement
            mummyController = other.GetComponent<MummyController>();
            Rigidbody2D mummyRb = other.GetComponent<Rigidbody2D>();
            mummyController.chaseSpeed = 0;
            mummyController.MummySpeed = 0;
            mummyRb.freezeRotation = true;
            
            
            //Mummy Hit animation
            Animator animMummy = other.GetComponent<Animator>();
            SpriteRenderer rendMummy = other.GetComponent<SpriteRenderer>();
            if (transform.position.x - other.transform.position.x > 0)
            {
                animMummy.Play(MUMMY_HIT);
                rendMummy.flipX = false;
            }
            else
            {
                animMummy.Play(MUMMY_HIT);
                rendMummy.flipX = true;
            }
            StartCoroutine(WaitAndExecute());
            IEnumerator WaitAndExecute()
            {
                yield return new WaitForSeconds(0.3f); 
                if (transform.position.x - other.transform.position.x > 0)
                {
                    anim.Play(DEATH);
                    rend.flipX = false;
                    audioSource.PlayOneShot(deathSFX, 0.6F);
                }
                else
                {
                    anim.Play(DEATH);
                    rend.flipX = true;
                    audioSource.PlayOneShot(deathSFX, 0.6F);
                }
                yield return new WaitForSeconds(3f);
                SceneManager.LoadScene("GameOver");
            }

            
        }

        //Death handling
        if (other.CompareTag("Level1Mummy"))
        {
            //Stop player movement
            speed = 0;
            isDying = true;

            //Stop mummy movement
            level1MummyController = other.GetComponent<Level1Mummy>();
            Rigidbody2D mummyRb = other.GetComponent<Rigidbody2D>();
            level1MummyController.MummySpeed = 0;
            mummyRb.freezeRotation = true;


            //Mummy Hit animation
            Animator animMummy = other.GetComponent<Animator>();
            SpriteRenderer rendMummy = other.GetComponent<SpriteRenderer>();
            if (transform.position.x - other.transform.position.x > 0)
            {
                animMummy.Play(MUMMY_HIT);
                rendMummy.flipX = false;
            }
            else
            {
                animMummy.Play(MUMMY_HIT);
                rendMummy.flipX = true;
            }
            StartCoroutine(WaitAndExecute());
            IEnumerator WaitAndExecute()
            {
                yield return new WaitForSeconds(0.3f);
                if (transform.position.x - other.transform.position.x > 0)
                {
                    anim.Play(DEATH);
                    rend.flipX = false;
                    audioSource.PlayOneShot(deathSFX, 0.6F);
                }
                else
                {
                    anim.Play(DEATH);
                    rend.flipX = true;
                    audioSource.PlayOneShot(deathSFX, 0.6F);
                }
                yield return new WaitForSeconds(3f);
                SceneManager.LoadScene("GameOver");
            }


        }

        //Level Transitions
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

        //Opens door with animation when touched with key
        if (other.CompareTag("Door") && gotKey)
        {
            animDoor = other.GetComponent<Animator>();
            animDoor.Play(DOOR_OPEN);
            audioSource.PlayOneShot(StonePush);
            speed = 0;
            StartCoroutine(WaitAndExecute());
            IEnumerator WaitAndExecute()
            {
                yield return new WaitForSeconds(0.3f); // Wait for 2 seconds
                Destroy(other.gameObject); // Code to execute after waiting
                speed = 2.0f;
            }
        }

        //picking up the key
        if (other.CompareTag("Key"))
        {
            audioSource.PlayOneShot(keySFX);
            gotKey = true;
            Destroy(other.gameObject);
        }

        //Handles sarcophogus hiding details
        if (other.CompareTag("SarcophagusH"))
        {
            sarRend = other.GetComponent<SpriteRenderer>();
            canHide = true;
            isHidden = false;
            sarLocation = new Vector3 (other.transform.position.x, other.transform.position.y, -10f) ;
            sarOrientation = "H";
        }

        //Handles sarcophogus hiding details
        if (other.CompareTag("SarcophagusV"))
        {
            sarRend = other.GetComponent<SpriteRenderer>();
            canHide = true;
            isHidden = false;
            sarLocation = new Vector3(other.transform.position.x, other.transform.position.y, -10f);
            sarOrientation = "V";
        }

        //Picking up the Flashlight upgrade
        if (other.CompareTag("Upgrade"))
        {
            audioSource.PlayOneShot(Flash1);
            gotUpgrade = true;
            Destroy(other.gameObject);
        }

        //Picking up the Waypoint upgrade
        if (other.CompareTag("Waypoint"))
        {
            if (!gotWaypoints)
            {
                audioSource.PlayOneShot(Waypoint);
                count++;
                if (count > 10)
                {
                    gotWaypoints = true;
                    audioSource.PlayOneShot(CanUseWay);
                }
                Destroy(other.gameObject);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Handles sarcophogus hiding details
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
        if (!isDying)
        {
            anim.Play(newState);
        }
       

        // Update current state
        currentState = newState;
    }
}