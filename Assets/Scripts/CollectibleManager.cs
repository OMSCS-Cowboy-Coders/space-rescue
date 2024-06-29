using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectibleManager : MonoBehaviour
/*
This class is a general class that can be used for any type of collectible received.
*/
{
    void OnTriggerEnter(Collider c) {
        
        if (c.GetComponent<Rigidbody>() == null) return;

        PlayerMetrics pm = c.GetComponent<Rigidbody>().gameObject.GetComponent<PlayerMetrics>();
        if (pm == null) return;

        switch(tag) {
            case "HealthCollectible":
                print("Got the HealthCollectible Tag!");
                pm.incrementHealth();
                break;
            case "SprintPowerup":
                print("Got the SprintPowerup Tag!");
                pm.collectSprintPowerup();
                break;
            default:
                break;
        }


        // Split logic based on collectible type

        // if health collectible

        // Health Collectible
        // Stamina Collectible
        // Recharge rate for sprint

        // pm.ReceiveBall();
        // EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);
        // Destroy(this.gameObject);
    }
}
