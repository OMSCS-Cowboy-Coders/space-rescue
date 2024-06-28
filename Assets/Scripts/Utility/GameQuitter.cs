using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameQuitter : MonoBehaviour
{
    // Start is called before the first frame update

    public Button exitButton;
    void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(QuitGame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
            QuitGame();
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
