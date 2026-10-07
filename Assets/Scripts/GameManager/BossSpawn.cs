using System.Collections;
using TMPro;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float time;

    public GameObject bossPrefab;

    private bool bossSpawned = false;

    public GameObject waveManager;

    void Start()
    {
        time = 0f;
    }

    void Update()
    {
            time += Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (time >= 600 && !bossSpawned)
        {
            StartCoroutine(SpawnBoss());
            bossSpawned = true;
        }
    }

    IEnumerator SpawnBoss()
    {
        // Stop new enemies from spawning
        waveManager.SetActive(false);

        // Destroy every existing enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Wait one frame so they are actually removed
        yield return null;

        // Spawn boss
        Instantiate(bossPrefab, transform.position, Quaternion.identity);
    }
}