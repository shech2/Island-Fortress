using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;
    public float transitionTime = 2f;
    public float overheadDelay = 1f;
    public AudioClip cameraSwitchSound; // The sound to be played during camera switches

    private AudioSource audioSource; // To play the sound
    private int currentIndex = 0;
    private bool isSwitching = false;

    private void Start()
    {
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }

        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
        }

        audioSource = GetComponent<AudioSource>(); // Assuming you've added an AudioSource component to this GameObject
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource component found on this GameObject. Adding one.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isSwitching)
        {
            if (currentIndex == 0) // If the current camera is the first one
            {
                StartCoroutine(AutoSwitchSequence());
            }
            else if (currentIndex == cameras.Length - 1) // If the current camera is the last one
            {
                StartCoroutine(ReverseSwitchSequence());
            }
        }
    }

    private IEnumerator AutoSwitchSequence()
    {
        isSwitching = true;

        for (int i = currentIndex + 1; i < cameras.Length; i++)
        {
            cameras[currentIndex].gameObject.SetActive(false);

            currentIndex = i;
            cameras[currentIndex].gameObject.SetActive(true);

            // Play the switch sound
            if (cameraSwitchSound)
            {
                audioSource.PlayOneShot(cameraSwitchSound);
            }

            if (currentIndex == 1) // If it's the overhead camera
            {
                yield return new WaitForSeconds(overheadDelay);
            }
            else
            {
                yield return new WaitForSeconds(transitionTime);
            }
        }

        isSwitching = false;
    }

    private IEnumerator ReverseSwitchSequence()
    {
        isSwitching = true;

        for (int i = currentIndex - 1; i >= 0; i--)
        {
            cameras[currentIndex].gameObject.SetActive(false);

            currentIndex = i;
            cameras[currentIndex].gameObject.SetActive(true);

            // Play the switch sound
            if (cameraSwitchSound)
            {
                audioSource.PlayOneShot(cameraSwitchSound);
            }

            if (currentIndex == 1) // If it's the overhead camera
            {
                yield return new WaitForSeconds(overheadDelay);
            }
            else
            {
                yield return new WaitForSeconds(transitionTime);
            }
        }

        isSwitching = false;
    }
}
