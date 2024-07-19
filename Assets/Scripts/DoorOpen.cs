using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator doorAnim;

    public GameObject batteryStorage;

    public int numTotal;

    public GameObject doorCollider; 

    void Start() {
        doorAnim = GetComponent<Animator>();
    }

    void Update() {
        if(batteryStorage.transform.childCount == numTotal) {
            doorAnim.SetBool("collectedBattery", true);

            Debug.Log("Player collides with door.");
            BoxCollider collider = doorCollider.GetComponent<BoxCollider>();
            if (collider != null)
            {
                collider.isTrigger = true;
                Debug.Log("Door collider is triggered.");
            }
        }
    }

}
