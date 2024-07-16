using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Animator animator;
    public bool isFrozen = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Pauses the animation of the platform 
    public void FreezePlatform()
    {
        isFrozen = true;
        animator.speed = 0; 
    }

    // Unpauses the animation of the platform 
    public void UnfreezePlatform()
    {
        isFrozen = false;
        animator.speed = 1; 
    }
}
