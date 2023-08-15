using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentScript : MonoBehaviour
{
    public Transform target;
    public AudioClip sound; // sound that will be played when 'M' is pressed
    public AudioClip freeMonkey; // sound that will be played when 'N' is pressed
    public AudioClip ComeBack; // sound that will be played when 'B' is pressed
    public AudioClip MonkeySound;

    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    AudioSource audioSource;
    bool shouldFollow = true;
    [SerializeField] private Transform[] points;
    int pointsIndex = 0;
    bool canPressM = false;


    public AudioSource audioSource1;
    public AudioSource audioSource2;


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

        if (Input.GetKeyDown(KeyCode.M) && canPressM)
        {
            float distance = Vector3.Distance(agent.transform.position, target.position);
            audioSource2.Stop();
            audioSource1.PlayOneShot(sound);

            if (distance >= 5f)
            {
                audioSource1.PlayOneShot(ComeBack);
            }

            shouldFollow = !shouldFollow;
            if (shouldFollow)
            {
                pointsIndex = 0;
                agent.SetDestination(target.position); // Explicitly set destination immediately after toggle

            }

            canPressM = false;


        }

        if (Input.GetKeyDown(KeyCode.N) && shouldFollow)
        {
            audioSource1.PlayOneShot(freeMonkey);
            StartCoroutine(PlayDelayedSound());
            shouldFollow = false;
            pointsIndex = 0;
            VisitPoints();
            canPressM = true; // Allow M to be pressed after N has been pressed
        }
    }

    IEnumerator PlayDelayedSound() // Custom IEnumerator for delay
    {
        yield return new WaitForSeconds(1f);  // Wait for 1 second
        audioSource2.PlayOneShot(MonkeySound);
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
