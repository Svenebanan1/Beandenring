using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyprefab; // Changed from GameObject to GameObject[]

    public Transform player;

    public float spawninterval = 2f;
    public float spawnradius = 12f;

    private float timer = 0f;

    public TextMeshProUGUI killCount;
    public float kills;

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        timer += Time.deltaTime;

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
}
