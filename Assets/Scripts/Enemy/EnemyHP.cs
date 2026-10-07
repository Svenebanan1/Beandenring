using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyVariables _enemyVariables;

    [SerializeField] private GameObject _xpShard;
    [SerializeField] private GameObject bloodExplosionPrefab;

    private float bloodLifetime;

    public float hp;
    private WaveManager waveManager;

    [SerializeField] private EnemyHealthBar _healthBar;

    [SerializeField] private Renderer enemyRenderer;

    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float flashTime = 0.08f;

    private Color normalColor;

    private void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        hp = _enemyVariables.maxHp * waveManager.enemyHPMultiplier; 
        bloodLifetime = 1f;
        normalColor = enemyRenderer.material.color;
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        _healthBar.SetHealth(hp, _enemyVariables.maxHp);
        StartCoroutine(HitFlash());
        if (hp <= 0)
        {
            GameObject blood = Instantiate(bloodExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(blood, bloodLifetime);
            Instantiate(_xpShard, transform.position, Quaternion.identity);
            waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
            waveManager.kills += 1;
            Destroy(gameObject);
        }
    }

    private IEnumerator HitFlash()
    {
        enemyRenderer.material.color = hitColor;

        yield return new WaitForSeconds(flashTime);

        enemyRenderer.material.color = normalColor;
    }
}
