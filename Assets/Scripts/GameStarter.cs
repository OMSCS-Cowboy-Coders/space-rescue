using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    public Button startButton;
    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame() {
        Debug.Log("Hello");
        SceneManager.LoadScene("demo");
    }
}
