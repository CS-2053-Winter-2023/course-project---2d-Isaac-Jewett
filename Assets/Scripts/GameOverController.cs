using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public void RetryLevel()
    {
        SceneManager.LoadScene("JournalEntries");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
