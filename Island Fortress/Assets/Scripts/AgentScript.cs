using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentScript : MonoBehaviour
{
    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        
        // Calculate distance between agent and target
        float distance = Vector3.Distance(agent.transform.position, target.position);
        
        // If the distance is less or equal to 3, set IsSit to true
        if (distance <= 3f)
        {
            animator.SetBool("IsSit", true);
            agent.stoppingDistance = 2f; // Stop 1 unit away from the target
        }
        else
        {
            animator.SetBool("IsSit", false);

           
        }

        if(distance >= 8f)
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }
    }
}
