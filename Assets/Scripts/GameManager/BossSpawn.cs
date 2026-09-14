using TMPro;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 600f;
    }

    void Update()
    {
        time -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
