using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;

    public UnityEngine.UI.Image healthImage;

    [SerializeField] private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerValues.hp = _playerValues.maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        float targetHealth = _playerValues.hp / _playerValues.maxhp;

        healthImage.fillAmount = Mathf.MoveTowards(
            healthImage.fillAmount,
            targetHealth,
            0.3f * Time.deltaTime
        );

        text.text = $"{_playerValues.hp}/{_playerValues.maxhp}";
    }

    public void TakeDamage(float dmg)
    {
        _playerValues.hp -= dmg;
    }

}
