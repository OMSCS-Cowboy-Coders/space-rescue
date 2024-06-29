using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectibleManager : MonoBehaviour
/*
This class is a general class that can be used for any type of collectible received.
*/
{
    void OnTriggerEnter(Collider c) {
        print("Entered ONTRIGGERENTER");
        if (c.GetComponent<Rigidbody>() == null) return;

        PlayerMetrics pm = c.GetComponent<RigidBody>().gameObject.GetComponent<PlayerMetrics>();
        if (pm == null) return;

        // Split logic based on collectible type

        // Health Collectible
        // Stamina Collectible
        // Recharge rate for sprint

        pm.ReceiveBall();
        

        // BallCollector bc = c.GetComponent<Rigidbody>().gameObject.GetComponent<BallCollector>();
        // EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);
        // Destroy(this.gameObject);
    }
}
