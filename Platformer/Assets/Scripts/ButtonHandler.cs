using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void startGame()
    {
        Debug.Log("Start the game");
        SceneManager.LoadScene("OtherLevel topdown");
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
