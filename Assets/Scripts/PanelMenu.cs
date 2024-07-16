using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum CanvasStates {
    Pause,
    WinScreen
}

[RequireComponent(typeof(CanvasGroup))]
public class PanelMenu : MonoBehaviour
{
    public Canvas canvas;
    private CanvasGroup canvasGroup;
    public Transform pausePanel;
    public Transform winScreenPanel;

    private CanvasGroup selectedScreen;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component is missing");
        }

        canvas = GetComponent<Canvas>();
        
        pausePanel = canvas.transform.Find("Panel");
        winScreenPanel = canvas.transform.Find("WinScreen");

    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            showScreen(CanvasStates.Pause);
            ToggleMenu();
        }
    }
    private void ToggleMenu()
    {
        bool isActive = canvasGroup.interactable;
        canvasGroup.interactable = !isActive;
        canvasGroup.blocksRaycasts = !isActive;
        canvasGroup.alpha = isActive ? 0f : 1f;
        Time.timeScale = isActive ? 1f : 0f;
    }

    public void showCompletion(int numStars)
    {
        showScreen(CanvasStates.WinScreen);
        ToggleMenu();
        // Make winScreenPanel visible, and Panel invisible
        // TODO: show 1, 2, or 3 stars depending on what's passed in here from GameStatusManager.
    }

    private void showScreen(CanvasStates state) {
        switch (state) {
            case CanvasStates.Pause:
                // pausePanel.gameObject.SetActive(true);
                pausePanel.gameObject.SetActive(true);
                winScreenPanel.gameObject.SetActive(false);
                break;
            case CanvasStates.WinScreen:
                pausePanel.gameObject.SetActive(false);
                winScreenPanel.gameObject.SetActive(true);
                break;
            default:
                break;
        }

    }
}
