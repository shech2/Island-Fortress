using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Player playerInput = null;

    // 3d Character Controller
    private CharacterController controller;

    private Rigidbody rb;

    // Player Movement Speed
    public float speed = 12f;
    public Vector2 moveDirection;
    // Gravity
    public float gravity = -9.81f;
    public Vector3 velocity = Vector3.zero;

    // Jump
    public float jumpHeight = 3f;


    private Vector2 moveVector = Vector2.zero;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = new Player();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Movement.Movement.performed += OnMovementPerformed;
        playerInput.Movement.Movement.canceled += OnMovementCanceled;
        playerInput.Movement.Jump.performed += OnJumpPerformed;
        playerInput.Movement.Sprint.performed += OnSprintPerformed;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Movement.Movement.performed -= OnMovementPerformed;
        playerInput.Movement.Movement.canceled -= OnMovementCanceled;
        playerInput.Movement.Jump.performed -= OnJumpPerformed;
        playerInput.Movement.Sprint.performed -= OnSprintPerformed;


    }


    private void Sprint()
    {
        if (playerInput.Movement.Sprint.ReadValue<float>() > 0)
        {
            speed = 20f;
        }
        else
        {
            speed = 12f;
        }
    }

    private void Jump()
    {
        if (playerInput.Movement.Jump.ReadValue<float>() > 0 && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnMovementPerformed(InputAction.CallbackContext obj)
    {
        moveVector = obj.ReadValue<Vector2>();
        Debug.Log("Movement Performed: " + moveVector);
    }

    private void OnMovementCanceled(InputAction.CallbackContext obj)
    {
        moveVector = Vector2.zero;
        Debug.Log("Movement Canceled");
    }
    private void OnSprintPerformed(InputAction.CallbackContext obj)
    {
        Sprint();
    }


    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        Jump();
    }




    private void FixedUpdate()
    {
        Move();
        Jump();
        Sprint();
    }

    private void Move()
    {
        // move to to the camera direction
        Vector3 move = new Vector3(moveVector.x, 0, moveVector.y);
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0.0f;
        move.Normalize();
        move *= speed;
        controller.Move(move * Time.deltaTime);
    }

}
