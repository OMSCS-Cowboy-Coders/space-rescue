using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateControl : MonoBehaviour
{
     private Animator anim;

     private Animator animPartCovering;

     public GameObject partCovering;

     public GameObject batteryPack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c) {
         if (c.attachedRigidbody != null && c.transform.gameObject.tag == "Player") {
            RetrievePart retrievePart = c.attachedRigidbody.gameObject.GetComponent<RetrievePart>(); 
            if (retrievePart != null) {
                anim =  this.transform.gameObject.GetComponent<Animator>();
                anim.SetBool("activated", true);
                animPartCovering = partCovering.GetComponent<Animator>();
                animPartCovering.SetBool("reveal", true);

                batteryPack.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}