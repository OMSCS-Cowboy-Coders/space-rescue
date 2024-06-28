using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollector : MonoBehaviour
{
    public bool hasBall;
    // Start is called before the first frame update
    void Start()
    {
        hasBall = false;
    }

    public void ReceiveBall() {
        print("Received Ball");
        hasBall = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
