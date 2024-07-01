using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetrics : MonoBehaviour
{
    // Start is called before the first frame update
    public static int MAX_HEALTH = 3;
    public static float POWERUP_SPRINT_DURATION = 3f;

    private float regularMovingSpeed = 1.0f;
    private float regularAnimSpeed = 1.0f;

    private float sprintSpeed = 1.15f; // initially set as default
    private float sprintAnimSpeed = 1.15f; // initially set as default

    private float defaultSprintSpeed = 1.15f;
    private float defaultSprintAnimSpeed = 1.15f;

    private float powerupSprintSpeed = 2.0f;
    private float powerupSprintAnimSpeed = 2.0f;

    private int health;
    private int sprintPowerupsLeft;
    private bool isUsingSprintPowerup;

    private float sprintEnergy = 100.0f;
    private float maxSprintEnergy = 100.0f;
    private float SPRINT_RECHARGE_RATE = 5.0f;
    
    private float SPRINT_DISCHARGE_RATE = -10.0f;

    private PlayerController playerController;

    private Coroutine alterEnergyCoroutine;

    public HealthCollectibleUIManager healthCollectibleUIManager;
    public PowerupsUIManager powerupsUIManager;

    public RestartGame restartGame;

    void Start()
    {
        health = MAX_HEALTH;
        sprintPowerupsLeft = 0;
        isUsingSprintPowerup = false;

        playerController = gameObject.GetComponent<PlayerController>(); // update values here.

        sprintSpeed = defaultSprintSpeed;
        sprintAnimSpeed = defaultSprintAnimSpeed;

        healthCollectibleUIManager = FindObjectOfType<HealthCollectibleUIManager>();
        powerupsUIManager = FindObjectOfType<PowerupsUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public float getMoveSpeed() {
        return regularMovingSpeed;
    }
    
    public float getMoveAnimSpeed() {
        return regularAnimSpeed;
    }

    public void incrementHealth() {
        /*
        This function will increment health only til it hits MAX_HEALTH
        */ 
        health = health >= MAX_HEALTH ? health : health++;
        print("INCREMENTING HEALTH: HEALTH IS " + health);

        healthCollectibleUIManager.UpdateHealthCollectibleText(health);
    }

    public void decrementHealth() {
        health--;
        print("This is health: " + health);
        if (health <= 0) {
            print("GAME RESTART");

            // restart the game
            RestartGame.Restart();

            // end the game
            // UnityEditor.EditorApplication.isPlaying = false;
            // Application.Quit();
        }

        healthCollectibleUIManager.UpdateHealthCollectibleText(health);
    }

    public void collectSprintPowerup() {
        sprintPowerupsLeft++;
        powerupsUIManager.updateSprintPowerupsText(sprintPowerupsLeft);
    }

    private void toggleSprintPowerup() {
        isUsingSprintPowerup = !isUsingSprintPowerup;
    }

    private IEnumerator enhanceSprintAbility() {
        /*
        When the button "R" is pressed, the powerup fires to 
            1. Restore sprint energy to full
            2. Double sprint speed (2x) for 3 seconds.
        
        Afterwards, sprint speed is set back to 1.15x
        */
        isUsingSprintPowerup = true;

        // Instant boost of sprint energy
        sprintEnergy = maxSprintEnergy;

        // temporarily bump up settings
        sprintSpeed = powerupSprintSpeed;
        sprintAnimSpeed = powerupSprintAnimSpeed;

        yield return new WaitForSeconds(POWERUP_SPRINT_DURATION);

        // restore settings
        sprintSpeed = defaultSprintSpeed;
        sprintAnimSpeed = defaultSprintAnimSpeed;
        updatePlayerControllerSpeeds(sprintSpeed, sprintAnimSpeed);
        print("Settings restored");

        isUsingSprintPowerup = false;
    }

    public void useSprintPowerup() {
        // if there are no sprint power ups left,
        // or if the character is currently using a sprint powerup
        // then do not activate the sprint powerup.
        if (sprintPowerupsLeft <= 0 || isUsingSprintPowerup) return;

        sprintPowerupsLeft--;
        powerupsUIManager.updateSprintPowerupsText(sprintPowerupsLeft);
        toggleSprintPowerup();

        StartCoroutine(enhanceSprintAbility());
        // get the rigidbody and assign the speed of which it travels for 30 seconds
    }

    private bool canSprint() {
        return sprintEnergy > 0;
    }

    private IEnumerator AlterEnergyOverTime(float energyDelta)
    {
        // this discharges or charges sprint energy over time. 
        while (0 <= sprintEnergy && sprintEnergy <= maxSprintEnergy)
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
        sprintEnergy = Mathf.Clamp(sprintEnergy, 0, maxSprintEnergy); // Ensure sprintEnergy stays within 0 and 100
        print("Coroutine is stopped. This is sprintEnergy " + sprintEnergy);
        if (!canSprint()) {
            stopSprint();
        }
    }

    private void changeSprintEnergy(float energyDelta)
    {
        if (alterEnergyCoroutine != null)
        {
            print("Stopping the old sprint energy coroutine");
            StopCoroutine(alterEnergyCoroutine);
        }
        print("Starting sprint energy coroutine");
        alterEnergyCoroutine = StartCoroutine(AlterEnergyOverTime(energyDelta));
    }

    public void startSprint() {
        if (canSprint()) {
            // sprintSpeed and sprintAnimSpeed values can change depending on whether or not there is a powerup.
            updatePlayerControllerSpeeds(sprintSpeed, sprintAnimSpeed);
            changeSprintEnergy(SPRINT_DISCHARGE_RATE); // discharge sprint energy
        }
    }

    public void stopSprint() {
        updatePlayerControllerSpeeds(regularMovingSpeed, regularAnimSpeed);
        changeSprintEnergy(SPRINT_RECHARGE_RATE); // recharge sprint energy
    }

    private void updatePlayerControllerSpeeds(float moveSpeed, float moveAnimSpeed) {
        playerController.moveSpeed = moveSpeed;
        playerController.moveAnimSpeed = moveAnimSpeed;
    }

}
