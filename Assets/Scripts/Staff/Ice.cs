using UnityEngine;

public class Ice : MonoBehaviour
{
    [SerializeField] private float _speed = 35f;

    private Rigidbody _rb;
    private Transform _target;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction =
            (_target.position - transform.position).normalized;

        _rb.linearVelocity = direction * _speed;

        transform.rotation =
            Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}