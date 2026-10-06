using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyVariables _enemyVariables;

    [SerializeField] private GameObject _xpShard;
    [SerializeField] private GameObject bloodExplosionPrefab;

    private float bloodLifetime;

    public float hp;
    private WaveManager waveManager;

    [SerializeField] private EnemyHealthBar _healthBar;

    private void Start()
    {
        waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();
        hp = _enemyVariables.maxHp * waveManager.enemyHPMultiplier; 
        bloodLifetime = 1f;
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        _healthBar.SetHealth(hp, _enemyVariables.maxHp);

        if (hp <= 0)
        {
            GameObject blood = Instantiate(bloodExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(blood, bloodLifetime);
            Instantiate(_xpShard, transform.position, Quaternion.identity);
            waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();
            waveManager.kills += 1;
            Destroy(gameObject);
        }
    }
}
