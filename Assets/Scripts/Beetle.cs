using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleBehavior : MonoBehaviour
{
    public float minJumpForce = 5f;
    public float maxJumpForce = 10f;
    public float minJumpInterval = 1f;
    public float maxJumpInterval = 3f;
    public float minTorque = -10f;
    public float maxTorque = 10f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private float nextJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNextJumpTime();
    }

    void FixedUpdate()
    {
        if (isGrounded && Time.time >= nextJumpTime)
        {
            Jump();
            SetNextJumpTime();
        }
    }

    private void Jump()
    {
        // Apply a random upward force
        float jumpForce = Random.Range(minJumpForce, maxJumpForce);
        Vector3 force = new Vector3(0, jumpForce, 0);
        rb.AddForce(force, ForceMode.Impulse);

        // Apply random torque
        float torqueX = Random.Range(minTorque, maxTorque);
        float torqueY = Random.Range(minTorque, maxTorque);
        float torqueZ = Random.Range(minTorque, maxTorque);
        Vector3 torque = new Vector3(torqueX, torqueY, torqueZ);
        rb.AddTorque(torque, ForceMode.Impulse);
    }

    private void SetNextJumpTime()
    {
        nextJumpTime = Time.time + Random.Range(minJumpInterval, maxJumpInterval);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = false;
        }
    }
}