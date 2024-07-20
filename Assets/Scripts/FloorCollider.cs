using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    private Animator doorAnim;

    public GameObject doorAttachedTrigger;

    void Start()
    {
        doorAnim = doorAttachedTrigger.GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player triggers door" + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player opens the door.");
            doorAnim.SetBool("triggerDoor", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Player exits door" + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exits the door.");
            doorAnim.SetBool("triggerDoor", false);
        }
    }

}
