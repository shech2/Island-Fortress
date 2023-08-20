using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
    public AudioClip buttonClickSound; // Audio clip for button click sound
    public AudioClip backgroundMusic; // Background music clip
    private AudioSource audioSource;
    public Slider volumeSlider;


    // Start is called before the first frame update
    void Start()
    {
        quitMenu.enabled = false;
        audioSource = GetComponent<AudioSource>();

        if (backgroundMusic)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; // Makes sure the music loops
            audioSource.Play();
        }

        if (volumeSlider)
        {
            volumeSlider.value = audioSource.volume;

            // Add a listener to the slider to detect value changes
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void ExitPress()
    {
        PlaySound();
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPress()
    {
        PlaySound();
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    public void PlayLevel()
    {
        PlaySound();
        audioSource.Stop(); // Stops the background music
        Application.LoadLevel(0); // Note: Application.LoadLevel is obsolete, consider using SceneManager.LoadScene
    }

    public void ExitGame()
    {
        PlaySound();
        Application.Quit();
    }

    private void PlaySound()
    {
        if (audioSource && buttonClickSound)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource)
        {
            audioSource.volume = volume;
        }
    }
}
