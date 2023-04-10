using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreController : MonoBehaviour
{

    public GameObject loreEntryImage;
    public GameObject loreEntry;
    private bool isLoreActive = false;

    // Start is called before the first frame update
    void Start()
    {
        loreEntryImage.SetActive(false);
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

}
