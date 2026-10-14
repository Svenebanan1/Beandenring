
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] enemyprefab;
    [SerializeField] private float[] unlockTimes;
    public Transform player;

    [Header("Spawning")]
    [SerializeField] private float spawninterval = 3f;
    [SerializeField] private float spawnradius = 15f;

    [SerializeField] private int startingEnemyCap = 15;
    [SerializeField] private int maximumEnemyCap = 25;

    private int currentEnemyCap;

    private const float minSpawnInterval = 0.5f;
    private const int maxEnemiesPerSpawn = 50;

    private readonly List<GameObject> aliveEnemies = new();

    [Header("Timers")]
    private float timer = 0f;
    private float gameTime = 0f;

    [SerializeField] private float maxScaleTime = 40f;
    [SerializeField] private float scaletime;

    [SerializeField, Range(0.1f, 1f)]
    private float scaleFactor = 0.85f;

    [Header("Enemy Scaling")]
    public float enemyHPMultiplier = 1f;
    public float enemyDMGMultiplier = 1f;
    public float enemyAmount = 1f;

    [Header("Other")]
    public TextMeshProUGUI killCount;
    public int kills;

    [SerializeField, Range(0f, 0.8f)]
    private float rareEnemyChance = 0.2f;

    private readonly List<GameObject> _available = new();

    private void Start()
    {
        enemyHPMultiplier = 1f;
        enemyDMGMultiplier = 1f;

        maxScaleTime = Mathf.Max(1f, maxScaleTime);
        scaletime = maxScaleTime;

        spawninterval = Mathf.Max(minSpawnInterval, spawninterval);

        currentEnemyCap = Mathf.Clamp(
            startingEnemyCap, 1, maximumEnemyCap
        );
    }

    private void Update()
    {
        if (player == null) return;

        float delta = Time.deltaTime;

        gameTime += delta;
        timer += delta;
        scaletime -= delta;

        // Remove references to destroyed enemies
        aliveEnemies.RemoveAll(enemy => enemy == null);

        // Scale difficulty at fixed intervals
        if (scaletime <= 0f)
        {
            ScaleEnemy();
            scaletime += maxScaleTime;
        }

        // Spawn when timer expires OR arena is empty
        if (timer >= spawninterval || aliveEnemies.Count == 0)
        {
            SpawnEnemy();
            timer = 0f;
        }

        if (killCount != null)
            killCount.text = ": " + kills;
    }

    private bool IsUnlocked(int index)
    {
        if (unlockTimes == null || index >= unlockTimes.Length)
            return true;

        return gameTime >= unlockTimes[index];
    }

    private GameObject PickEnemy()
    {
        if (enemyprefab == null || enemyprefab.Length == 0)
            return null;

        int last = enemyprefab.Length - 1;

        if (last > 0 && IsUnlocked(last)
            && enemyprefab[last] != null
            && Random.value < rareEnemyChance)
        {
            return enemyprefab[last];
        }

        _available.Clear();

        int commonCount = last == 0 ? 1 : last;

        for (int i = 0; i < commonCount; i++)
        {
            if (enemyprefab[i] != null && IsUnlocked(i))
                _available.Add(enemyprefab[i]);
        }

        if (_available.Count > 0)
        {
            return _available[
                Random.Range(0, _available.Count)
            ];
        }

        return IsUnlocked(last) ? enemyprefab[last] : null;
    }

    private void SpawnEnemy()
    {
        // Never exceed the current enemy cap
        int remainingSlots = currentEnemyCap - aliveEnemies.Count;

        if (remainingSlots <= 0) return;

        int spawnCount = Mathf.Min(
            Mathf.RoundToInt(enemyAmount),
            remainingSlots
        );

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemyPrefab = PickEnemy();

            if (enemyPrefab == null) return;

            Vector2 direction = Random.insideUnitCircle.normalized;

            Vector3 spawnPos = player.position + new Vector3(
                direction.x * spawnradius,
                0f,
                direction.y * spawnradius
            );

            GameObject spawnedEnemy = Instantiate(
                enemyPrefab,
                spawnPos,
                Quaternion.identity
            );

            aliveEnemies.Add(spawnedEnemy);
        }
    }

    private void ScaleEnemy()
    {
        // Increase enemy stats
        enemyHPMultiplier *= 1.075f;
        enemyDMGMultiplier *= 1.1f;

        // Gradually increase enemies per spawn
        enemyAmount = Mathf.Min(
            enemyAmount + 1f,
            maxEnemiesPerSpawn
        );

        // Gradually increase maximum alive enemies
        currentEnemyCap = Mathf.Min(
            currentEnemyCap + 1,
            maximumEnemyCap
        );

        // Reduce spawn interval, never below 0.5 seconds
        spawninterval = Mathf.Max(
            minSpawnInterval,
            spawninterval * scaleFactor
        );
    }
}
