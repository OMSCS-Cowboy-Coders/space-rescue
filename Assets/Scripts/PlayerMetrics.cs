using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
1. Book keeping for sprints
Keeping track of how much energy 
Factors that inform energy: 
1. How long the button has been pressed 
2. Recharge speed   => 5% every second
3. Discharge speed  => 10% every second
4. Capacity         => 100%

5. When sprint power up is used, for 30 seconds, it will double the sprint capacity and increase recharge speed by +10%.

Also will need to factor the max amoutn of sprint in (reverse engineer this by determining a max sprint time of 10 seconds)

*/
public class PlayerMetrics : MonoBehaviour
{
    // Start is called before the first frame update
    public static int MAX_HEALTH = 3;
    public static float POWERUP_SPRINT_SPEED = 3f;

    private float moveSpeed = 1.0f;
    private float sprintAnimSpeed = 1.0f;
    private int health;
    private int sprintPowerupsLeft;
    private bool isUsingSprintPowerup;

    private float sprintEnergy = 100.0f;

    private PlayerController playerController;

    private Coroutine alterEnergyCoroutine;
    void Start()
    {
        health = MAX_HEALTH;
        sprintPowerupsLeft = 0;
        isUsingSprintPowerup = false;

        playerController = gameObject.GetComponent<PlayerController>(); // update values here.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getMoveSpeed() {
        return moveSpeed;
    }
    
    public float getSprintAnimSpeed() {
        return sprintAnimSpeed;
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
        Debug.Log("Sprint set to 1.2x");
        isUsingSprintPowerup = true;
        
        yield return new WaitForSeconds(POWERUP_SPRINT_SPEED);
        
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

    private bool canSprint() {
        return sprintEnergy > 0;
    }

    private IEnumerator AlterEnergyOverTime(float energyDelta)
    {
        while (0 <= sprintEnergy && sprintEnergy <= 100)
        {
            if (energyDelta < 0) {
                print("Sprinting. Energy left: " + sprintEnergy);
            }
            if (energyDelta > 0) {
                print("Recharging. Energy left: " + sprintEnergy);
            }
            sprintEnergy += energyDelta;
            yield return new WaitForSeconds(1f);
        }
        sprintEnergy = Mathf.Clamp(sprintEnergy, 0, 100); // Ensure sprintEnergy stays within 0 and 100
        print("Coroutine is stopped. This is sprintEnergy " + sprintEnergy);
        if (!canSprint()) {
            stopSprint();
        }
    }

    private void changeSprintEnergy(float energyDelta)
    {
        if (alterEnergyCoroutine != null)
        {
            print("Switching!");
            StopCoroutine(alterEnergyCoroutine);
        }
        alterEnergyCoroutine = StartCoroutine(AlterEnergyOverTime(energyDelta));
    }
    public void startSprint() {
        // determine if we can sprint here
        // canSprint 
        // update values accordingly.

        if (canSprint()) {
            playerController.moveSpeed = 1.3f;
            playerController.sprintAnimSpeed = 1.3f;
            // discharge sprint energy
            changeSprintEnergy(-10.0f);
        }
    }
    public void stopSprint() {
        playerController.moveSpeed = 1.0f;
        playerController.sprintAnimSpeed = 1.0f;
        // recharge sprint energy
        changeSprintEnergy(5.0f);
    }

}
