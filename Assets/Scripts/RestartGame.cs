using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartGame : MonoBehaviour

{
    public static string sceneName = "Mars_Scene"; 

    // public void StartGame()
    // {
    //     Restart();
    // }
    public static void Restart() {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }
}
