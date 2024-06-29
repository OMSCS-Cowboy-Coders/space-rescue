using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMetrics : MonoBehaviour
{
    // Start is called before the first frame update
    public static int MAX_HEALTH = 3;
    public static float POWERUP_SPRINT_SPEED = 3f;
    private int health;
    private int sprintPowerupsLeft;
    private bool isUsingSprintPowerup;
    private float sprintSpeed;

    void Start()
    {
        health = MAX_HEALTH;
        sprintPowerupsLeft = 0;
        isUsingSprintPowerup = false;
        sprintSpeed = 1.0f;
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
        sprintPowerupsLeft++;
        print("INCREMENTING SPRINT POWERUPS: " + sprintPowerupsLeft);
        useSprintPowerup(); // TODO: remove this, trigger this by a button.
    }


    private void toggleSprintPowerup() {
        isUsingSprintPowerup = !isUsingSprintPowerup;
    }

    private IEnumerator enhanceSprintAbility() {
        
        // set the RigidBody's sprint to like 1.2x as fast
        // Alternatively, set sprint capacity to like 1.4x longer
        sprintSpeed = 1.2f;
        Debug.Log("Sprint set to 1.2x");
        isUsingSprintPowerup = true;
        
        yield return new WaitForSeconds(POWERUP_SPRINT_SPEED);
        
        sprintSpeed = 1.0f;
        isUsingSprintPowerup = false;
        Debug.Log("Sprint set back to 1");
    }

    public void useSprintPowerup() {
        // if there are no sprint power ups left,
        // or if the character is currently using a sprint powerup
        // then do not activate the sprint powerup.
        if (sprintPowerupsLeft <= 0 || isUsingSprintPowerup) return;


        sprintPowerupsLeft--;
        toggleSprintPowerup();

        StartCoroutine(enhanceSprintAbility());
        // get the rigidbody and assign the speed of which it travels for 30 seconds

    }
}
