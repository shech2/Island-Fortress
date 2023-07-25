using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetInteraction : Interactable
{
    [SerializeField]
    private GameObject ClosetDoor;

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
            if (audioSource != null)
            {

                audioSource.Play();
                OpenDoor();
            }
            else
            {
                OpenDoor();

            }
        }

    }

    private void OpenDoor()
    {
        base.prompMessage = doorOpen ? "Close Door" : "Open Door";
        ClosetDoor.GetComponent<Animator>().SetBool("open", doorOpen);
    }
}
