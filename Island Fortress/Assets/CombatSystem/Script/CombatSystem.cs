using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatSystem : MonoBehaviour
{
    public GameObject PlayerHealthBar;
    [SerializeField]
    private SceneManager SceneManager;
    private float PlayerHealth = 100f;


    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            PlayerHealth -= 100;
            PlayerHealthBar.GetComponent<TextMeshProUGUI>().text = PlayerHealth.ToString();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth <= 0)
        {
            Death();
        }

    }

    private void Death()
    {
        SceneManager.ResetScene();
    }
}
