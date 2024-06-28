using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBall : MonoBehaviour
{
    void OnTriggerEnter(Collider c) {
        if (c.GetComponent<Rigidbody>() == null) return;
        BallCollector bc = c.GetComponent<Rigidbody>().gameObject.GetComponent<BallCollector>();
        if (bc == null) return;
        bc.ReceiveBall();
        print("Woohoo");
        EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);
        Destroy(this.gameObject);
    }
}
