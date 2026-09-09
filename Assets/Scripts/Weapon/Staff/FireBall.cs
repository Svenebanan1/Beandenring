using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

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

        _weaponManager.projectileLifetime -= Time.deltaTime;
        if(_weaponManager.projectileLifetime <= 0)
        {
            Destroy(gameObject);
        }

        Vector3 direction =
            (_target.position - transform.position).normalized;

        _rb.linearVelocity = direction * _weaponManager.projectileSpeed;

        transform.rotation =
            Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(_weaponManager.damage);
            Destroy(gameObject);
        }
    }
}

