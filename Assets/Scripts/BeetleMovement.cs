using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    public float minJumpWait = 1f;
    public float maxJumpWait = 4f;
    public float minTorque = -20f;
    public float maxTorque = 20f;
    public float minJumpForce = 3f;
    public float maxJumpForce = 12f;

    private float nextJumpTime;
    private bool grounded = true;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        nextJumpTime = Time.time + Random.Range(minJumpWait, maxJumpWait);
    }

    void FixedUpdate()
    {
        if (grounded && Time.time > nextJumpTime)
        {
            Jump();
            nextJumpTime = Time.time + Random.Range(minJumpWait, maxJumpWait);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            grounded = false;
        }
    }
    private void Jump()
    {
        float jumpForce = Random.Range(minJumpForce, maxJumpForce);
        Vector3 force = new Vector3(0, jumpForce, 0);

        float torqueX = Random.Range(minTorque, maxTorque);
        float torqueY = Random.Range(minTorque, maxTorque);
        float torqueZ = Random.Range(minTorque, maxTorque);
        Vector3 torque = new Vector3(torqueX, torqueY, torqueZ);
        
        rigidbody.AddForce(force, ForceMode.Impulse);
        rigidbody.AddTorque(torque, ForceMode.Impulse);
    }
}