using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CanvasStates {
    Pause,
    WinScreen
}

[RequireComponent(typeof(CanvasGroup))]
public class PanelMenu : MonoBehaviour
{
    public Canvas canvas;
    private CanvasGroup canvasGroup;
    private Transform pausePanel;
    private Transform winScreenPanel;

    private CanvasGroup selectedScreen;

    public Sprite oneStarImage;
    public Sprite twoStarsImage;
    public Sprite threeStarsImage;

    public TextMeshProUGUI winScreenText;
    

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

    public void showCompletion(int numStars, float duration)
    {
        showScreen(CanvasStates.WinScreen);
        Image winImage = winScreenPanel.GetComponentInChildren<Image>();
        winImage.color = new Color(1f, 1f, 1f);
        switch (numStars) {
            case 1:
                winImage.sprite = oneStarImage;
                break;
            case 2:
                winImage.sprite = twoStarsImage;
                break;
            case 3:
                winImage.sprite = threeStarsImage;
                break;
        }
        winScreenText.text = "Congratulations! You got " + numStars + " stars and finished in " + (int)duration + " seconds!";
        ToggleMenu();
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
