using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState {
    Idle = 300,
    Patrolling = 200,
    Following = 100,
}

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    NavMeshAgent agent;
    private NavMeshHit hit;
    private bool blocked = false;
    private EnemyState state;
    private CharacterController charController;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        charController = Player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        state = determineState();

        agent.SetDestination(determineNextPosition(state));
    }

    EnemyState determineState() {
        float remainingDistance = determineRemainingDistance();
        if (remainingDistance < (int) EnemyState.Following) {
            return EnemyState.Following;
        } else if (remainingDistance < (int) EnemyState.Patrolling) {
            return EnemyState.Patrolling;
        } else {
            return EnemyState.Idle;
        }
    }
    private float determineRemainingDistance() {
        charController = Player.GetComponent<CharacterController>();
        return Vector3.Distance(agent.transform.position, Player.transform.position);
    }

    Vector3 determineNextPosition(EnemyState state) {
        Vector3 dest;
        switch(state) {
            case EnemyState.Idle:
                dest = agent.transform.position; // keep the same
                break;
            case EnemyState.Patrolling:
                dest = agent.transform.position;
                // Vector3 randomDirection = Random.insideUnitSphere * 2;
                // randomDirection += agent.transform.position;
                // NavMeshHit hit;
                // if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
                // {
                //     return hit.position;
                // }
                // dest = origin;
                break;
            case EnemyState.Following:
                float remainingDistance = determineRemainingDistance();
                float lookAheadTime = Mathf.Clamp(remainingDistance / agent.speed, 0,6);
                Vector3 futureTarget = Player.transform.position + lookAheadTime * charController.velocity;
                blocked = NavMesh.Raycast(agent.transform.position, futureTarget, out hit, NavMesh.AllAreas);
                Debug.DrawLine(agent.transform.position, futureTarget, blocked ? Color.red : Color.green);
                
                if (blocked){
                    dest = transform.TransformPoint(futureTarget);
                    //View is blocked. Go to original waypoint position
                    // Debug.DrawRay(hit.position, Vector3.up, Color.red);
                    // agent.SetDestination(futureTarget);
                }
                else{
                    //Not blocked, so go towards it
                    dest = Player.transform.position;
                    // agent.SetDestination(Player.transform.position);
                }
                break;
            default:
                dest = agent.transform.position;
                break;
        }
        return dest;
    }
}
