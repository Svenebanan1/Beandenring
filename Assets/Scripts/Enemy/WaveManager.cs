using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyprefab;

    [Tooltip("Seconds into the game when each prefab can start spawning. Same order/length as enemyprefab. 90 = 1:30, 180 = 3:00")]
    [SerializeField] private float[] unlockTimes;

    public Transform player;

    [SerializeField] private float spawninterval = 2f;
    [SerializeField] private float spawnradius = 15f;

    private float timer = 0f;
    private float gameTime = 0f;

    public TextMeshProUGUI killCount;
    public float kills;

    [SerializeField] private float scaleFactor = 0.85f;
    [SerializeField] private float maxScaleTime = 45f;
    [SerializeField] private float scaletime;
    [SerializeField] private float scaleAmount = 0f;

    public float enemyHPMultiplier;
    public float enemyDMGMultiplier;
    public float enemyAmount = 1f;

    [SerializeField, Range(0f, 0.8f)]
    private float rareEnemyChance = 0.2f;

    private readonly List<GameObject> _available = new List<GameObject>();

    private void Start()
    {
        enemyHPMultiplier = 1f;
        enemyDMGMultiplier = 1f;
        scaletime = maxScaleTime;
    }

    void Update()
    {
        if (player == null) return;

        gameTime += Time.deltaTime;
        timer += Time.deltaTime;
        scaletime -= Time.deltaTime;

        if (scaletime <= 0f)
        {
            spawninterval *= scaleFactor;
            scaletime = maxScaleTime *= 0.85f;
            ScaleEnemy();
            scaleAmount++;
        }

        if (timer >= spawninterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
        killCount.text = ": " + kills;
    }

    bool IsUnlocked(int index)
    {
        // Prefabs with no unlock time set are available from the start
        if (unlockTimes == null || index >= unlockTimes.Length) return true;
        return gameTime >= unlockTimes[index];
    }

    GameObject PickEnemy()
    {
        int last = enemyprefab.Length - 1;

        // Rare enemy (last prefab), only if it has unlocked
        if (last > 0 && IsUnlocked(last) && Random.value < rareEnemyChance)
            return enemyprefab[last];

        // Common enemies (all except the last), only the unlocked ones
        _available.Clear();
        for (int i = 0; i < last; i++)
            if (IsUnlocked(i)) _available.Add(enemyprefab[i]);

        if (_available.Count == 0) return enemyprefab[0]; // safety fallback
        return _available[Random.Range(0, _available.Count)];
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject enemy = PickEnemy();

            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            randomDirection.y = 0f;

            Vector3 spawnPos = player.position + randomDirection * spawnradius;
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }

    void ScaleEnemy()
    {
        enemyHPMultiplier *= 1.15f;
        enemyDMGMultiplier *= 1.2f;
        if (scaleAmount == 2 || scaleAmount == 4  || scaleAmount == 5 ||scaleAmount == 6 || scaleAmount == 7 || scaleAmount == 8 || scaleAmount == 9 || scaleAmount == 10)
            enemyAmount += 1f;
    }
}