using Unity.VisualScripting;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private float _enemyHP;
    [SerializeField] private float _enemyMaxHP = 50f;

    private void Start()
    {
        _enemyHP = _enemyMaxHP;
    }

    public void TakeDamage(float dmg)
    {
        _enemyHP -= dmg;
    }
}
