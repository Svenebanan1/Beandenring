using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

    private Rigidbody _rb;
    private Transform _target;

    public EnemyHP enemy;

    public float _dmg;
    [SerializeField] private float _lifeTime;
    public float _projectileSpeed;

    private void Start()
    {
        _projectileSpeed = _weaponManager.projectileSpeed;
        _lifeTime = _weaponManager.projectileLifetime;

        _dmg = _weaponManager.damage;
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, _lifeTime);

        float force = Random.Range(5f, 20f);
        _rb.AddTorque(Vector3.up * force, ForceMode.Impulse);
    }


    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 direction =
            (_target.position - transform.position).normalized;

        _rb.linearVelocity = direction * _projectileSpeed;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();

            if (enemy != null)
            {
                enemy.TakeDamage(_dmg);

                // Stop homing, but continue flying forward.
                if (other.transform == _target || other.transform.IsChildOf(_target))
                {
                    _target = null;
                }
            }

        }
    }
}

