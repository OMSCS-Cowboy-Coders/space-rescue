using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienMotionController : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocityX;
    public float attackRange = 5f;
    public float velocityZ;
    public GameObject Player;

    Animator animator;
    NavMeshAgent agent;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void OnCollisionEnter(Collision collision){
        string tag = collision.gameObject.tag;
        if(tag == "Player"){
            animator.SetBool("Attack_Player",true);
        }
    }

    void OnCollisionExit(Collision collision){
        string tag = collision.gameObject.tag;
        if(tag == "Player"){
            animator.SetBool("Attack_Player",false);
        }
    }
    void Update()
    {
        velocityX = agent.velocity.x;
        velocityZ = agent.velocity.z;
        animator.SetFloat("velocityX", velocityX);
        animator.SetFloat("velocityZ", velocityZ);
        Vector3 playerPos = this.Player.transform.position;
        
    }
}
