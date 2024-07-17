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

    void OnCollisionEnter(Collision c) {
        // check if the collider is an alien
        string tag = c.gameObject.tag;
        print("TRIGGERED by " + tag);

        switch (tag) {
            case "Alien":
                Debug.Log("DECREMENT HEALTH");
                playerMetrics.decrementHealth(false);
                break;
            case "Crater_Floor":
                playerMetrics.decrementHealth(true);
                break;
            case "Volcano":
                playerMetrics.decrementHealth(true);
                break;
            case "LavaRocks":
                playerMetrics.decrementHealth(false);
                break;
            default:
                break;
        }
    }
}
