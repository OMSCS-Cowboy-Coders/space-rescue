using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public static string sceneName = "Mars_Scene";
    public static string sceneName2 = "Instructions";
    public static string sceneName3 = "StartMenu";

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
}

