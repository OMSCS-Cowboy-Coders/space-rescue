using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    private Rigidbody astronautRigidBody;

    private Collider astronautCollider;

    public Animator anim;

    private float astronautX;
    private float astronautY;

    private float astronautZ;

    // TODO: implement this so that the player can pick stuff up. (only one at a time)
    private bool isHoldingObject;

    private Vector3 newLocation;

    public float moveSpeed;
    public float moveAnimSpeed;

    private PlayerMetrics playerMetrics;

    private bool playerIsTryingToPickUpObject;

    public GameObject objectToCarry;



    public GameObject itemContainerForPlayer;


    public bool retrievedAllParts = false;
    public int numPartsRecieved = 0;

    public float fasterFallGravityForJump = 2f;


    private FootstepsController footstepsController;

     public GameObject WinTextPanel;


    private bool onGround;
             float jumpHeight = 5f;
             RaycastHit boxHit;
    public GenerateEnemies generateEnemies;

    public bool onIce = false;

    public GameObject finalDoor;

    public GameObject finalFinalDoor;

    public GameObject batteryStorage;

    private bool doorNotOpened =false;

    public bool reachedFinalBattery = false;

    private bool playerIsJumping;
    private bool playerIsOnGround; 
    private bool playerIsAtPeak;
    private bool playerIsFalling;

    private float speedWhileJumping;

    private Quaternion rotationDirection;

    private bool isPlayerMoving; 

    private void findGenerateEnemies() {
        // hardcode GameObject name so that it can handle delayed initialization of the Terrain.
        // this method is called at Start and before usage in updateNumBatteriesRetrieved
        GameObject terrainObject = GameObject.Find("Terrain");
        if (terrainObject != null && generateEnemies == null)
        {
            generateEnemies = terrainObject.GetComponent<GenerateEnemies>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {        
        print("Starting playerController");
        // WinTextPanel.SetActive(false);
        astronautRigidBody = GetComponent<Rigidbody>();
        astronautCollider = GetComponent<Collider>();
        anim = GetComponent<Animator>();

        Debug.Log("collider: " + astronautCollider);

        playerMetrics = gameObject.GetComponent<PlayerMetrics>();

        // set initial values
        moveSpeed       = playerMetrics.getMoveSpeed();
        moveAnimSpeed   = playerMetrics.getMoveAnimSpeed();
        footstepsController = GetComponent<FootstepsController>();
        
        generateEnemies = null;
        findGenerateEnemies();
    }

    void OnMove(InputValue inputValue) {
        Vector2 astronautMovement = inputValue.Get<Vector2>();
        astronautX = astronautMovement.x;
        astronautY = astronautMovement.y;

    }

   
    void FixedUpdate() {
        anim.SetFloat("Y_movement", astronautY);

        

        Debug.Log("Num Parts Recieved : " + batteryStorage.transform.childCount);
        Debug.Log("door not opened: " + doorNotOpened);

        if(batteryStorage.transform.childCount == 3 && !doorNotOpened) {
            Debug.Log("Door to final challenge opens!");
            // WinTextPanel.SetActive(true);
            Animator animDoor = finalDoor.GetComponent<Animator>();
            Animator animFinalFinalDoor = finalFinalDoor.GetComponent<Animator>();
            Debug.Log("Anim Final door: " + animFinalFinalDoor);
            animDoor.SetBool("collectedAllBatteries", true);
            animFinalFinalDoor.SetBool("collectedAllBatteries", true);
            doorNotOpened = true;
        }

        if (Mathf.Abs(astronautX) > 0.1f || Mathf.Abs(astronautY) > 0.1f)
        {
            footstepsController.PlayFootstepSound();
        }
    }

    public float turnRate = 100f;
    void OnAnimatorMove()
    {

        if(playerIsOnGround) {
            
            if(onIce) {
                moveSpeed = 4f;
            }
            AnimatorClipInfo[] animatorClipInfo = anim.GetCurrentAnimatorClipInfo(0);
            String currentAnimationPlaying = animatorClipInfo[0].clip.name;
        
            float movementSpeed; 
    
        
            Vector3 newRootPosition;
            Vector3 testPostion; 

            movementSpeed = anim.GetFloat("Walkspeed") > 0 ? anim.GetFloat("Walkspeed") : anim.GetFloat("Runspeed");
            
            var rot = Quaternion.AngleAxis(turnRate * astronautX * Time.deltaTime, Vector3.up);
            rotationDirection = astronautRigidBody.rotation * rot;

            astronautRigidBody.MoveRotation(astronautRigidBody.rotation * rot);
            
            testPostion = astronautRigidBody.position + movementSpeed*Time.deltaTime*transform.forward;
            newRootPosition = Vector3.LerpUnclamped(astronautRigidBody.transform.position, testPostion, moveSpeed);
            astronautRigidBody.MovePosition(newRootPosition);

            anim.SetFloat("SprintAnimSpeed", moveAnimSpeed);
        }
    }

    void Update() {
        isAstronautOnTheGround();
        InputDetector();
        Debug.Log("Astronaut x: " + astronautX);
        if(astronautY > 0) {
            Debug.Log("Astronaut x: " + astronautX);
            anim.SetBool("isMoving", true);
        } else if (!isPlayerMoving) {
            anim.SetBool("isMoving", false);
        }
        if(astronautRigidBody.velocity.y < 0 && playerIsJumping) { // Astronaut is falling after jump
            playerIsJumping = false;
            playerIsAtPeak = false;
            playerIsFalling = true;
            anim.SetBool("startedJump", false);
            anim.SetBool("fallingJump", true);
            astronautRigidBody.velocity = astronautRigidBody.velocity + (Vector3.up * Physics.gravity.y* (fasterFallGravityForJump) *Time.deltaTime);
        }
        Debug.Log("Is the player on the ground: " + playerIsOnGround);
        if (playerIsOnGround) {
            anim.SetBool("onGround", true);
            anim.SetBool("fallingJump", false);
        } 

        if(playerIsJumping) {
            anim.SetBool("onGround", false);
            astronautRigidBody.AddForce(astronautRigidBody.transform.forward *astronautY*2, ForceMode.Impulse);
        }

            // Vector3 velocity = astronautRigidBody.transform.forward * anim.GetFloat("Runspeed")* 3f;
            // velocity.y = astronautRigidBody.velocity.y;
            
            //astronautRigidBody.transform.forward * anim.GetFloat("Runspeed") * 3f; 
            // float movementSpeed; 
    
            // Vector3 newRootPosition;
            // Vector3 testPostion; 
            // movementSpeed = anim.GetFloat("Walkspeed") > 0 ? anim.GetFloat("Walkspeed") : anim.GetFloat("Runspeed");
            
            // var rot = Quaternion.AngleAxis(turnRate * astronautX * Time.deltaTime, Vector3.up);
            // rotationDirection = astronautRigidBody.rotation * rot;
            
            // testPostion = astronautRigidBody.position + movementSpeed*Time.deltaTime*transform.forward;
            // newRootPosition = Vector3.LerpUnclamped(astronautRigidBody.transform.position, testPostion, moveSpeed);
            // astronautRigidBody.MovePosition(newRootPosition);
            
        
    }

    void InputDetector() {

        if(Input.GetKeyUp(KeyCode.UpArrow)) {
            isPlayerMoving = false; 
        }        

        if (Input.GetKeyDown(KeyCode.LeftShift) && !onIce) {
            playerMetrics.startSprint();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            playerMetrics.stopSprint();
        }
        else if (Input.GetKeyDown(KeyCode.R) && !onIce) {
            print("Using Powerup!");
            playerMetrics.useSprintPowerup();
        } else if (Input.GetKeyDown(KeyCode.Space) && playerIsOnGround && !onIce) {
            playerJumps();
        }   else if (Input.GetKeyDown(KeyCode.Y)) {
            print("Bumping up the aliens");
            updateNumBatteriesRetrieved();
        }
    }

    void playerJumps() {
        playerIsJumping = true;
        anim.SetBool("startedJump", true);
        Debug.Log("Trying to jump : " + playerIsJumping);
        float jumpHeight = 5f;
        float jumpForceBasedOnHeight = Mathf.Sqrt(jumpHeight * Physics.gravity.y * -2) * astronautRigidBody.mass;
        astronautRigidBody.AddForce(Vector2.up * jumpForceBasedOnHeight, ForceMode.Impulse);
    }

    bool isAstronautOnTheGround() {
        float maxDistanceOfBox = 1;
        Debug.Log("Rigid body: " + astronautRigidBody);

        // bool onGround = Physics.BoxCast(astronautRigidBody.transform.position, 
        // , -astronautRigidBody.transform.up, 
        // astronautRigidBody.transform.rotation, maxDistanceOfBox);
        
        onGround = Physics.BoxCast(astronautCollider.bounds.center, astronautRigidBody.transform.localScale*.25f
        , -astronautRigidBody.transform.up, out boxHit,
        astronautRigidBody.transform.rotation, maxDistanceOfBox);
        Debug.Log("On the ground: " + onGround);
        if (onGround) {
            playerIsOnGround = true;
            return true;
        }
        Debug.Log("player is not on the ground!");
        playerIsOnGround = false;
        return false;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Debug.Log("drawing gizmos: " + onGround);
        
        if(onGround) {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(astronautRigidBody.transform.position, -astronautRigidBody.transform.up * boxHit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(astronautRigidBody.transform.position + -astronautRigidBody.transform.up * boxHit.distance, astronautRigidBody.transform.localScale);
        } else {
            
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(astronautRigidBody.transform.position, -astronautRigidBody.transform.up * jumpHeight);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + -astronautRigidBody.transform.up * jumpHeight, astronautRigidBody.transform.localScale);
        }
    }

     void OnAnimatorIK(int layerIndex) {
        float itemWeight = 1.0f;
        if(anim) {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0);
         
            // if(astate.IsName("picking_up")) {
            //     Debug.Log("trying to pick up");
            //     itemWeight = anim.GetFloat("ItemWeight");
            //     // Set the target position, if one has been assigned
            //     if(objectToCarry != null) {
            //         anim.SetLookAtWeight(itemWeight);
            //         anim.SetLookAtPosition(objectToCarry.transform.position);
            //         anim.SetIKPositionWeight(AvatarIKGoal.RightHand,itemWeight);
            //         anim.SetIKPosition(AvatarIKGoal.RightHand, objectToCarry.transform.position);
            //         Debug.Log("finished setting up things");
            //     }
            // } else {
            //     anim.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
            //     anim.SetLookAtWeight(0);
            // }
        }
    }

    public void PickUpItem() {
        Debug.Log("In pick up item");
        Debug.Log(objectToCarry);
        
        objectToCarry.transform.SetParent(itemContainerForPlayer.transform);

        PickUpObjects pickUpObjects = objectToCarry.GetComponent<PickUpObjects>();

        // Collider c = objectToCarry.GetComponent<Collider>();
        // Rigidbody rb = objectToCarry.GetComponent<Rigidbody>();
       
        pickUpObjects.setVarsForPickUp();
    
        // objectToCarry.transform.localPosition = Vector3.zero;
        // objectToCarry.transform.localRotation = Quaternion.Euler(Vector3.zero);
        // this.transform.localScale = Vector3.one;

        // rb.isKinematic = true;
        // c.isTrigger = true;
    }

    public void PutDownItem() {
         Debug.Log("In pick up item");
         
        objectToCarry.transform.SetParent(itemContainerForPlayer.transform);

        PickUpObjects pickUpObjects = objectToCarry.GetComponent<PickUpObjects>();

        Collider c = objectToCarry.GetComponent<Collider>();
        Rigidbody rb = objectToCarry.GetComponent<Rigidbody>();
       
    
        objectToCarry.transform.SetParent(null);
        objectToCarry.transform.SetParent(GameObject.FindWithTag("BatteryStorage").transform);
        // this.transform.localScale = Vector3.one;      
    }

    public void setPickUpAnimToFalse() {
        anim.SetBool("PickingUpObject", false);
        anim.SetBool("CarryingObject", true);
    }

    void OnCollisionEnter(Collision c) {
        Debug.Log("entering this collision: " + c.transform.gameObject.tag);
         if(c.transform.gameObject.tag == "Ice") {
            Debug.Log("trying to slide");
            astronautCollider.material.staticFriction = 0;
            astronautCollider.material.dynamicFriction = 0;
            
            astronautRigidBody.velocity = new Vector3(-1.5f * astronautRigidBody.transform.forward.x*12, 0, -1.5f * astronautRigidBody.transform.forward.z*12);
            onIce = true; 
        } else if (c.transform.gameObject.tag == "Lift") {
            astronautRigidBody.isKinematic = true;
        }   else if (c.transform.gameObject.tag == "SpaceshipPart") {
            astronautRigidBody.velocity = new Vector3(0,0,0);
        } else if (c.transform.gameObject.tag == "final_win_battery") {
            reachedFinalBattery = true;
            Debug.Log("setting the win: " + reachedFinalBattery);
        }
    }

    private void OnCollisionExit(Collision c)
    {
         if(c.transform.gameObject.tag == "Ice") {
            Debug.Log("trying to not slide");
            astronautCollider.material.staticFriction = 0.6f;
            astronautCollider.material.dynamicFriction = 0.6f;
            astronautRigidBody.velocity = new Vector3(1, 0, 1);
            astronautRigidBody.drag = 0f;
            moveSpeed = 10.5f;
            onIce = false;
             
        } 

    }

    public void updateNumBatteriesRetrieved() {
        Debug.Log("Updating battery count!!");
        numPartsRecieved = numPartsRecieved + 1;
        Debug.Log("num parts in func " + numPartsRecieved);

        // the game progressively gets harder when this number is updated
        findGenerateEnemies();
        generateEnemies.addMoreAliens();
    }

}