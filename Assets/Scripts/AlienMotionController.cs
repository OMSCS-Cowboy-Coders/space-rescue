using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMotionController : MonoBehaviour
{
    // Start is called before the first frame update
    public float AlienSpeed;
    public float AlienAngularSpeed;
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        AlienSpeed = rb.velocity.magnitude;
        AlienAngularSpeed = rb.angularVelocity.magnitude;
    }
}
