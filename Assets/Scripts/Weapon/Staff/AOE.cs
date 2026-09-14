using UnityEngine;

public class AOE : MonoBehaviour
{
    private EnemyHP enemy;
    private float aoeDamage;

    [SerializeField] WeaponManager _weaponManager;


    private void Start()
    {
        aoeDamage = _weaponManager.damage * 0.75f;
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
