using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public List<GameObject> IconLocation = new List<GameObject>();

    public TextMeshProUGUI dmg;
    public TextMeshProUGUI firerate;
    public TextMeshProUGUI speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradingIF = false;

        swordactivation.SetActive(false);
        slashactivation.SetActive(false);

        swordIcon.SetActive(false);
        slashIcon.SetActive(false);

        damageMultiplier = 1f;
        firefateMultiplier = 1f;
        speedMultiplier = 1f;
    }

    private void Update()
    {
        dmg.SetText("Damage Multiplier: {0:0.00}", damageMultiplier);
        firerate.SetText("Firerate Multiplier: {0:0.00}", firefateMultiplier);
        speed.SetText("Projectile Speed Multiplier: {0:0.00}", speedMultiplier);
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

}


