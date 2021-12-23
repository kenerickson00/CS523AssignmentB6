using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class textHandler : MonoBehaviour
{

    void OnMouseOver()
    {
        GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1);
        GetComponent<TextMesh>().color = new Color(1, 0, 0, 1);
    }

    void OnMouseExit()
    {
        GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        GetComponent<TextMesh>().color = new Color(0, 26f/255f, 169f/255f, 1);
    }

    void OnMouseDown()
    {
        string buttonClicked = GetComponent<TextMesh>().text;
        if(buttonClicked == "Start Game")
            SceneManager.LoadScene("OtherLevel topdown");
        else if(buttonClicked == "Demo Area")
            SceneManager.LoadScene("testscene");
        else
        {
            Debug.Log("Testing");
            Application.Quit();
        }
    }
}
