using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject fracturedObject;
    public GameObject explosin;
    public float explosionMinForce = 5;
    public float explosionMaxForce = 100;
    public float explosionRadius = 10;
    public float scale = 1;

    private GameObject fractObj;
    private bool isExploding = false;  // The new flag
    private List<Coroutine> shrinkCoroutines = new List<Coroutine>(); // List to store Shrink coroutines

    void Update()
    {
        if (!isExploding && Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    void Explode()
    {
        isExploding = true;  // Set the flag to true at the beginning of the operation

        if (originalObject != null)
        {
            originalObject.SetActive(false);
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

                Destroy(fractObj, 7);

                if (explosin != null)
                {
                    GameObject expl = Instantiate(explosin) as GameObject;
                    Destroy(expl, 7);
                }
            }
        }
    }

    void Reset()
    {
        foreach (Coroutine coroutine in shrinkCoroutines) // Stop all the Shrink coroutines
        {
            StopCoroutine(coroutine);
        }
        shrinkCoroutines.Clear(); // Clear the list for next use

        if (fractObj != null) 
        {
            Destroy(fractObj);
        }
        originalObject.SetActive(true);
        isExploding = false;  // Reset the flag here as well
    }


    IEnumerator Shrink(Transform t, float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 newScale = t.localScale;

        while (t != null && newScale.x >= 0)
        {
            newScale -= new Vector3(scale, scale, scale);
            t.localScale = newScale;
            yield return new WaitForSeconds(0.05f);
        }

        isExploding = false;  // Set the flag to false at the end of the operation
    }



}
