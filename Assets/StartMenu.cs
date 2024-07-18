using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public static string sceneName = "Mars_Scene";
    public static string sceneName2 = "Instructions";
    public static string sceneName3 = "StartMenu";
    // This function is called when the Start button is clicked
    public void PlayButton()
    {
        SceneManager.LoadScene("Mars_Scene");
    }

    // This function is called when the Instructions button is clicked
    public void ShowInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    // This function is called when the Exit button in the Instructions scene is clicked
    public void ReturnToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

