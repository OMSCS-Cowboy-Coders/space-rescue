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

    private bool canBePickedUp = true;

  




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
          distanceFromItem <= farthestDistanceFromItem && canBePickedUp) {
            Debug.Log("In this call to pickup the object: " + gameObject);
            playerIsTryingToPickUpObject = true;
            // playerController.anim.SetBool("PickingUpObject", true);
            // playerController.anim.SetBool("PuttingDownObject", false);
            playerController.objectToCarry = gameObject;
            playerController.PickUpItem();
            Debug.Log(playerController.objectToCarry);
            Debug.Log("picked up object");
            
        } else if(itemIsBeingHeldByPlayer && Input.GetKeyDown(KeyCode.Q)) {
            playerIsTryingToPickUpObject = false;
            // playerController.anim.SetBool("PuttingDownObject", true);
            // playerController.anim.SetBool("CarryingObject", false);
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
        itemContainerForPlayer.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        Vector3 rotObect = new Vector3(-90, 0, 0);
        itemContainerForPlayer.transform.localRotation = Quaternion.Euler(rotObect);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);
        this.transform.localScale = Vector3.one;

        itemRB.isKinematic = true;
        itemCollider.isTrigger = true;
    }

    public void PutDownObject() {
        itemIsBeingHeldByPlayer = false;
        playerIsHoldingItemGlobal = false;

        this.transform.SetParent(null);

        
        // itemRB.velocity = player.GetComponent<Rigidbody>().velocity;   

        itemContainerForPlayer.transform.localScale = Vector3.one;
        this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Debug.Log("Player forward: " + player.transform.forward);
        Debug.Log("player postion: " + player.transform.position);
        float multiplier = player.transform.forward.z > 0 ? 1 : -1;
        this.transform.position = player.transform.position + new Vector3(0,0, .5f * multiplier);
        

         itemRB.isKinematic = false;
        itemCollider.isTrigger = false;
        
        GameObject batteryStorage = GameObject.FindWithTag("BatteryStorage");
        float closeToBatteryStorage = Vector3.Distance(this.transform.position, batteryStorage.transform.position);
        if (closeToBatteryStorage < 4f) {
            this.transform.SetParent(GameObject.FindWithTag("BatteryStorage").transform);
            canBePickedUp = false;
            itemRB.isKinematic = true;
            playerController.updateNumBatteriesRetrieved();
        }
        Debug.Log("Object postion 2: " + this.transform.position);
    }

    void OnCollisionEnter(Collision c) {
        itemRB.velocity = Vector3.zero;
    }
    void OnCollisionExit(Collision c) {
        itemRB.velocity = Vector3.zero;
        c.rigidbody.velocity = Vector3.zero;
    }


}

