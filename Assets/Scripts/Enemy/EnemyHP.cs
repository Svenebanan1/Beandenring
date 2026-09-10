using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyVariables _enemyVariables;

    [SerializeField] private GameObject _xpShard;

    float hp;

    private void Start()
    {
        hp = _enemyVariables.maxHp;
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Instantiate(_xpShard, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
