using UnityEngine;

public class AOE : MonoBehaviour
{
    private EnemyHP enemy;
    private float aoeDamage;

    [SerializeField] WeaponManager _weaponManager;

    public WeaponManager Manager => _weaponManager;

    public UpgradingScript upgrading;


    private void Start()
    {
        upgrading = GameObject.Find("UI Items Active").GetComponent<UpgradingScript>();
        aoeDamage = Manager.damage * upgrading.damageMultiplier * 0.75f;

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
