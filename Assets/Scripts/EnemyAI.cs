using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState {
    Idle = 300f,
    Patrolling = 200f,
    Following = 100f,
}

public class EnemyAI : MonoBehaviour
{
    private float 
    public GameObject Player;
    NavMeshAgent agent;
    private NavMeshHit hit;
    private bool blocked = false;
    private EnemyState state;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        state = determineState();
        
        agent.SetDestination(state);
        
        if (blocked){
            //View is blocked. Go to original waypoint position
            Debug.DrawRay(hit.position, Vector3.up, Color.red);
            agent.SetDestination(futureTarget);
        }
        else{
            //Not blocked, so go towards it
            agent.SetDestination(Player.transform.position);
        }

        agent.SetDestination()
    }

    EnemyState determineState() {
        float remainingDistance = determineRemainingDistance();
        if (remainingDistance < EnemyState.Following) {
            return EnemyState.Following;
        } else if (remainingDistance < EnemyState.Patrolling) {
            return EnemyState.Patrolling;
        } else {
            return EnemyState.Idle;
        }
    }
    private float determineRemainingDistance() {
        CharacterController charController = Player.GetComponent<CharacterController>();
        return Vector3.Distance(agent.transform.position, Player.transform.position);
    }

    Vector3 determineNextPosition(EnemyState state) {
        Vector3 dest;
        switch(state) {
            case EnemyState.Idle:
                dest = agent.transform.position; // keep the same
                break;
            case EnemyState.Patrolling:
                break;
            case EnemyState.Following:
                float remainingDistance = determineRemainingDistance();
                float lookAheadTime = Mathf.Clamp(remainingDistance / agent.speed, 0,6);
                Vector3 futureTarget = Player.transform.position + lookAheadTime * charController.velocity;
                blocked = NavMesh.Raycast(agent.transform.position, futureTarget, out hit, NavMesh.AllAreas);
                Debug.DrawLine(agent.transform.position, futureTarget, blocked ? Color.red : Color.green);
                dest = transform.TransformPoint(futureTarget);
                break;
            default:
                break;
        }
        return dest;
    }
}
