
using UnityEngine;
using UnityEditor;

public abstract class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    public bool useEvents;
    public string prompMessage;

    public void BaseInteract()
    {
        if (useEvents)
        {
            GetComponent<InteractionEvent>().onInteract.Invoke();
        }
        Interact();
    }

    protected virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }
}
