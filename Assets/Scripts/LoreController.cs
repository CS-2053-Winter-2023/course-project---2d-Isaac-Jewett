using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreController : MonoBehaviour
{

    public GameObject loreEntryImage;
    public GameObject loreEntry;
    private bool isLoreActive = false;

    public float amplitude; // Height of the float
    public float speed; // Speed of the float

    private float startY; // Starting Y position of the object

    // Start is called before the first frame update
    void Start()
    {
        loreEntryImage.SetActive(false);

        startY = transform.position.y; // Save the starting Y position of the object

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && isLoreActive)
        {

            Time.timeScale = 1;
            loreEntryImage.SetActive(false);
            Destroy(loreEntry);

        }
    }

    public void SetLoreActive()
    {

        isLoreActive = true;
        loreEntryImage.SetActive(true);
        Time.timeScale = 0;

    
    }

    private void FixedUpdate()
    {
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude; // Calculate the new Y position using a sine wave
        transform.position = new Vector3(transform.position.x, newY, transform.position.z); // Set the new position of the object
    }












}
