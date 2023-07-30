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

        if (angle < 45f && directionToPlayer.magnitude < targetRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(transform.position, directionToPlayer, Color.green);
                    return true;
                }
            }
        }
        return false;
    }

    private void VisitPoints()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[pointsIndex].position;
        animator.SetBool("Run", true);
        animator.SetBool("Attack", false);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            pointsIndex = (pointsIndex + 1) % points.Length;
        }
    }

    private void ChaseEnemy()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");
        agent.destination = enemy.transform.position;
        animator.SetBool("Run", true);
        animator.SetBool("Attack", false);
    }

}
