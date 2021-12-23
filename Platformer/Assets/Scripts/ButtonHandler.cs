using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("OtherLevel topdown");
    }

    public void levelTwo()
    {
        SceneManager.LoadScene("Level2");
    }

    public void levelThree()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void startDemo()
    {
        SceneManager.LoadScene("testscene");
    }

    public void endGame()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("start screen");
    }
}
