using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;
    [SerializeField] private float maxHP;

    public UnityEngine.UI.Image healthImage;

    [SerializeField] private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHP = _playerValues.maxhp;
        _playerValues.hp = _playerValues.maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        _playerValues.hp = Mathf.Clamp(_playerValues.hp, 0, maxHP);

        float targetHealth = _playerValues.hp / maxHP;

        healthImage.fillAmount = Mathf.MoveTowards(
            healthImage.fillAmount,
            targetHealth,
            0.5f * Time.deltaTime
        );

        text.text = $"{_playerValues.hp} / {maxHP}";
    }

    public void TakeDamage(float dmg)
    {
        _playerValues.hp -= dmg;
    }

}
