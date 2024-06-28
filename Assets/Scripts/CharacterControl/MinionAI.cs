using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum AIState {
    VisitStationaryWaypoints,
    VisitMovingWaypoint
}

public class MinionAI : MonoBehaviour
{
    public AIState aiState;
    public NavMeshAgent navMeshAgent;
    public Animator animator;

    public GameObject[] waypoints;

    public Transform movingWaypoint;

    private VelocityReporter targetVelocityReporter;

    public GameObject rectangle;
    public Transform rectanglePosition;

    int currWaypoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        rectangle.SetActive(false);
        aiState = AIState.VisitStationaryWaypoints;
        targetVelocityReporter = movingWaypoint.GetComponent<VelocityReporter>();
        
        setNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("vely", navMeshAgent.velocity.magnitude / navMeshAgent.speed);

        setNextWaypoint();
    }

    private void setNextWaypoint() {

        switch (aiState) {
            case AIState.VisitStationaryWaypoints:
                if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 1) {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f) {
                        if (waypoints.Length == 0) return;

                        if (currWaypoint >= waypoints.Length){
                            currWaypoint = 0;
                            // at this point the minion has visited all of the stationary waypoints
                            // and is now ready to chase the moving waypoint
                            switchState();
                            break;
                        }

                        navMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);

                        currWaypoint++;

                    }
                }
                break;
            case AIState.VisitMovingWaypoint:
                Vector3 targetVelocity = targetVelocityReporter.velocity;
                print("test");
                
                float distanceToTarget = Vector3.Distance(this.transform.position, movingWaypoint.position);
                float speed = navMeshAgent.speed;

                float lookaheadTime = distanceToTarget / (speed + targetVelocity.magnitude);

                // predict where the moving target is going to be next and then navigate there.
                Vector3 predictedPosition = movingWaypoint.position + targetVelocity * lookaheadTime;
                rectanglePosition.position = predictedPosition;
                if (distanceToTarget > 1)
                {
                    navMeshAgent.SetDestination(predictedPosition);
                } else {
                    switchState();
                }
                break;
            default: 
                break;
        }
    }

    // transitions
    private void switchState() {
        switch (aiState) {
            case AIState.VisitStationaryWaypoints:
                aiState = AIState.VisitMovingWaypoint;
                rectangle.SetActive(true);
                break;
            case AIState.VisitMovingWaypoint:
                aiState = AIState.VisitStationaryWaypoints;
                rectangle.SetActive(false);
                break;
            default: 
                break;
        }
    }
}
