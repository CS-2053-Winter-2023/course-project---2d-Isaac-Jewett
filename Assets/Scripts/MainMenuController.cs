using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    public void playGame() 
    {
        EntryController.nextLevel = "Room1";
        EntryController.currentEntry = EntryController.CurrentEntry.entry1;
        SceneManager.LoadScene("JournalEntries");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
