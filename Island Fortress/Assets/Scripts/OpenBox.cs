using UnityEngine;
using UnityEngine.UI; // Necessary to access the Text component

public class OpenBox : MonoBehaviour
{
    public float proximityThreshold = 2f;
    public float maxOpenAngle = 30f;
    public GameObject player;
    public Text promptText; // Reference to the UI Text component

    private Animator animator;

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
            // If the animator is not yet enabled, show the prompt.
            if (!animator.enabled && promptText != null)
            {
                promptText.text = "press Q to open the box";
                promptText.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator.enabled = true;
                // Hide the text prompt after pressing Q
                if (promptText != null)
                {
                    promptText.enabled = false;
                }
            }
        }
        else
        {
            // Hide the text prompt if the player is not in position
            if (promptText != null)
            {
                promptText.enabled = false;
            }
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
