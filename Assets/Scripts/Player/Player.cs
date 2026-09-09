using UnityEngine;
using UnityEngine.InputSystem;  

public class Player : MonoBehaviour
{
    public InputActionAsset inputActions; // Reference to the Input Action Asset
    public CharacterController characterController; // Reference to the CharacterController component

    public Transform mainCamera;

    private InputAction moveAction; // Reference to the Move action
    private InputAction jumpAction; // Reference to the Jump action
    private InputAction lookAction; // Reference to the Look action
    private InputAction runAction;

    private Vector2 moveInput; // Stores the player's movement input
    private Vector2 lookInput; // Stores the player's look input

    private float walkSpeed = 8f; // Walking speed of the player
    private float runSpeed = 12.5f; // Run speed of player
    private float playerRotationDampening = 0.2f; // Damping factor for player rotation
    private float turnSmoothVelocity; // Velocity used for smoothing the player's rotation

    private float verticalVelocity; // Vertical velocity of the player
    private float gravity = -9.81f; // Gravity applied to the player
    private float jumpHeight = 4f; // Height of the player's jump

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        lookAction = InputSystem.actions.FindAction("Look");
        runAction = InputSystem.actions.FindAction("Run");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        lookInput = lookAction.ReadValue<Vector2>();

        OnMove();
        OnJump();
        OnRun();
    }

    private void OnEnable()
    {
        // Enable the input actions when the player is enabled
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions when the player is disabled
        inputActions.FindActionMap("Player").Disable();
    }

    private void OnMove()
    {
        Vector3 playerDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        Vector3 verticalMove = new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime;

        if(playerDirection.magnitude > 0f )
        {
            float targetAngle = Mathf.Atan2(playerDirection.x, playerDirection.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float smoothTargetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, playerRotationDampening);

            transform.rotation = Quaternion.Euler(0f, smoothTargetAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            characterController.Move(moveDirection.normalized * walkSpeed * Time.deltaTime + verticalMove);
        }
        else
        {
            characterController.Move(verticalMove);
        }
            
    }

    private void OnJump()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -1;

            if (jumpAction.WasPressedThisFrame())
            {
                verticalVelocity = jumpHeight;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void OnRun()
    {
        if(runAction.IsPressed())
        {
            walkSpeed = runSpeed;
        }
        else
        {
            walkSpeed = 8f;
        }
    }

}
