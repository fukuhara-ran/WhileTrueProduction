using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Option() 
    {
        Debug.Log("Options Button Clicked");
    }

    public void ExitGame() 
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }
}
