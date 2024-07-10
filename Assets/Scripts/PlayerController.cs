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


    private FootstepsController footstepsController;

     public GameObject WinTextPanel;

     private Vector2 currentInputVectorForMovement;
     private Vector2 smoothingVelocity;
     private float smoothingInputSpeed = 0.5f;

     private float forceAppliedForJump = 100f;

     private float astronautDistanceFromGround;

    private bool onGround;
             float jumpHeight = 5f;
             RaycastHit boxHit;
    // Start is called before the first frame update
    void Start()
    {        
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
    }

    void OnMove(InputValue inputValue) {
        Vector2 astronautMovement = inputValue.Get<Vector2>();
        astronautX = astronautMovement.x;
        astronautY = astronautMovement.y;
    }

   
    void FixedUpdate() {
        anim.SetFloat("Y_movement", astronautY);

        newLocation.Set(astronautX, 0f, astronautY);
        newLocation.Normalize();

        if(numPartsRecieved == 5) {
            Debug.Log("You won!");
            WinTextPanel.SetActive(true);
        }

        if (Mathf.Abs(astronautX) > 0.1f || Mathf.Abs(astronautY) > 0.1f)
        {
            footstepsController.PlayFootstepSound();
        }
    }

    public float turnRate = 100f;
    void OnAnimatorMove()
    {

        AnimatorClipInfo[] animatorClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        String currentAnimationPlaying = animatorClipInfo[0].clip.name;
    
        float movementSpeed; 
 
       
        Vector3 newRootPosition;
        Vector3 testPostion; 

        movementSpeed = anim.GetFloat("Walkspeed") > 0 ? anim.GetFloat("Walkspeed") : anim.GetFloat("Runspeed");
        
        var rot = Quaternion.AngleAxis(turnRate * astronautX * Time.deltaTime, Vector3.up);
        astronautRigidBody.MoveRotation(astronautRigidBody.rotation * rot);
        
        testPostion = astronautRigidBody.position + movementSpeed*Time.deltaTime*transform.forward;
        newRootPosition = Vector3.LerpUnclamped(astronautRigidBody.transform.position, testPostion, moveSpeed);
        astronautRigidBody.MovePosition(newRootPosition);
        

        anim.SetFloat("SprintAnimSpeed", moveAnimSpeed);
    }

    void Update() {
        InputDetector();
    }

    void InputDetector() {

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            playerMetrics.startSprint();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            playerMetrics.stopSprint();
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            print("Using Powerup!");
            playerMetrics.useSprintPowerup();
        } else if (Input.GetKeyDown(KeyCode.Space) && isAstronautOnTheGround()) {
            playerJumps();
        }
    }

    void playerJumps() {
        Debug.Log("Trying to jump");
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
        
        onGround = Physics.BoxCast(astronautCollider.bounds.center, astronautRigidBody.transform.localScale*.5f
        , -astronautRigidBody.transform.up, out boxHit,
        astronautRigidBody.transform.rotation, maxDistanceOfBox);
        Debug.Log("On the ground: " + onGround);
        if (onGround) {
            return true;
        }
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
            {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(astronautRigidBody.transform.position, -astronautRigidBody.transform.up * jumpHeight);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + -astronautRigidBody.transform.up * jumpHeight, astronautRigidBody.transform.localScale);
        }
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
        Debug.Log("Tag: " + c.transform.gameObject.tag);
        astronautRigidBody.velocity = Vector3.zero;
    }

     private void OnCollisionExit(Collision collision)
    {

    }

    public void updateNumBatteriesRetrieved() {
        numPartsRecieved = numPartsRecieved + 1;
    }

}