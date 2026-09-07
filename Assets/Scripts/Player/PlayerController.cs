using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _moveSpeed, _runSpeed = 13f, _normalSpeed = 7f, _rotationSpeed = 5f, _jumpForce = 5f, _gravity = -11.5f  ;

    private float _rotationY;
    private float _verticalVelocity;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _moveSpeed = _normalSpeed;
    }

    public void Move(Vector2 movementVector)
    {
        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;

        // Ignore camera's vertical angle
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * movementVector.y +
                       right * movementVector.x;

        move *= _moveSpeed * Time.deltaTime;

        _characterController.Move(move);

        _verticalVelocity += _gravity * Time.deltaTime;

        _characterController.Move(
            new Vector3(0, _verticalVelocity, 0) * Time.deltaTime
        );
    }

    public void Rotate(Vector2 lookVector)
    {
        float rotation = lookVector.x * _rotationSpeed * Time.deltaTime;

        transform.Rotate(0f, rotation, 0f);
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = _jumpForce;
        }
    }

    public void Run()
    {
        _moveSpeed = _runSpeed;
    }

    public void StopRunning()
    {
        _moveSpeed = _normalSpeed;
    }
}