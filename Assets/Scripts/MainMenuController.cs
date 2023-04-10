using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    public void playGame() 
    {
        SceneManager.LoadScene("Room1");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
