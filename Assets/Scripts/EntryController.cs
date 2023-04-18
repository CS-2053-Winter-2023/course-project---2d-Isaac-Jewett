using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryController : MonoBehaviour
{
    public GameObject cover;
    public GameObject entry1;
    public GameObject entry2;
    public GameObject entry3;
    public GameObject entry4;
    public GameObject entry5;
    public GameObject entry6;
    public GameObject entry7;
    public GameObject entry8;
    public GameObject entry9;


    public enum CurrentEntry { 
    
        entry1,
        entry2,
        entry3,
        entry4,
        entry5,
        entry6,
        entry7,
        entry8,
        entry9
    
    }

    public static CurrentEntry currentEntry = CurrentEntry.entry1;
    public static string nextLevel = "Room1";

    private bool isDone;

    // Start is called before the first frame update
    void Start()
    {
        entry1.SetActive(false);
        entry2.SetActive(false);
        entry3.SetActive(false);
        entry4.SetActive(false);
        entry5.SetActive(false);
        entry6.SetActive(false);
        entry7.SetActive(false);
        entry8.SetActive(false);
        entry9.SetActive(false);
        cover.SetActive(false);
        isDone = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDone) {
            StartCoroutine(JournalRoutine());
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentEntry != CurrentEntry.entry9) {

            SceneManager.LoadScene(nextLevel);

        } else if (Input.GetKeyDown(KeyCode.Space) && currentEntry == CurrentEntry.entry9) {

            SceneManager.LoadScene("MainMenu");

        }



    }

    IEnumerator JournalRoutine() {

        if (currentEntry == CurrentEntry.entry1)
        {
            cover.SetActive(true);
            yield return new WaitForSeconds(1);
            entry1.SetActive(true);
            cover.SetActive(false);
            isDone = true;

        }
        else if (currentEntry == CurrentEntry.entry2)
        {
            cover.SetActive(true);
            yield return new WaitForSeconds(1);
            entry2.SetActive(true);
            cover.SetActive(false);
            isDone = true;

        }
        else if (currentEntry == CurrentEntry.entry3)
        {
            cover.SetActive(true);
            yield return new WaitForSeconds(1);
            entry3.SetActive(true);
            cover.SetActive(false);
            isDone = true;

        }
        else if (currentEntry == CurrentEntry.entry4)
        {
            cover.SetActive(true);
            yield return new WaitForSeconds(1);
            entry4.SetActive(true);
            cover.SetActive(false);
            isDone = true;

        }
        else if (currentEntry == CurrentEntry.entry5)
        {
            cover.SetActive(true);
            yield return new WaitForSeconds(1);
            entry5.SetActive(true);
            cover.SetActive(false);
            isDone = true;

        }
        else if (currentEntry == CurrentEntry.entry6)
        {
            cover.SetActive(true);
            yield return new WaitForSeconds(1);
            entry6.SetActive(true);
            cover.SetActive(false);
            isDone = true;

        }
        else if (currentEntry == CurrentEntry.entry7)
        {
            cover.SetActive(true);
            yield return new WaitForSeconds(1);
            entry7.SetActive(true);
            cover.SetActive(false);
            isDone = true;

        }
        else if (currentEntry == CurrentEntry.entry8)
        {
            cover.SetActive(true);
            yield return new WaitForSeconds(1);
            entry8.SetActive(true);
            cover.SetActive(false);
            isDone = true;

        }
        else if (currentEntry == CurrentEntry.entry9)
        {
            cover.SetActive(true);
            yield return new WaitForSeconds(1);
            entry9.SetActive(true);
            cover.SetActive(false);
            isDone = true;

        }



    }



}
