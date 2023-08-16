using UnityEngine;
using UnityEngine.UI;

public class BoatScript : MonoBehaviour
{
    public GameObject Paddle;
    public GameObject Paddle1;
    public Text messageText;

    private void Start()
    {
        messageText.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!Paddle.GetComponent<MeshRenderer>().enabled && !Paddle1.GetComponent<MeshRenderer>().enabled)
            {
                messageText.text = "You have to find the 2 Paddles";
                messageText.gameObject.SetActive(true);
            }
            else if (!Paddle.GetComponent<MeshRenderer>().enabled || !Paddle1.GetComponent<MeshRenderer>().enabled)
            {
                messageText.text = "You have to find the other Paddle";
                messageText.gameObject.SetActive(true);
            }
            else
            {
                messageText.text = "Press E to get in the boat";
                messageText.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            messageText.gameObject.SetActive(false);
        }
    }
}
