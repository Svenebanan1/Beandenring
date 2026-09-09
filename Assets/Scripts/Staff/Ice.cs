using UnityEngine;

public class Ice : MonoBehaviour
{
    [SerializeField] private float _speed, _iceDamage = 7f, _slowingEffect = 0.5f;

    private Rigidbody _rb;
    private Transform _target;

    public EnemyHP enemy;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(_iceDamage);
            Destroy(gameObject);
        }
    }
}