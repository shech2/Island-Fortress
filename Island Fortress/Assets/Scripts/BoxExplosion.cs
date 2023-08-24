using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BoxExplosion : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject fracturedObject;
    public GameObject explosion;

    public GameObject Gaz;
    public float explosionMinForce = 5;
    public float explosionMaxForce = 100;
    public float explosionRadius = 10;
    public float scale = 1;
    public ParticleSystem particleSystem;
    public AudioClip audioClip;
    public Text promptText;
    public GameObject PlayerHealthBar;
    private GameObject fractObj;
    private bool isExploding = false;
    private List<Coroutine> shrinkCoroutines = new List<Coroutine>();
    private AudioSource audioSource;
    private GameObject player;
    private bool hasExploded = false;
    private float PlayerHealth = 100f;
    private Vector3 playerStartingPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Gaz.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerStartingPosition = player.transform.position;
        }
        UpdateHealthBar();
    }

    void Update()
    {
        if (IsPlayerNearby() && PlayerIsFacingBox())
        {
            if (!isExploding)
            {
                if (promptText != null)
                {
                    promptText.text = "press Q to open the box";
                    promptText.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Gaz.SetActive(false);
                    if (!gameObject.activeInHierarchy)
                    {
                        Debug.LogWarning("Trying to explode an inactive object: " + gameObject.name);
                        return;
                    }
                    promptText.enabled = false;
                    isExploding = true;
                    Explode();
                }
            }
        }
        else
        {
            if (promptText != null)
            {
                promptText.enabled = false;
            }
        }

        if (PlayerHealth <= 0)
        {
            StartCoroutine(ReturnPlayerToStartPosition()); // This will handle the delay before death
        }
    }


    void Explode()
    {
        isExploding = true;
        hasExploded = true;
        particleSystem.Play();
        audioSource.PlayOneShot(audioClip);

        if (fracturedObject != null)
        {
            fractObj = Instantiate(fracturedObject) as GameObject;
            foreach (Transform t in fractObj.transform)
            {
                var rb = t.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce), originalObject.transform.position, explosionRadius);
                }
                Coroutine shrinkCoroutine = StartCoroutine(Shrink(t, 7));
                shrinkCoroutines.Add(shrinkCoroutine);
            }

            Destroy(fractObj, 8);

            if (explosion != null)
            {
                GameObject expl = Instantiate(explosion) as GameObject;
                Destroy(expl, 7);
            }

            PlayerHealth -= 100;
        }

        if (originalObject != null)
        {
            originalObject.SetActive(false);
        }
    }

    IEnumerator Shrink(Transform t, float delay)
    {
        yield return new WaitForSeconds(delay);
        while (t != null && t.gameObject != null)
        {
            Vector3 newScale = t.localScale;
            if (newScale.x >= 0)
            {
                newScale -= new Vector3(scale, scale, scale);
                t.localScale = newScale;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private bool IsPlayerNearby()
    {
        if (player != null)
        {
            return Vector3.Distance(transform.position, player.transform.position) < 3f;
        }
        return false;
    }

    private bool PlayerIsFacingBox()
    {
        if (player != null)
        {
            Vector3 toBox = (transform.position - player.transform.position).normalized;
            float angleToBox = Vector3.Angle(player.transform.forward, toBox);
            return angleToBox < 45f;
        }
        return false;
    }

    private void Death()
    {
        if (player != null)
        {
            player.transform.position = playerStartingPosition;
            PlayerHealth = 100f;
            UpdateHealthBar();
        }
    }



    private void UpdateHealthBar()
    {
        if (PlayerHealthBar != null)
        {
            PlayerHealthBar.GetComponent<TextMeshProUGUI>().text = PlayerHealth.ToString();
        }
    }

    IEnumerator ReturnPlayerToStartPosition()
    {
        yield return new WaitForSeconds(1.0f);
        if (player != null)
        {
            Death();
        }
    }

}
