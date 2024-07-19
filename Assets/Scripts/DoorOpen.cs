using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator doorAnim;

    public GameObject batteryStorage;

    public int numTotal;

    void Start() {
        doorAnim = GetComponent<Animator>();
    }

    void Update() {
        if(batteryStorage.transform.childCount == numTotal) {
            doorAnim.SetBool("collectedBattery", true);
        }
    }
}
