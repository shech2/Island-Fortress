using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform[] points;
    private Animator animator;
    int pointsIndex = 0;

    public float WaitTime = 2f;
    public float targetRange = 30f;

    private GameObject player;
    private float playerHealth = 100f;  // Player's health
    public TextMeshProUGUI playerHealthText;  // Reference to the TextMeshPro component to display health

    public float attackCooldown = 1f; // 1 second cooldown for the attack
    private float lastAttackTime = -1f; // When the last attack occurred

    public AudioClip soundClip; // The audio clip you want to play
    private AudioSource audioSource;
    public float soundPlayDistance = 6f; // Distance within which the sound will play
    public float soundCooldown = 5f; // Cooldown for the sound to be played again
    private float lastSoundTime = -5f; // The time when the sound was last played


    IEnumerator Start()
    {
        audioSource = GetComponent<AudioSource>();


        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent.autoBraking = false;
        UpdatePlayerHealthDisplay();  // Update the UI display at start

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
        else
        {
            VisitPoints();
        }
    }

    private bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (angle < 45f && directionToPlayer.magnitude < targetRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit))
            {
                if (hit.collider.gameObject == player)
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
        agent.destination = player.transform.position;
        animator.SetBool("Run", true);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(soundClip);
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            AttackEnemy();
        }
    }

    private void AttackEnemy()
    {
        agent.destination = player.transform.position;
        animator.SetBool("Run", false);

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(soundClip);
            }

            animator.SetBool("Attack", true);


            // Check distance when attacking
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= 3f)
            {
                DecreasePlayerHealth(10f);
                lastAttackTime = Time.time; // Update the last attack time
            }
        }
    }


    private void DecreasePlayerHealth(float amount)
    {
        playerHealth -= amount;
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(soundClip);
        }


        if (playerHealth < 0)
            playerHealth = 0; // Ensuring health doesn't go below 0

        UpdatePlayerHealthDisplay();
    }

    // Method to update player's health on the UI
    private void UpdatePlayerHealthDisplay()
    {
        if (playerHealthText != null)
        {
            playerHealthText.text = playerHealth.ToString();
        }

        // Check if player's health is 0 or below
        if (playerHealth <= 0)
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;  // Pause the game
    }




}


//________________________________________________________________________________________The old code !! ________________________________________________________________________________________

// using UnityEngine;
// using UnityEngine.AI;
// using System.Collections;

// public class PlayerController : MonoBehaviour
// {
//     private NavMeshAgent agent;
//     [SerializeField] private Transform[] points;
//     private Animator animator;
//     int pointsIndex = 0;

//     public float WaitTime = 2f;
//     public float targetRange = 30f;
//     IEnumerator Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         animator = GetComponent<Animator>();
//         agent.autoBraking = false;

//         while (true)
//         {
//             VisitPoints();
//             yield return new WaitForSeconds(WaitTime);
//         }
//     }

//     private void Update()
//     {
//         if (IsPlayerInSight())
//         {
//             ChaseEnemy();
//         }
//         else
//         {
//             VisitPoints();
//         }
//     }


//     private bool IsPlayerInSight()
//     {
//         GameObject enemy = GameObject.FindGameObjectWithTag("Player");
//         Vector3 directionToPlayer = enemy.transform.position - transform.position;
//         float angle = Vector3.Angle(directionToPlayer, transform.forward);

//         if (angle < 45f && directionToPlayer.magnitude < targetRange)
//         {
//             RaycastHit hit;
//             if (Physics.Raycast(transform.position, directionToPlayer, out hit))
//             {
//                 if (hit.collider.gameObject.tag == "Player")
//                 {
//                     Debug.DrawRay(transform.position, directionToPlayer, Color.green);
//                     return true;
//                 }
//             }
//         }
//         return false;
//     }

//     private void VisitPoints()
//     {
//         if (points.Length == 0)
//             return;

//         agent.destination = points[pointsIndex].position;
//         animator.SetBool("Run", true);
//         animator.SetBool("Attack", false);

//         if (agent.remainingDistance <= agent.stoppingDistance)
//         {
//             pointsIndex = (pointsIndex + 1) % points.Length;
//         }
//     }

//     private void ChaseEnemy()
//     {
//         GameObject enemy = GameObject.FindGameObjectWithTag("Player");
//         agent.destination = enemy.transform.position;
//         animator.SetBool("Run", true);
//         if (agent.remainingDistance <= agent.stoppingDistance)
//         {
//             AttackEnemy();
//         }
//     }

//     private void AttackEnemy()
//     {
//         GameObject enemy = GameObject.FindGameObjectWithTag("Player");
//         agent.destination = enemy.transform.position;
//         animator.SetBool("Run", false);
//         animator.SetBool("Attack", true);
//     }

// }
