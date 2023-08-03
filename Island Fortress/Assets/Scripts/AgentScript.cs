using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentScript : MonoBehaviour
{
    public Transform target;
    public AudioClip sound; // sound that will be played when 'M' is pressed
    public AudioClip freeMonkey; // sound that will be played when 'N' is pressed
    public AudioClip ComeBack; // sound that will be played when 'B' is pressed
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    AudioSource audioSource;
    bool shouldFollow = true;
    [SerializeField] private Transform[] points;
    int pointsIndex = 0;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
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

            if (distance >= 8f)
            {
                animator.SetBool("IsRun", true);
            }
            else
            {
                animator.SetBool("IsRun", false);
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                VisitPoints();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            float distance = Vector3.Distance(agent.transform.position, target.position);
            audioSource.PlayOneShot(sound); // Play the sound
            if(distance>= 5f){
                audioSource.PlayOneShot(ComeBack); // Play the sound
            }
            
            shouldFollow = !shouldFollow;
            if (shouldFollow)
                pointsIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.N) && shouldFollow)
        {
            audioSource.PlayOneShot(freeMonkey); // Play the sound
            shouldFollow = false;
            pointsIndex = 0; // Reset pointsIndex to 0 each time 'N' is pressed
            VisitPoints();
        }
    }

    private void VisitPoints()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[pointsIndex].position;
        animator.SetBool("IsRun", true);
        animator.SetBool("IsSit", false);

        pointsIndex = (pointsIndex + 1) % points.Length;
    }
}
