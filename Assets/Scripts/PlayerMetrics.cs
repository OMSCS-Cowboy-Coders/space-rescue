using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetrics : MonoBehaviour
{
    // Start is called before the first frame update
    public static int MAX_HEALTH = 3;
    public static float POWERUP_SPRINT_DURATION = 3f;

    private float regularMovingSpeed = 10.5f;
    private float regularAnimSpeed = 1.5f;

    private float sprintSpeed = 15.75f; // initially set as default
    private float sprintAnimSpeed = 15.75f; // initially set as default

    private float defaultSprintSpeed = 15.75f;
    private float defaultSprintAnimSpeed = 15.75f;

    private float powerupSprintSpeed = 20.0f;
    private float powerupSprintAnimSpeed = 20.0f;

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

    public PanelMenu panelMenu;

    private float lastHitTime;
    private float lastRegenTime;
    private float currentTime;
    private void findPanelMenu() {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null && panelMenu == null)
        {
            panelMenu = canvas.GetComponent<PanelMenu>();
        }
    }


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
        // healthCollectibleUIManager.UpdateHealthCollectibleText(health);
        findPanelMenu();
        lastRegenTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        
        currentTime = Time.realtimeSinceStartup;
        regenHealth();
        
    }

    public float getMoveSpeed() {
        return regularMovingSpeed;
    }
    
    public float getMoveAnimSpeed() {
        return regularAnimSpeed;
    }

    private void regenHealth(){
        //Regen every 10 seconds
        if(currentTime - lastRegenTime > 10f){
            print("Regenerating Health! @: " + currentTime);
            incrementHealth();
            lastRegenTime = Time.realtimeSinceStartup;
        }
    }
    public void incrementHealth() {
        /*
        This function will increment health only til it hits MAX_HEALTH
        */ 
        health++;
        if (health >= MAX_HEALTH) {
            health = MAX_HEALTH;
        }

        if (healthCollectibleUIManager == null) {
            healthCollectibleUIManager = FindObjectOfType<HealthCollectibleUIManager>();
        }
        healthCollectibleUIManager.updateHealth(health);
    }

    public void decrementHealth(bool dieImmediately) {
        print("Last hit time:  " + currentTime);
        if(currentTime - lastHitTime > 5f){
            //If 5 seconds haven't clearly went by, decrement the player's health
            print("Hasn't been more than 5 seconds bro: " + (currentTime - lastHitTime));
            lastHitTime =  currentTime;
            return;
        }
        else{
            if(dieImmediately) {
                health = 0;
                healthCollectibleUIManager.updateHealth(health);
            } else {
                health--;
            }
            print("This is health: " + health);
            if (health <= 0) {
                findPanelMenu();
                panelMenu.showLoseScreen();
                // RestartGame.Restart();
                
                // end the game 
                // UnityEditor.EditorApplication.isPlaying = false;
                // Application.Quit();
            
            }
            healthCollectibleUIManager.updateHealth(health);
            lastHitTime =  currentTime;
        }
    }

    public float getSprintEnergy() {
        return sprintEnergy;
    }

    public void collectSprintPowerup() {
        sprintPowerupsLeft++;
        powerupsUIManager.updateSprintPowerups(sprintPowerupsLeft);
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
        powerupsUIManager.updateSprintPowerups(sprintPowerupsLeft);
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
            Debug.Log("Can sprint! " + sprintSpeed);
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
        Debug.Log("Sprint speed: " + moveSpeed);
        playerController.moveSpeed = moveSpeed;
        playerController.moveAnimSpeed = moveAnimSpeed;
    }

}
