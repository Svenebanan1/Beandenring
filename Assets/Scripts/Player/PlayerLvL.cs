using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Internal;
using UnityEditor.Experimental.GraphView;

public class PlayerLvL : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;
    [SerializeField] private float _maxXP;
    [SerializeField] private float _currentXP;
    [SerializeField] private int _currentLevel;

    [SerializeField] private Image _xpBar;

    [SerializeField] private float _fillSpeed = 2f;

    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private float _targetFill;

    public GameObject levelPanel;
    public GameObject unlockPanel;

    public UpgradingScript upgrading;

    private bool maxLevelReached;

    private void Start()
    {
        _maxXP = _playerValues.xpTillNextLvL;
        _currentXP = _playerValues.xp;
        _currentLevel = _playerValues.lvl;

        _currentLevel = 0;
        _currentXP = 0f;

        UpdateTargetFill();
        _xpBar.fillAmount = _targetFill;

        maxLevelReached = false;

    }

    private void Update()
    {
        _textMeshProUGUI.text = $"LVL : {_currentLevel}";

        _xpBar.fillAmount = Mathf.MoveTowards(
            _xpBar.fillAmount,
            _targetFill,
            _fillSpeed * Time.deltaTime
        );
        if(upgrading.swordUnlocked && upgrading.slashUnlocked)
        {
            Destroy(unlockPanel);
        } 
        
        if(_currentLevel == 25)
        {
            maxLevelReached = true;
        }

    }

    private void LevelUp()
    {
        _currentLevel++;
        _maxXP += 5;
        _maxXP *= 1.02f;
    }

    public void GainedXP()
    {
        if (maxLevelReached)
            return;

        _currentXP += 15f;

        while (_currentXP >= _maxXP)
        {
            _currentXP -= _maxXP;
            LevelUp();
            StartCoroutine(upgrading.Upgrading());
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (_currentLevel == 5 || _currentLevel == 10)
            {
                unlockPanel.SetActive(true);
                Time.timeScale = 0f;
               break;
            }
            if (upgrading.upgradingIF == true)
            {
                levelPanel.SetActive(true);
                Time.timeScale = 0f;
            }
           
        }

        UpdateTargetFill();
    }

    private void UpdateTargetFill()
    {
        _targetFill = _currentXP / _maxXP;
    }
}