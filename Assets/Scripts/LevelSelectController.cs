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
        SceneManager.LoadScene("Room1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Room2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Room3");
    }
}
