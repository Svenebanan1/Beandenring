using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingScript : MonoBehaviour
{
    public bool upgradingIF;
    public FireBall fireBall;
    public Ice icePick;
    public Slash slash;
    public Sword sword;
    public ShootSlash shootSlash;
    public ShootSword shootSword;
    public RedRock redRock;
    public BlueRock blueRock;

    public GameObject levelPanel;
    public GameObject unlockPanel;

    public bool swordUnlocked;
    public bool slashUnlocked;

    public GameObject swordactivation;
    public GameObject slashactivation;

    public GameObject swordButton;
    public GameObject slashButton;

    public GameObject swordIcon;
    public GameObject slashIcon;

    public float damageMultiplier;
    public float firefateMultiplier;
    public float speedMultiplier;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradingIF = false;

        swordactivation.SetActive(false);
        slashactivation.SetActive(false);

        swordIcon.SetActive(false);
        slashIcon.SetActive(false);

        damageMultiplier = 1;
        firefateMultiplier = 1;
        speedMultiplier = 1;
    }

    public void AddDamage()
    {
        damageMultiplier += 0.15f;
        upgradingIF = false;
        levelPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AddFirerate()
    {
        firefateMultiplier -= 0.1f;
        upgradingIF = false;
        levelPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AddSpeed()
    {
        speedMultiplier += 0.2f;
        upgradingIF = false;
        levelPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void unlockSword()
    {
        swordUnlocked = true;
        swordactivation.SetActive(true);

        AddIcon(swordIcon);

        unlockPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Destroy(swordButton);
    }

    public void unlockSlash()
    {
        slashUnlocked = true;
        slashactivation.SetActive(true);

        AddIcon(slashIcon);

        unlockPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Destroy(slashButton);
    }

    public IEnumerator Upgrading()
    {
        upgradingIF = true;
        yield return null;
    }

    [SerializeField] private RectTransform lastIcon;
    [SerializeField] private float iconSpacing = 80f;

    private void AddIcon(GameObject icon)
    {
        RectTransform newIcon = icon.GetComponent<RectTransform>();

        newIcon.anchoredPosition = new Vector2(
            lastIcon.anchoredPosition.x,
            lastIcon.anchoredPosition.y - iconSpacing
        );

        icon.SetActive(true);

        lastIcon = newIcon;
    }
}


