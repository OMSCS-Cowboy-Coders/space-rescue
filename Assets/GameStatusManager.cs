using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    NotStarted,
    Playing,
    Paused,
    Complete
}
public class GameStatusManager : MonoBehaviour
{
    // Start is called before the first frame update
    private const string collectibleBatteryName = "Battery_small_06";
    private int totalNumParts;
    public PlayerController playerController;

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

    private GameState gameState;
    void Start()
    {
        // get the player controller script

        totalNumParts = findItems(collectibleBatteryName);
        findPlayerController();
        gameState = GameState.NotStarted;
        startTime = Time.time;

    }

    // Update is called once per frame

    public void startGame() {
        gameState = GameState.Playing;
        // start timer
    }

    // TODO: Make a function that will pause the timer if the game is paused.

    void Update()
    {
        findPlayerController();
        // check for playerController's numPartsRecieved

        if (playerController.numPartsRecieved == totalNumParts || Input.GetKeyDown(KeyCode.G)) {
            // Game is complete
            endTime = Time.time;
            float duration = endTime - startTime;
            print("Total duration of the game is " + duration);
        }
    }
}
