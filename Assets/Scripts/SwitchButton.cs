using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    public MovingPlatform platform;
    private Animator switchAnimator;
    private bool isActivated = false;

    void Start()
    {
        switchAnimator = GetComponent<Animator>();
    }

    // Triggers the button press animation
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Button has been triggered");
        if (other.CompareTag("Player") && !isActivated)
        {
            Debug.Log("Player has entered and activated switch");
            TogglePlatform();
            switchAnimator = this.transform.gameObject.GetComponent<Animator>();
            switchAnimator.SetBool("isActivated", true);
            switchAnimator.SetTrigger("SwitchButtonPress");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = false;
        }
    }

    void TogglePlatform()
    {
        if (platform.isFrozen)
        {
            platform.UnfreezePlatform();
        }
        else
        {
            platform.FreezePlatform();
        }
    }

}
