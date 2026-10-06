using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradingScript : MonoBehaviour
{
    public bool upgradingIF;

    public GameObject levelPanel;
    public GameObject unlockPanel;

    public bool swordUnlocked;
    public bool slashUnlocked;
    public bool lightningUnlocked;

    public GameObject swordactivation;
    public GameObject slashactivation;
    public GameObject lightningactivation;

    public GameObject swordButton;
    public GameObject slashButton;
    public GameObject lightButton;

    public GameObject swordIcon;
    public GameObject slashIcon;
    public GameObject lightningIcon;

    public GameObject dmgbutton;
    public GameObject fireratebutton;
    public GameObject speedbutton;

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
        lightningactivation.SetActive(false);

        swordIcon.SetActive(false);
        slashIcon.SetActive(false);
        lightningIcon.SetActive(false);

        damageMultiplier = 1f;
        firefateMultiplier = 1f;
        speedMultiplier = 1f;
    }

    private void Update()
    {
        dmg.SetText("Damage Multiplier: {0:0.00}", damageMultiplier);
        firerate.SetText("Firerate Multiplier: {0:0.00}", firefateMultiplier);
        speed.SetText("Projectile Speed Multiplier: {0:0.00}", speedMultiplier);

        if (firefateMultiplier == 0.1f)
        {
            fireratebutton.SetActive(false);
        }
      
        if (speedMultiplier == 2.6f)
        {
            speedbutton.SetActive(false);
        }


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

        swordIcon.SetActive(true);
        swordIcon.transform.position = IconLocation[0].transform.position;
        IconLocation.RemoveAt(0);

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

        slashIcon.SetActive(true);
        slashIcon.transform.position = IconLocation[0].transform.position;
        IconLocation.RemoveAt(0);

        unlockPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Destroy(slashButton);
    }

    public void unlockLightning()
    {
        lightningUnlocked = true;
        lightningactivation.SetActive(true);

        lightningIcon.SetActive(true);
        lightningIcon.transform.position = IconLocation[0].transform.position;
        IconLocation.RemoveAt(0);

        unlockPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Destroy(lightButton);
    }

    public IEnumerator Upgrading()
    {
        upgradingIF = true;
        yield return null;
    }

}


