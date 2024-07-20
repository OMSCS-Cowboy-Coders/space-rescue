using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoomEntry : MonoBehaviour
{
    public Animator doorAnimator;   

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player triggered door."); 
        // Trigger the door to close after entry 
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the final boss room.");
            doorAnimator.SetBool("CloseDoorAfterEntry", true);
        }
    }

}
