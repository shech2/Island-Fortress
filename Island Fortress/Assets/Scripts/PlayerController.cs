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
    public float targetRange = 60f;
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
        // if Player is in sight of Enemy, chase Enemy
        if (IsPlayerInSight())
        {
            ChaseEnemy();
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
        agent.SetDestination(enemy.transform.position);

        if (agent.remainingDistance < 2f)
        {
            agent.isStopped = true;
            animator.SetBool("Attack", true);
        }
        else
        {
            agent.isStopped = false;
            animator.SetBool("Attack", false);
        }

        if (agent.remainingDistance > 5)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
}
