using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyVariables _enemyVariables;

    [SerializeField] private GameObject _xpShard;

    private void Start()
    {
        _enemyVariables.hp = _enemyVariables.maxHp;
    }

    public void TakeDamage(float dmg)
    {
        _enemyVariables.hp -= dmg;

        if (_enemyVariables.hp <= 0)
        {
            Instantiate(_xpShard, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
