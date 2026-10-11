using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;
    public float maxHP;
    public float currentHP;
    [SerializeField] private float _timeBeforeRegen;
    [SerializeField] private bool _canRegen;

    public UnityEngine.UI.Image healthImage;

    [SerializeField] private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHP = _playerValues.maxhp;
        currentHP = _playerValues.hp;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        _timeBeforeRegen = Mathf.Clamp(_timeBeforeRegen, 0, 4);

        _timeBeforeRegen -= Time.deltaTime;

        float targetHealth = currentHP / maxHP;

        healthImage.fillAmount = Mathf.MoveTowards(
            healthImage.fillAmount,
            targetHealth,
            0.5f * Time.deltaTime
        );

        text.text = $"{currentHP} / {maxHP}";

        if(_timeBeforeRegen <= 0 && currentHP != maxHP)
        {
            _canRegen = true;
            if(_canRegen)
            {
                currentHP += 1;
                _timeBeforeRegen = 0.15f;
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        _timeBeforeRegen = 4f;
        _canRegen = false;
    }

}
