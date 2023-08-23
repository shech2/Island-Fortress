using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatSystem : MonoBehaviour
{
    public GameObject PlayerHealthBar;
    private float PlayerHealth = SceneManager.Instance.Health;
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
        UpdateHealthBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            PlayerHealth -= 100;
            UpdateHealthBar();
        }
    }

    public float GetPlayerHealth()
    {
        return PlayerHealth;
    }

    void Update()
    {
        if (PlayerHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        transform.position = startingPosition;
        PlayerHealth = 100f; // reset player's health
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (PlayerHealthBar != null)
        {
            PlayerHealthBar.GetComponent<TextMeshProUGUI>().text = PlayerHealth.ToString();
        }
    }
}
