using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMetrics : MonoBehaviour
{
    // Start is called before the first frame update
    public static int MAX_HEALTH = 3;
    private int health;
    private int sprintPowerupsLeft;
    private bool isUsingSprintPowerup;

    void Start()
    {
        health = MAX_HEALTH;
        sprintPowerupsLeft = 0;
        isUsingSprintPowerup = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementHealth() {
        /*
        This function will increment health only til it hits MAX_HEALTH
        */ 
        health = health >= MAX_HEALTH ? health : health++;
        print("INCREMENTING HEALTH: HEALTH IS " + health);
    }

    public void decrementHealth() {
        health--;
        if (health <= 0) {
            // endGame
            return;
        }
    }

    public void collectSprintPowerup() {
        print("INCREMENTING SPRINT POWERUPS");
        sprintPowerupsLeft++;
    }

    public void toggleSprintPowerup() {
        isUsingSprintPowerup = !isUsingSprintPowerup;
    }
    public void useSprintPowerup() {
        // if there are no sprint power ups left,
        // or if the character is currently using a sprint powerup
        // then do not activate the sprint powerup.
        if (sprintPowerupsLeft <= 0 || isUsingSprintPowerup) return;


        sprintPowerupsLeft--;
        toggleSprintPowerup();

        // get the rigidbody and assign the speed of which it travels for 30 seconds

    }
}
