using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody astronautRigidBody;

    private Animator anim;

    private float astronautX;
    private float astronautY;

    private float astronautZ;

    private Vector3 newLocation;
    // Start is called before the first frame update
    void Start()
    {
        astronautRigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void OnMove(InputValue inputValue) {
        Debug.Log("Entered On Move");
        Vector2 astronautMovement = inputValue.Get<Vector2>();

        astronautX = astronautMovement.x;
        astronautY = astronautMovement.y;
        Debug.Log("astronautX: " + astronautX + "astronautY: " + astronautY + "astronautZ: " + astronautZ);
    }

    void FixedUpdate() {
         anim.SetFloat("Y_movement", astronautY);

        newLocation.Set(astronautX, 0f, astronautY);
        newLocation.Normalize ();

    }

    void OnAnimatorMove()
    {
        astronautRigidBody.MovePosition(astronautRigidBody.position + newLocation * anim.deltaPosition.magnitude*2);
    }


}