using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradig : MonoBehaviour
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradingIF = false;
    }

    public void AddDamage()
    {
        fireBall._dmg *= 1.15f;
        icePick._dmg *= 1.15f;
        slash._dmg *= 1.15f;
        sword._dmg *= 1.15f;
        upgradingIF = false;
    }

    public void AddFirerate()
    {
        shootSlash._spawnTimer -= 0.15f;
        shootSword._spawnTimer -= 0.15f;
        redRock._spawnTimer -= 0.15f;
        blueRock._spawnTimer -= 0.15f;
        upgradingIF = false;
    }

    public void AddSpeed()
    {
        fireBall._projectileSpeed *= 1.1f;
        icePick._projectileSpeed *= 1.1f;
        slash._projectileSpeed *= 1.1f;
        sword._projectileSpeed *= 1.1f;
        upgradingIF = false;
    }

    public IEnumerator Upgrading()
    {
        upgradingIF = true;
        yield break;
    }
}
