using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

    public WeaponManager Manager => _weaponManager;

    private Rigidbody _rb;
    private Transform _target;

    public EnemyHP enemy;
    public BossHP bossHP;

    public float _dmg;
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
        bool hit = false;

        if (other.CompareTag("Enemy") || other.CompareTag("RangedEnemy") || other.CompareTag("TankEnemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null) { enemy.TakeDamage(_dmg); hit = true; }
        }
        else if (other.CompareTag("Boss"))
        {
            var boss = other.GetComponent<BossHP>();
            if (boss != null) { boss.TakeDamage(_dmg); hit = true; }
        }

        if (hit && _target != null &&
            (other.transform == _target || other.transform.IsChildOf(_target) || _target.IsChildOf(other.transform)))
        {
            _target = null;
        }
    }
}

