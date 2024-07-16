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
        if (other.CompareTag("Player") && !isActivated)
        {
            TogglePlatform();
            isActivated = true;
            switchAnimator.SetTrigger("SwitchButtonPress");
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
