using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamAnimationScript : MonoBehaviour
{
    private Animator anim;
    AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip runningSound;
    public AudioClip EnviormentSound;

    public AudioSource walkingAudioSource;
    public AudioSource runningAudioSource;

    public AudioSource EnviormentAudioSource;
    // Start is called before the first frame update

    void Awake()
    {
        anim = GetComponent<Animator>();
        EnviormentAudioSource.clip = EnviormentSound;
        EnviormentAudioSource.loop = true;
        EnviormentAudioSource.volume = 0.3f;
        EnviormentAudioSource.Play();

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
                walkingAudioSource.volume = 0.2f;
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
                runningAudioSource.volume = 0.5f;
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
            // Play the backward walking sound here if needed
            walkingAudioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetFloat("speed", 0);
            // Stop the backward walking sound here if needed
            walkingAudioSource.Stop();
        }


    }
}
