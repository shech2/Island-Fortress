using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private bool soundPlayed = false; // Flag to check if sound has been played

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !soundPlayed)  // Check if sound has not been played yet
        {
            audioSource.Play();
            soundPlayed = true; // Set the flag to true after playing the sound
        }
    }
}
