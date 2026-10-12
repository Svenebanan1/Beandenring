using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyVariables _enemyVariables;

    [SerializeField] private GameObject _xpShard;
    [SerializeField] private GameObject bloodExplosionPrefab;

    private float _xpDropChance = 0.95f;
    private int _maxXpOnField = 25;

    private float bloodLifetime;

    public float hp;
    private WaveManager waveManager;

    [SerializeField] private EnemyHealthBar _healthBar;

    [SerializeField] private Renderer enemyRenderer;

    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float flashTime = 0.08f;

    private Color normalColor;
    private Coroutine _flashRoutine;
    private bool _dead;

    private void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        hp = _enemyVariables.maxHp * waveManager.enemyHPMultiplier;
        bloodLifetime = 1f;
        normalColor = enemyRenderer.material.color;
    }

    public void TakeDamage(float dmg)
    {
        if (_dead) return; // prevents double kills / double drops from simultaneous hits

        hp -= dmg;
        _healthBar.SetHealth(hp, _enemyVariables.maxHp);

        if (hp <= 0)
        {
            _dead = true;

            GameObject blood = Instantiate(bloodExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(blood, bloodLifetime);

            if (Random.value < _xpDropChance && XP.ActiveCount < _maxXpOnField)
            {
                Instantiate(_xpShard, transform.position, Quaternion.identity);
            }

            waveManager.kills += 1;
            Destroy(gameObject);
            return;
        }

        if (_flashRoutine != null) StopCoroutine(_flashRoutine);
        _flashRoutine = StartCoroutine(HitFlash());
    }

    private IEnumerator HitFlash()
    {
        enemyRenderer.material.color = hitColor;

        yield return new WaitForSeconds(flashTime);

        enemyRenderer.material.color = normalColor;
        _flashRoutine = null;
    }
}