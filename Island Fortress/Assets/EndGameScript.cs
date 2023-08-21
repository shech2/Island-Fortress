using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public RawImage rawImage;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            rawImage.gameObject.SetActive(true);
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        // Pause the game
        Time.timeScale = 0;
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}
