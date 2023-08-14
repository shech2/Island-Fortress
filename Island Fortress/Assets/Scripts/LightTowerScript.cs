using UnityEngine;
using UnityEngine.UI;

public class LightTowerScript : MonoBehaviour
{
    public Text messageText; 

    private void Start()
    {
        messageText.gameObject.SetActive(false); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            messageText.text = "No entry to the building";
            messageText.gameObject.SetActive(true);
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
