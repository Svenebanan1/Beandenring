using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

    public WeaponManager Manager => _weaponManager;

    private Rigidbody _rb;
    private Transform _target;

    private float scale = 4;

    public EnemyHP enemy;

    private Vector3 _direction;

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

    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;
    }


    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        transform.position +=
           _direction * _projectileSpeed * Time.deltaTime;

        transform.localScale += Vector3.one * scale * Time.deltaTime;
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

