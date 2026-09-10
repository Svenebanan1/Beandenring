using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

    private Rigidbody _rb;
    private Transform _target;

    public EnemyHP enemy;

    [SerializeField] private SphereCollider _aoeCollider;

    [SerializeField] private float _dmg;
    [SerializeField] private float _aoeDamage;

    private void Start()
    {
        _aoeDamage = _weaponManager.damage * 0.75f;
        _dmg = _weaponManager.damage;
        _aoeCollider.enabled = false;
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, _weaponManager.projectileLifetime);
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

        _rb.linearVelocity = direction * _weaponManager.projectileSpeed;

        transform.rotation =
            Quaternion.LookRotation(direction);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _aoeCollider.enabled = true;
            enemy = other.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(_dmg);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(_aoeDamage);
        }
    }
}

