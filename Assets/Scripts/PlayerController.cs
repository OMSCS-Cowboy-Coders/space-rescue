using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    private Rigidbody astronautRigidBody;

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

    public int groundContactCount;

    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    public GameObject itemContainerForPlayer;


      public bool retrievedAllParts = false;
      public int numPartsRecieved = 0;


    private FootstepsController footstepsController;

    // Start is called before the first frame update
    void Start()
    {
        astronautRigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

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
            // pause the game
            // Show Win text
            Debug.Log("You've won!");

        }

        if (Mathf.Abs(astronautX) > 0.1f || Mathf.Abs(astronautY) > 0.1f)
        {
            footstepsController.PlayFootstepSound();
        }
    }

    public float turnRate = 100f;
    void OnAnimatorMove()
    {
        Vector3 newRootPosition;
    
        newRootPosition = Vector3.LerpUnclamped(astronautRigidBody.transform.position, anim.rootPosition, moveSpeed);
        astronautRigidBody.MovePosition(newRootPosition);

        var rot = Quaternion.AngleAxis(turnRate * astronautX * Time.deltaTime, Vector3.up);
        astronautRigidBody.MoveRotation(astronautRigidBody.rotation * rot);

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
        }
    }

     void OnAnimatorIK(int layerIndex) {
        float itemWeight = 1.0f;
        if(anim) {
            
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0);
         
            if(astate.IsName("picking_up")) {
                Debug.Log("trying to pick up");
                itemWeight = anim.GetFloat("ItemWeight");
                // Set the target position, if one has been assigned
                if(objectToCarry != null) {
                    anim.SetLookAtWeight(itemWeight);
                    anim.SetLookAtPosition(objectToCarry.transform.position);
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand,itemWeight);
                    anim.SetIKPosition(AvatarIKGoal.RightHand, objectToCarry.transform.position);
                    Debug.Log("finished setting up things");
                }
            } else {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                anim.SetLookAtWeight(0);
            }
        }
    }

    public void PickUpItem() {
        Debug.Log("In pick up item");
        
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
        if (c.transform.gameObject.tag == "MarsFloor")
        { 
            ++groundContactCount;
        } else if (c.transform.gameObject.tag == "SpaceshipPart") {

        }
        astronautRigidBody.velocity = Vector3.zero;
    }

     private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.gameObject.tag == "MarsFloor")
        {
            --groundContactCount;
        }

    }

    public void updateNumBatteriesRetrieved() {
        numPartsRecieved = numPartsRecieved + 1;
    }

}