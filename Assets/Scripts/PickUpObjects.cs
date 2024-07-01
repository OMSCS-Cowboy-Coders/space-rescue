using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PickUpObjects : MonoBehaviour
{

    public PlayerController playerController;
    private Rigidbody itemRB;

    private Collider itemCollider;

    public GameObject player, itemContainerForPlayer;

    public bool itemIsBeingHeldByPlayer;

    public static bool playerIsHoldingItemGlobal;

    private bool playerIsTryingToPickUpObject;

    public float farthestDistanceFromItem = 2f;

    private bool pickUpItem = false; 

  




    // Start is called before the first frame update
    void Start()
    {
        itemRB = this.GetComponent<Rigidbody>();
        itemCollider = this.GetComponent<Collider>();

        if(itemRB != null && itemCollider != null) {
            if(!itemIsBeingHeldByPlayer) {
                itemRB.isKinematic = false;
                itemCollider.isTrigger = false;
            } else {
                itemRB.isKinematic = false;
                itemCollider.isTrigger = false;
                playerIsHoldingItemGlobal = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        float distanceFromItem = 0f;
        distanceFromItem = Vector3.Distance(player.transform.position, this.transform.position);

         if (!itemIsBeingHeldByPlayer &&
         Input.GetKeyDown(KeyCode.E) &&
          !playerIsHoldingItemGlobal &&
          distanceFromItem <= farthestDistanceFromItem) {
            Debug.Log("In this call to pickup the object");
            playerIsTryingToPickUpObject = true;
            playerController.anim.SetBool("PickingUpObject", true);
            playerController.anim.SetBool("PuttingDownObject", false);
            playerController.objectToCarry = gameObject;
            
        } else if(itemIsBeingHeldByPlayer && Input.GetKeyDown(KeyCode.Q)) {
            playerIsTryingToPickUpObject = false;
            playerController.anim.SetBool("PuttingDownObject", true);
            playerController.anim.SetBool("CarryingObject", false);
            PutDownObject();
        }

        if(itemIsBeingHeldByPlayer) {
         itemRB.velocity = player.GetComponent<Rigidbody>().velocity;
        }
    }

    public void setVarsForPickUp() {
        Debug.Log("In pick up item");
       
        itemIsBeingHeldByPlayer = true;
        playerIsHoldingItemGlobal = true;

        this.transform.SetParent(itemContainerForPlayer.transform);
        // itemContainerForPlayer.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);
        // this.transform.localScale = Vector3.one;

        itemRB.isKinematic = true;
        itemCollider.isTrigger = true;
    }

    public void PutDownObject() {
        itemIsBeingHeldByPlayer = false;
        playerIsHoldingItemGlobal = false;

        this.transform.SetParent(null);
        
        itemRB.velocity = player.GetComponent<Rigidbody>().velocity;

        itemRB.isKinematic = false;
        itemCollider.isTrigger = false;

        itemContainerForPlayer.transform.localScale = Vector3.one;
        this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        
        GameObject batteryStorage = GameObject.FindWithTag("BatteryStorage");
        float closeToBatteryStorage = Vector3.Distance(this.transform.position, batteryStorage.transform.position);
        if (closeToBatteryStorage < 2f) {
            this.transform.SetParent(GameObject.FindWithTag("BatteryStorage").transform);
            playerController.updateNumBatteriesRetrieved();
        }
    }

}

