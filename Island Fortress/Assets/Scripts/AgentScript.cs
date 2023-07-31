using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentScript : MonoBehaviour
{
    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    bool shouldFollow = true;
    [SerializeField] private Transform[] points;
    int pointsIndex = 0;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (shouldFollow)
        {
            agent.SetDestination(target.position);
        
            float distance = Vector3.Distance(agent.transform.position, target.position);
            
            if (distance <= 3f)
            {
                animator.SetBool("IsSit", true);
                agent.stoppingDistance = 2f;
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
        else  // If not following player, check if destination has been reached
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                VisitPoints();  // If destination has been reached, move to next point
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            shouldFollow = !shouldFollow;
            if (shouldFollow)
                pointsIndex = 0;  // If switching to follow player, reset points index
        }

        if (Input.GetKeyDown(KeyCode.N) && shouldFollow)
        {
            shouldFollow = false;
            VisitPoints();  // If switching to points navigation, move to first point
        }
    }

    private void VisitPoints()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[pointsIndex].position;
        animator.SetBool("IsRun", true);
        animator.SetBool("IsSit", false);

        pointsIndex = (pointsIndex + 1) % points.Length; // Increment and loop index if end is reached
    }
}
