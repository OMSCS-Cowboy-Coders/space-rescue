using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PanelMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component is missing");
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
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
        // TODO: show 1, 2, or 3 stars depending on what's passed in here from GameStatusManager.

    }
}
