using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float distance = 3f;

    [SerializeField]
    private LayerMask mask;
    private PlayerUI PlayerUI;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        PlayerUI = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance, mask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                PlayerUI.ShowInteractText(interactable.prompMessage);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.BaseInteract();
                }
            }
        }
        else
        {
            PlayerUI.HideInteractText();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * distance);
    }
}
