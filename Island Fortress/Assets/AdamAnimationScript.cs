using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamAnimationScript : MonoBehaviour
{
    private Animator anim;
    AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip runningSound;

    public AudioSource walkingAudioSource;
    public AudioSource runningAudioSource;
    // Start is called before the first frame update

    void Awake()
    {
        anim = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        // Walking
        if (Input.GetKeyDown(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("speed", 1);
            if (!walkingAudioSource.isPlaying)
            {
                walkingAudioSource.clip = walkingSound;
                walkingAudioSource.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("speed", 0);
            walkingAudioSource.Stop();
        }

        // Running
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            anim.SetFloat("speed", 2);
            walkingAudioSource.Stop();  // Stop walking sound
            if (!runningAudioSource.isPlaying)
            {
                runningAudioSource.clip = runningSound;
                runningAudioSource.loop = true;
                runningAudioSource.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || !Input.GetKey(KeyCode.W))
        {
            runningAudioSource.Stop();
            runningAudioSource.loop = false;
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("speed", 1);
                walkingAudioSource.Play();
            }
            else
            {
                anim.SetFloat("speed", 0);
            }
        }


        // Handle S key logic (backward)
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetFloat("speed", -1);
            // you can add a backward walking sound here if needed
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetFloat("speed", 0);
            // Stop the backward walking sound here if needed
        }

    }
}
