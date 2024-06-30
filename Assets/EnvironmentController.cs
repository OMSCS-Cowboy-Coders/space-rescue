using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMetrics playerMetrics;
    void Start()
    {
        playerMetrics = gameObject.GetComponent<PlayerMetrics>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c) {
        // check if the collider is an alien
        print("TRIGGERED");

    }
}
