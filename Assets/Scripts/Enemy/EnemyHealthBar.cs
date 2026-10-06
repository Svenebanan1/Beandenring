using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthFill;

    private Transform _camera;

    private void Start()
    {
        _camera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        // Always face the camera
        transform.LookAt(
            transform.position + _camera.rotation * Vector3.forward,
            _camera.rotation * Vector3.up
        );
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        _healthFill.fillAmount = currentHealth / maxHealth;
    }
}