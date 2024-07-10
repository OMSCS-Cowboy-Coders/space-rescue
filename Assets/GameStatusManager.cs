using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CompletionStars {
    OneStar = 600, // 10 minutes
    TwoStars = 540, // 9 minutes
    ThreeStars = 480 // 8 minutes
}
public enum GameState {
    NotStarted,
    Playing,
    Paused,
    Complete
}
public class GameStatusManager : MonoBehaviour
{

    private const string collectibleBatteryName = "Battery_small_06";
    private int totalNumParts;
    public PlayerController playerController;
    public PanelMenu panelMenu;

    private float startTime;
    private float endTime;

    private int findItems(string targetName) {
        int count = 0;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == targetName)
            {
                count++;
            }
        }
        return count;
    }

    private void findPlayerController() {
        GameObject player = GameObject.Find("Player");
        if (player != null && playerController == null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    private void findPanelMenu() {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null && panelMenu == null)
        {
            panelMenu = canvas.GetComponent<PanelMenu>();
        }
    }

    private GameState gameState;
    void Start()
    {
        // get the player controller script

        findPlayerController();
        findPanelMenu();

        totalNumParts = findItems(collectibleBatteryName);
        print("Total number of collectible ship parts: " + totalNumParts);
        gameState = GameState.NotStarted;
        startTime = Time.time;

    }

    // Update is called once per frame

    public void startGame() {
        gameState = GameState.Playing;
        // start timer
    }

    // TODO: Make a function that will pause the timer if the game is paused.

    private void completeGame() {
        gameState = GameState.Complete;
        endTime = Time.time;

        float duration = endTime - startTime;
        print("Total duration of the game is " + duration);


        int stars;
        switch (duration) {
            case var expression when duration < (int) CompletionStars.ThreeStars:
                stars = 3;
                break;
            case var expression when duration < (int) CompletionStars.TwoStars:
                stars = 2;
                break;
            case var expression when duration < (int) CompletionStars.OneStar:
                stars = 1;
                break;
            default: 
                stars = 1;
                break;
        }

        panelMenu.showCompletion(stars);
    }

    void Update()
    {
        findPlayerController();
        findPanelMenu();

        if (playerController.numPartsRecieved == totalNumParts || Input.GetKeyDown(KeyCode.G)) {
            completeGame();
        }
    }
}
