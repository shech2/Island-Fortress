using UnityEngine;
using UnityEngine.UI;

public class OpenBox : MonoBehaviour
{
    public float proximityThreshold = 2f;
    public float maxOpenAngle = 30f;
    public GameObject player;
    public GameObject paddleInsideBox; // Reference to the paddle inside the box
    public Vector3 jumpForce = new Vector3(0, 5, 0); // Adjust this to control the "jump out" force
    public Text promptText;

    private Animator animator;
    private bool boxOpened = false; // Flag to check if the box has already been opened

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;

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
        if (IsPlayerNearby() && PlayerIsFacingBox() && !boxOpened)
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
        // Enable the paddle's Mesh Renderer to make it visible
        paddleInsideBox.GetComponent<MeshRenderer>().enabled = true;

        // Get the paddle's Rigidbody
        Rigidbody rb = paddleInsideBox.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Make the Rigidbody dynamic and apply a force for the jump
            rb.isKinematic = false;
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
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
}
