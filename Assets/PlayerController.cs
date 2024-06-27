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

    }

    // void OnAnimatorMove()
    // {
    //     Debug.Log("Entering OnAnimatorMove");
    //     Debug.Log(this.transform.position);
    //     float speed = 1f;
    //     Vector3 newPosition = anim.rootPosition;
    //     newPosition = Vector3.LerpUnclamped(this.transform.position, newPosition, speed);
    //     astronautRigidBody.MovePosition(newPosition);
    //     Debug.Log(newPosition);
    //     Debug.Log("Leaving OnAnimatorMove");

    // }


}