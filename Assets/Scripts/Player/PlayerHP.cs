using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;
    public float maxHP;
    public float currentHP;
    [SerializeField] private float _timeBeforeRegen;
    [SerializeField] private bool _canRegen;

    public UnityEngine.UI.Image healthImage;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private Animator _animator;

    public float canTakeDmg;

    public GameObject UI;

    [SerializeField] private Player player;
    [SerializeField] private RangeCheck rangecheck;
    [SerializeField] private ShootSword shootsword;
    [SerializeField] private ShootSlash shootSlash;
    [SerializeField] private LightningStrike lightningstrike;
    [SerializeField] private BlueRock bluerock;
    [SerializeField] private RedRock redrock;
    [SerializeField] private PlayerLvL playerlvl;
    [SerializeField] private SwitchRocks rockmanager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHP = _playerValues.maxhp;
        currentHP = _playerValues.hp;
        currentHP = maxHP;

        canTakeDmg = 0.5f;

        UI.SetActive(false);
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

        text.text = $"{(int)currentHP} / {(int)maxHP}";

        if (_timeBeforeRegen <= 0 && currentHP != maxHP)
        {
            _canRegen = true;
            if(_canRegen)
            {
                currentHP += 1;
                _timeBeforeRegen = 0.15f;
            }
        }

        canTakeDmg -= Time.deltaTime;
    }

    public void TakeDamage(float dmg)
    {
        if (canTakeDmg <= 0)
        {
            currentHP -= dmg;
            canTakeDmg = 0.5f;
        }      
        _timeBeforeRegen = 4f;
        _canRegen = false;

        if (currentHP <= 0)
        {
            player.enabled = false;
            playerlvl.enabled = false;
            shootSlash.enabled = false;
            shootsword.enabled = false;
            lightningstrike.enabled = false;
            bluerock.enabled = false;
            redrock.enabled = false;
            rangecheck.enabled = false;
            rockmanager.enabled = false;

           StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        Time.timeScale = 0.5f;
        _animator.SetTrigger("Die");
        UI.SetActive(true);
        yield return new WaitForSeconds(2f);
        
        SceneManager.LoadScene("MainMenu");
    }

}
