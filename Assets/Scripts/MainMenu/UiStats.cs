using TMPro;
using UnityEngine;

public class UiStats : MonoBehaviour
{
    private int angelsKilled;
    private int totalKills;
    private int maxKills;

    [SerializeField] private TextMeshProUGUI _angels;
    [SerializeField] private TextMeshProUGUI _maxKills;
    [SerializeField] private TextMeshProUGUI _totalKills;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        angelsKilled = PlayerPrefs.GetInt("Angels Killed", 0);
        totalKills = PlayerPrefs.GetInt("Total Kills", 0);
        maxKills = PlayerPrefs.GetInt("Highest Kills", 0);

        _angels.text = $"Angels Killed: {angelsKilled}";
        _totalKills.text = $"Total Kills: {totalKills}";
        _maxKills.text = $"Highest Kills: {maxKills}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
