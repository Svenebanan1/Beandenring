using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

    public WeaponManager Manager => _weaponManager;

    private Rigidbody _rb;
    private Transform _target;

    public RangedEnemyAI rangedEnemyAI;
    public EnemyHP enemy;

    public float _dmg;

    [SerializeField] private GameObject _targetGameObject;
    [SerializeField] private float _lifeTime;
    public float _projectileSpeed;

    public UpgradingScript upgrading;

    private void Start()
    {
        upgrading = GameObject.Find("UI Items Active").GetComponent<UpgradingScript>();
        _projectileSpeed = Manager.projectileSpeed * upgrading.speedMultiplier;
        _dmg = Manager.damage * upgrading.damageMultiplier;
        _lifeTime = _weaponManager.projectileLifetime;

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
            enemy = other.gameObject.GetComponent<EnemyHP>();

            enemy.TakeDamage(_dmg);

            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("RangedEnemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();
            rangedEnemyAI = other.gameObject.GetComponent<RangedEnemyAI>();

            rangedEnemyAI.SlowEffect();

            enemy.TakeDamage(_dmg);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("TankEnemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();
            TankEnemyAI tankEnemyAI = other.gameObject.GetComponent<TankEnemyAI>();
            tankEnemyAI.SlowEffect();
            enemy.TakeDamage(_dmg);
            Destroy(gameObject);
        }
    }

}

