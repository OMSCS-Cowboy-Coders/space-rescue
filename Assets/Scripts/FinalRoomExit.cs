using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoomExit : MonoBehaviour
{
    public Animator doorAnimator;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player triggered door.");
        // Trigger the door to close
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the room.");
            doorAnimator.SetBool("OpenFinalDoor", true);
            doorAnimator.SetBool("OpenFinalBattery", true);
        }
    }
}
