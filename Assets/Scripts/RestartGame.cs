using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartGame : MonoBehaviour

{
    public string sceneName = "Mars_Scene"; 

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }
}
