using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform[] points;
    private Animator animator;
    int pointsIndex = 0;

    public float WaitTime = 2f;
    public float targetRange = 30f;
    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.autoBraking = false;

        while (true)
        {
            VisitPoints();
            yield return new WaitForSeconds(WaitTime);
        }
    }

    private void Update()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = Vector3.Distance(transform.position, enemy.transform.position);

        // If Player is in sight of Enemy and within a certain distance, chase Enemy
        if ((IsPlayerInSight() && distanceToPlayer <= 10f)  || (IsPlayerInSight() && distanceToPlayer <= 3f))
        {
            ChaseEnemy();
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            agent.isStopped = false;
            VisitPoints();
        }
    }


    private bool IsPlayerInSight()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");
        Vector3 directionToPlayer = enemy.transform.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        return angle <= targetRange;
    }

    private void VisitPoints()
    {
        if (points.Length > 0)
        {
            if (agent.remainingDistance < 0.5f)
            {
                if (pointsIndex >= points.Length)
                {
                    pointsIndex = 0;
                }
                else
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", false);
                    agent.SetDestination(points[pointsIndex].position);
                    pointsIndex++;
                }
            }
        }
    }

    private void ChaseEnemy()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");
        Vector3 directionToPlayer = enemy.transform.position - transform.position;
        directionToPlayer.y = 0;
        float distanceToPlayer = Vector3.Distance(transform.position, enemy.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToPlayer), 0.1f);

        if (distanceToPlayer > 3f && distanceToPlayer <= 10f)
        {
            animator.SetBool("Run", true);
            animator.SetBool("Attack", false);
            agent.isStopped = false;
            agent.SetDestination(enemy.transform.position);

        }
        else if(distanceToPlayer <= 3f)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);
            agent.isStopped = true;
        }

    }

}
