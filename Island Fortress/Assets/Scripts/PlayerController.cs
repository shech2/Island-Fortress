using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform[] points;
    private Animator animator;
    int pointsIndex = 0;

    public float WaitTime = 2f;
    public float targetRange = 30f;

    private GameObject player;

    public float attackCooldown = 1f; // 1 second cooldown for the attack
    private float lastAttackTime = -1f; // When the last attack occurred

    public AudioClip soundClip; // The audio clip you want to play
    private AudioSource audioSource;
    public float soundPlayDistance = 15f; // Distance within which the sound will play
    public float soundCooldown = 5f; // Cooldown for the sound to be played again
    private float lastSoundTime = -5f; // The time when the sound was last played
    public GameObject panel;


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
        // Existing AI logic
        if (IsPlayerInSight())
        {
            ChaseEnemy();
        }
        else
        {
            VisitPoints();
        }

        // Sound based on distance logic
        ManageSoundBasedOnDistance();
        CheckIfPanelIsOpen();
    }

    private void ManageSoundBasedOnDistance()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // If the enemy is within the soundPlayDistance from the player and hasn't played a sound in a while, play the sound
        if (distanceToPlayer <= soundPlayDistance && Time.time >= lastSoundTime + soundCooldown)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(soundClip);
                lastSoundTime = Time.time; // Update the time when the sound was last played
            }
        }
        else if (distanceToPlayer > 15f) // If the distance is more than 15f, stop the audio
        {
            audioSource.Stop();
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

        // Calculate the distance between the enemy and the player


        // If the enemy is within the soundPlayDistance from the player and hasn't played a sound in a while, play the sound


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
        SceneManager.Instance.Health -= amount;
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(soundClip);
        }


        if (SceneManager.Instance.Health < 0)
            SceneManager.Instance.Health = 0; // Ensuring health doesn't go below 0

        UpdatePlayerHealthDisplay();
    }

    // Method to update player's health on the UI
    private void UpdatePlayerHealthDisplay()
    {
        GameObject playerHealthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar");
        if (playerHealthBar != null)
        {
            playerHealthBar.GetComponent<TextMeshProUGUI>().text = SceneManager.Instance.Health.ToString();
        }
        // Check if player's health is 0 or below
        if (SceneManager.Instance.Health <= 0)
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;  // Pause the game
        audioSource.Stop();  // Stop the audio
        panel.gameObject.SetActive(true);  // Show the panel
    }

    private void CheckIfPanelIsOpen()
    {
        if (panel.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.Instance.Health = 100f; // Reset the health
                Time.timeScale = 1;  // Resume the game
                panel.gameObject.SetActive(false);  // Hide the panel
                SceneManager.Instance.ResetScene();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;  // Resume the game
                panel.gameObject.SetActive(false);  // Hide the panel
                Cursor.lockState = CursorLockMode.None;
                SceneManager.Instance.LoadScene("Scene0");
            }
        }
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
