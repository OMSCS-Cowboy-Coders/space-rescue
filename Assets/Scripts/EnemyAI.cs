using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    NavMeshAgent agent;
    private NavMeshHit hit;
    private bool blocked = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController charController = Player.GetComponent<CharacterController>();
        float remainingDistance = Vector3.Distance(agent.transform.position, Player.transform.position);
        float lookAheadTime = Mathf.Clamp(remainingDistance / agent.speed, 0,6);
        Vector3 futureTarget = Player.transform.position + lookAheadTime * charController.velocity;
        blocked = NavMesh.Raycast(agent.transform.position, futureTarget, out hit, NavMesh.AllAreas);
        Debug.DrawLine(agent.transform.position, futureTarget, blocked ? Color.red : Color.green);
        Vector3 dest = transform.TransformPoint(futureTarget);
        if (blocked){
            //View is blocked. Go to original waypoint position
            Debug.DrawRay(hit.position, Vector3.up, Color.red);
            agent.SetDestination(futureTarget);
        }
        else{
            //Not blocked, so go towards it
            agent.SetDestination(Player.transform.position);
 }
    }
}
