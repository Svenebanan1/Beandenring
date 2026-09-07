using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _maxRotationX = 60f;

    private float _rotationX;

    public void Rotate(Vector2 lookVector)
    {
        _rotationX -= lookVector.y * _rotationSpeed * Time.deltaTime;

        _rotationX = Mathf.Clamp(
            _rotationX,
            -_maxRotationX,
            _maxRotationX
        );

        transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
    }
}
