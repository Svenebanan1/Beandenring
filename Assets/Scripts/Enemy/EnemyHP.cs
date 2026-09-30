using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyVariables _enemyVariables;

    [SerializeField] private GameObject _xpShard;
    [SerializeField] private GameObject bloodExplosionPrefab;

    private float bloodLifetime;

    float hp;

    private void Start()
    {
        hp = _enemyVariables.maxHp;
        bloodLifetime = 1f;
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            GameObject blood = Instantiate(bloodExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(blood, bloodLifetime);
            Instantiate(_xpShard, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
