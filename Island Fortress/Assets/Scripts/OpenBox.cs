using UnityEngine;
using UnityEngine.UI;

public class OpenBox : MonoBehaviour
{
    public float proximityThreshold = 2f;
    public float maxOpenAngle = 30f;
    public GameObject player;
    public GameObject paddleInsideBox; // Reference to the paddle inside the box
    public GameObject paddleInBoat; // Reference to the paddle outside the boat
    public Vector3 jumpForce = new Vector3(0, 5, 0); // Adjust this to control the "jump out" force
    public Text promptText;

    public AudioClip sound;  // Corrected capitalization
    public AudioClip takePaddleSound;

    private AudioSource audioSource;  // Corrected capitalization
    private Animator animator;
    private bool boxOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        audioSource = GetComponent<AudioSource>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogWarning("Player not assigned and cannot find GameObject with 'Player' tag.");
            }
        }

        // Initialize the paddle to be invisible
        if (paddleInsideBox != null)
        {
            paddleInsideBox.GetComponent<MeshRenderer>().enabled = false;
        }

        // Hide the text at the start
        if (promptText != null)
        {
            promptText.enabled = false;
        }
    }

    void Update()
    {
        if (IsPlayerNearby() && PlayerIsFacingBox())
        {
            if (!boxOpened)
            {
                if (promptText != null)
                {
                    promptText.text = "press Q to open the box";
                    promptText.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {

                    animator.enabled = true;
                    OpenBoxAndReleasePaddle();
                    boxOpened = true;
                }
            }
        }
        else if (boxOpened && paddleInsideBox.activeSelf && IsPlayerNearPaddle() && PlayerIsFacingPaddle())
        {
            if (promptText != null)
            {
                promptText.text = "Press E to take";
                promptText.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                TakePaddle();
            }
        }
        else
        {
            if (promptText != null)
            {
                promptText.enabled = false;
            }
        }
    }

    void OpenBoxAndReleasePaddle()
    {
        audioSource.PlayOneShot(sound);
        Invoke("StopSound", 1f);

        // Enable the paddle's Mesh Renderer to make it visible
        if (paddleInsideBox != null)
        {
            paddleInsideBox.GetComponent<MeshRenderer>().enabled = true;
        }

        // Get the paddle's Rigidbody
        Rigidbody rb = paddleInsideBox.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Make the Rigidbody dynamic and apply a force for the jump
            rb.isKinematic = false;
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    private void TakePaddle()
    {
        audioSource.PlayOneShot(takePaddleSound);
        // Deactivate the paddle to "pick it up"
        paddleInsideBox.SetActive(false);
        paddleInBoat.GetComponent<MeshRenderer>().enabled = true;
        Objective[] o = FindAnyObjectByType<ObjectPanel>().GetObjectives();
        ObjectiveManager op = FindAnyObjectByType<ObjectiveManager>();
        if (o.Length > 0)
        {
            o[0].isCompleted = true;
            op.CompleteObjective(o[0]);
        }
        // Hide the prompt text
        if (promptText != null)
        {
            promptText.enabled = false;
        }
    }

    private bool IsPlayerNearPaddle()
    {
        float distanceToPaddle = Vector3.Distance(player.transform.position, paddleInsideBox.transform.position);
        return distanceToPaddle < proximityThreshold;
    }

    private bool PlayerIsFacingPaddle()
    {
        Vector3 toPaddle = (paddleInsideBox.transform.position - player.transform.position).normalized;
        float angleToPaddle = Vector3.Angle(player.transform.forward, toPaddle);
        return angleToPaddle < maxOpenAngle;
    }

    private bool IsPlayerNearby()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        return distanceToPlayer < proximityThreshold;
    }

    private bool PlayerIsFacingBox()
    {
        Vector3 toBox = (transform.position - player.transform.position).normalized;
        float angleToBox = Vector3.Angle(player.transform.forward, toBox);
        return angleToBox < maxOpenAngle;
    }

    void StopSound()
    {
        audioSource.Stop();
    }
}
