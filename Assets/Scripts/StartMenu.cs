using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public static string sceneName = "Mars_Scene";
    public static string sceneName2 = "Instructions";
    public static string sceneName3 = "StartMenu";
    public static string sceneName4 = "GameCredits";

    public void PlayButton()
    {
        SceneManager.LoadScene("Mars_Scene");
    }

    public void ShowInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void ReturnToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void GameCredits()
    {
        SceneManager.LoadScene("GameCredits");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}


