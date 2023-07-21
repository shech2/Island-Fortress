using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RemyScript : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    public float health = 100f;
    public TextMeshProUGUI healthText;
    public GameObject Panel;
    // public GameOver gameOver;
    private AudioSource audioSource;
    private bool isShiftPressed = false;
    private bool isWPressed = false;
    private AudioSource walkingSound;
    private int screenshotIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        walkingSound = GetComponent<AudioSource>(); // Assign the AudioSource component to the new variable
                                                    // gameOver = new GameOver();
                                                    // gameOver.Panel = Panel;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            Time.timeScale = 0;
            Panel.SetActive(true);
            healthText.text = "0";
            // gameOver.EndGame();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            isWPressed = true;
            anim.SetBool("isWalking", true);
            walkingSound.Play(); // Play the walking sound
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            isWPressed = false;
            anim.SetBool("isWalking", false);
            walkingSound.Stop(); // Stop the walking sound
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isShiftPressed = true;
            anim.SetBool("isRunning", true);
            audioSource.Play(); // Play the sound when Shift is initially pressed
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isShiftPressed = false;
            anim.SetBool("isRunning", false);
            audioSource.Stop();
        }
        // If W is currently pressed, play the sound continuously
        if (isWPressed && !walkingSound.isPlaying)
        {
            walkingSound.Play();
        }
        // If Shift is currently pressed, play the sound continuously
        if (isShiftPressed && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ScreenCapture.CaptureScreenshot("Screenshot" + screenshotIndex + ".png");
            screenshotIndex++;
        }
    }

}
