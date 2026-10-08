using UnityEngine;
using UnityEngine.InputSystem;  

public class Player : MonoBehaviour
{
    public InputActionAsset inputActions; // Reference to the Input Action Asset
    public CharacterController characterController; // Reference to the CharacterController component

    public Transform mainCamera;

    private InputAction moveAction, jumpAction, lookAction, runAction; // Reference to the Move action

    private Vector2 moveInput, lookInput; // Stores the player's movement input
    private Vector3 horizontalVelocity;

    [SerializeField] private float currentSpeed, walkSpeed = 8f, runSpeed = 12.5f, playerRotationDampening = 0.2f, turnSmoothVelocity = 10f, airAcceleration = 60f; 

    private float verticalVelocity; // Vertical velocity of the player
    private float gravity = -9.81f; // Gravity applied to the player
    private float jumpHeight = 5.5f; // Height of the player's jump

    [SerializeField] private Animator animator;

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

    private void Start()
    {
        currentSpeed = walkSpeed;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        lookInput = lookAction.ReadValue<Vector2>();

        OnRun();
        OnMove();
        OnJump();

        UpdateAnimation();
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

        Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        if (inputDirection.sqrMagnitude > 1f)
        {
            inputDirection.Normalize();
        }

        Vector3 cameraForward = mainCamera.forward;
        Vector3 cameraRight = mainCamera.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 desiredDirection =
            cameraForward * inputDirection.z +
            cameraRight * inputDirection.x;

        if (desiredDirection.sqrMagnitude > 1f)
        {
            desiredDirection.Normalize();
        }

        Vector3 desiredVelocity = desiredDirection * currentSpeed;

        if (characterController.isGrounded)
        {
            // Ground movement responds immediately.
            horizontalVelocity = desiredVelocity;
        }
        else if (desiredDirection.sqrMagnitude > 0f)
        {
            // Air movement gradually moves toward the requested direction.
            horizontalVelocity = Vector3.MoveTowards(
                horizontalVelocity,
                desiredVelocity,
                airAcceleration * Time.deltaTime
            );
        }

        if (horizontalVelocity.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(
                horizontalVelocity.x,
                horizontalVelocity.z
            ) * Mathf.Rad2Deg;

            float smoothAngle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                playerRotationDampening
            );

            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        }

        Vector3 completeVelocity =
            horizontalVelocity +
            Vector3.up * verticalVelocity;

        characterController.Move(completeVelocity * Time.deltaTime);
    }

    private void OnJump()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -3;

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
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }

    private void UpdateAnimation()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        bool isRunning = runAction.IsPressed();

        if (!isMoving)
        {
            animator.SetInteger("MovementState", 0);
        }
        else if (isRunning)
        {
            animator.SetInteger("MovementState", 2);
        }
        else
        {
            animator.SetInteger("MovementState", 1);
        }
    }

}
