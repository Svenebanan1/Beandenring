using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyprefab; // Changed from GameObject to GameObject[]

    public Transform player;

    [SerializeField] private float spawninterval = 2f;
    [SerializeField] private float spawnradius = 15f;

    private float timer = 0f;

    public TextMeshProUGUI killCount;
    public float kills;

    [SerializeField] private float scaleFactor = 0.85f; // Factor by which the spawn interval decreases
    [SerializeField] private float scaletime = 60f; // Time interval after which the spawn interval decreases

    public float enemyHPMultiplier;
    public float enemyDMGMultiplier;

    private void Start()
    {
        enemyHPMultiplier = 1f;
        enemyDMGMultiplier = 1f;    
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        timer += Time.deltaTime;
        scaletime -= Time.deltaTime;

        if (scaletime <= 0f)
        {
            spawninterval *= scaleFactor;
            scaletime = 60f;
            ScaleEnemy();
        }

        if (timer >= spawninterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
        killCount.text = ": " + kills;
    }

    void SpawnEnemy()
    {
        GameObject enemy = enemyprefab[Random.Range(0, enemyprefab.Length)];

        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        randomDirection.y = 0f;

        Vector3 spawnPos = player.position + randomDirection * spawnradius;

        Instantiate(enemy, spawnPos, Quaternion.identity);
    }

    void ScaleEnemy()
    {
        enemyHPMultiplier *= 1.05f; // Increase HP multiplier by 5%
        enemyDMGMultiplier *= 1.1f; // Increase DMG multiplier by 10%
    }
}
