using UnityEngine;

public class Ice : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

    private Rigidbody _rb;
    private Transform _target;

    public EnemyHP enemy;
    public EnemyAI enemyAI;

    private void Start()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();
            enemyAI = other.gameObject.GetComponent<EnemyAI>();

            enemyAI.SlowEffect();

            enemy.TakeDamage(_weaponManager.damage);

            Destroy(gameObject);
        }
    }
}