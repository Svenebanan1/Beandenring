using UnityEngine;

public class LightningDamage : MonoBehaviour
{
    private EnemyHP enemy;
    private float aoeDamage;

    [SerializeField] WeaponManager _weaponManager;

    public WeaponManager Manager => _weaponManager;

    public UpgradingScript upgrading;

    private float lifetime = 0.5f;


    private void Start()
    {
        upgrading = GameObject.Find("UI Items Active").GetComponent<UpgradingScript>();
        aoeDamage = Manager.damage * upgrading.damageMultiplier;

        Destroy(gameObject, lifetime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();

            enemy.TakeDamage(aoeDamage);

            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("RangedEnemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();

            enemy.TakeDamage(aoeDamage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("TankEnemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(aoeDamage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            BossHP boss = other.gameObject.GetComponent<BossHP>();
            boss.TakeDamage(aoeDamage);
            Destroy(gameObject);
        }
    }
}
