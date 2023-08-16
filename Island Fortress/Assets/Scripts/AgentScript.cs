using System.Collections;
using UnityEngine;

public class AgentScript : MonoBehaviour
{
    public Transform target;
    public AudioClip sound;
    public AudioClip freeMonkey;
    public AudioClip ComeBack;
    public AudioClip MonkeySound;

    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    AudioSource audioSource;
    bool shouldFollow = true;
    [SerializeField] private Transform[] points;
    int pointsIndex = 0;
    bool canPressM = false;
    bool shouldMoveToNextPoint = true;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    private bool isClickedCoroutineRunning = false;

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

            if (distance <= 4f)
            {
                animator.SetBool("IsSit", true);
                agent.stoppingDistance = 3f;

                if (distance <= 2f && !isClickedCoroutineRunning)
                {
                    StartCoroutine(HandleClickedState());
                }
            }
            else
            {
                animator.SetBool("IsSit", false);
                if (isClickedCoroutineRunning)
                {
                    StopCoroutine(HandleClickedState());
                    animator.SetBool("IsClicked", false);
                    isClickedCoroutineRunning = false;
                }
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
            // Check if the agent has reached its current point
            if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                shouldMoveToNextPoint = true;
            }

            // Move to the next point if needed
            if (shouldMoveToNextPoint)
            {
                VisitPoints();
            }
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
                VisitPoints();
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
    IEnumerator HandleClickedState()
    {
        isClickedCoroutineRunning = true;
        animator.SetBool("IsClicked", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsClicked", false);
        yield return new WaitForSeconds(10f);
        isClickedCoroutineRunning = false;
    }


    IEnumerator PlayDelayedSound()
    {
        yield return new WaitForSeconds(1f);
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

        shouldMoveToNextPoint = false;
    }
}
