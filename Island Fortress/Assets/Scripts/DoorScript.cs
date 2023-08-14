using UnityEngine;
using UnityEngine.UI; // Ensure you're using Unity's UI system for the message.

public class DoorScript : MonoBehaviour
{
    public Text messageText; // Drag and drop your Text object here if you're using Unity's UI.

    private void Start()
    {
        messageText.gameObject.SetActive(false); // Ensure the message is hidden at the start.
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the tag "Player" (make sure to tag your player appropriately).
        if (collision.gameObject.CompareTag("Player"))
        {
            messageText.text = "This door does not open";
            messageText.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            messageText.gameObject.SetActive(false); // Hide the message when the player moves away from the door.
        }
    }
}
