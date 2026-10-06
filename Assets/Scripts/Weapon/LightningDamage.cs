using UnityEngine;

public class LightningDamage : MonoBehaviour
{
    private EnemyHP enemy;
    private float aoeDamage;

    [SerializeField] WeaponManager _weaponManager;

    public WeaponManager Manager => _weaponManager;

    public UpgradingScript upgrading;


    private void Start()
    {
        upgrading = GameObject.Find("UI Items Active").GetComponent<UpgradingScript>();
        aoeDamage = Manager.damage * upgrading.damageMultiplier;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(aoeDamage);

            Destroy(gameObject);
        }
    }
}
