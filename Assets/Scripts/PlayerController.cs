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

    // TODO: implement this so that the player can pick stuff up. (only one at a time)
    private bool isHoldingObject;

    private Vector3 newLocation;

    private Quaternion newRotation = Quaternion.identity;

     Vector3 m_EulerAngleVelocity;

    private float characterTurnSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        astronautRigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void OnMove(InputValue inputValue) {
        Vector2 astronautMovement = inputValue.Get<Vector2>();

        astronautX = astronautMovement.x;
        astronautY = astronautMovement.y;
    }

    void FixedUpdate() {
        // anim.SetFloat("X_movement", astronautX);
        anim.SetFloat("Y_movement", astronautY);

        newLocation.Set(astronautX, 0f, astronautY);
        newLocation.Normalize();
    }

    public float turnRate = 100f;
    void OnAnimatorMove()
    {
        Vector3 newRootPosition = Vector3.LerpUnclamped(astronautRigidBody.transform.position, anim.rootPosition, 20f);
        astronautRigidBody.MovePosition(newRootPosition);
        
        var rot = Quaternion.AngleAxis(turnRate * astronautX * Time.deltaTime, Vector3.up);
        astronautRigidBody.MoveRotation(astronautRigidBody.rotation * rot);
    }


}