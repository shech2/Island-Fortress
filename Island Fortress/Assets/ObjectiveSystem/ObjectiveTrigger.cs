using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public Dialogue objective;
    public void TriggerObjective()
    {
        FindObjectOfType<ObjectiveManager>().StartObjective(objective);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TriggerObjective();
            this.gameObject.SetActive(false);
        }
    }
}
