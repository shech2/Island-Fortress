using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : Interactable
{
    [SerializeField]
    private GameObject Door;

    [SerializeField]
    private AudioClip openDoorSound;  // sound when the door opens
    [SerializeField]
    private AudioClip closeDoorSound; // sound when the door closes

    private AudioSource audioSource = null;
    private bool doorOpen;

    private void Awake()
    {
        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("No Audio Source on " + transform.name);
        }
    }

    protected override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            doorOpen = !doorOpen;

            // Check if AudioSource exists
            if (audioSource != null)
            {
                // Determine which sound to play based on the door state
                if (doorOpen)
                {
                    audioSource.clip = openDoorSound;
                }
                else
                {
                    audioSource.clip = closeDoorSound;
                }

                // Stop any currently playing sound and play the new sound
                audioSource.Stop();
                audioSource.Play();
            }

            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        base.prompMessage = doorOpen ? "Close Door" : "Open Door";
        Door.GetComponent<Animator>().SetBool("open", doorOpen);
    }
}
