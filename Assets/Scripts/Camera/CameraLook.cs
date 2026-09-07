using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f, _maxRotationX = 60;

    private float _rotationX;

    public void Rotate(Vector2 lookVector)
    {
        // Look up/down
        _rotationX -= lookVector.y * _rotationSpeed * Time.deltaTime;

        // Prevent the camera from flipping upside down
        _rotationX = Mathf.Clamp(
            _rotationX,
            -_maxRotationX,
            _maxRotationX
        );

        transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
    }
}