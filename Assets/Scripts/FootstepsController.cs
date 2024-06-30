using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsController : MonoBehaviour
{
    public AudioClip footstepClip;
    public AudioSource footstepSource;

    void Start()
    {
        footstepSource = GetComponent<AudioSource>();
        footstepSource.clip = footstepClip;
        footstepSource.playOnAwake = false;
    }

    // Call this method to play footstep sound when player moves
    public void PlayFootstepSound()
    {
        if (!footstepSource.isPlaying && footstepSource.enabled)
        {
            footstepSource.PlayOneShot(footstepClip);
        }
    }


    public void Update()
    {
        // Movement logic 
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            PlayFootstepSound();
        }
    }
}
