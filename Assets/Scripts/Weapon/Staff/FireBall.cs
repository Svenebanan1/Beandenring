using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

    private Rigidbody _rb;
    private Transform _target;

    public EnemyHP enemy;

    [SerializeField] private float _dmg;

    [SerializeField] private GameObject _targetGameObject;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _projectileSpeed;

    private void Start()
    {
        _projectileSpeed = _weaponManager.projectileSpeed;
        _lifeTime = _weaponManager.projectileLifetime;

        _dmg = _weaponManager.damage;
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, _lifeTime);
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

        _rb.linearVelocity = direction * _projectileSpeed;

        transform.rotation =
            Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Fix: Instantiate the GameObject, not its position
            Instantiate(_targetGameObject, transform.position, Quaternion.identity);
            enemy = other.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(_dmg);
            Destroy(gameObject);
        }
    }

}

