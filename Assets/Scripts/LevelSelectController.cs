using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    // Start is called before the first frame update
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Level1()
    {
        EntryController.nextLevel = "Room1";
        EntryController.currentEntry = EntryController.CurrentEntry.entry1;
        SceneManager.LoadScene("JournalEntries");
    }

    public void Level2()
    {
        EntryController.nextLevel = "Room2";
        EntryController.currentEntry = EntryController.CurrentEntry.entry2;
        SceneManager.LoadScene("JournalEntries");
    }

    public void Level3()
    {
        EntryController.nextLevel = "Room3";
        EntryController.currentEntry = EntryController.CurrentEntry.entry3;
        SceneManager.LoadScene("JournalEntries");
    }
}
