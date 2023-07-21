using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private float xRotation = 0f;
    public float mouseSensitivity = 10f;

    public Vector2 moveDirection;


    private void Awake()
    {

        cam = GetComponentInChildren<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse Input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        // Rotate Player
        transform.Rotate(Vector3.up * mouseX);


        // Rotate Camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        //make the camera Smooth
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }
}
