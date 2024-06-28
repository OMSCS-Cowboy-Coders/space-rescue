using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChestController : MonoBehaviour
{
    public Animator animator; 

    private void OnTriggerEnter(Collider c)
    {
        print("Hit the collider");
        OpenChest();
    }

    private void OnTriggerExit(Collider c)
    {
        print("Exited the collider");
        CloseChest();
    }

    private void OpenChest()
    {
        animator.SetTrigger("StartElevator");
    }

    private void CloseChest()
    {
        animator.SetTrigger("StopElevator");
    }
}