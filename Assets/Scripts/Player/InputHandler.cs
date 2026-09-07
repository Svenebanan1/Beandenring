using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController playerController;
    public CameraController cameraController;
    public SwitchRocks switchRocks;

    private InputAction _moveAction, _lookAction, _jumpAction, _runAction, _switchRockAction;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _runAction = InputSystem.actions.FindAction("Run");
        _switchRockAction = InputSystem.actions.FindAction("SwitchRock");

        _jumpAction.performed += OnJumpPerformed;
        _runAction.performed += OnRunPerformed;
        _runAction.canceled += OnRunCanceled;
        _switchRockAction.performed += OnSwitchRockPerformed;

        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        Vector2 movementVector = _moveAction.ReadValue<Vector2>();
        playerController.Move(movementVector);

        Vector2 lookVector = _lookAction.ReadValue<Vector2>();

        // Player = left/right
        playerController.Rotate(lookVector);

        // Camera = up/down
        cameraController.Rotate(lookVector);
    }

     private void OnSwitchRockPerformed(InputAction.CallbackContext context)
    {
        switchRocks.OnSwitch();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        playerController.Jump();
    }

    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        playerController.Run();
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        playerController.StopRunning();
    }

}