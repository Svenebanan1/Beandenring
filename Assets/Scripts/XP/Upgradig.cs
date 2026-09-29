using System.Collections;
using UnityEngine;

public class Upgradig : MonoBehaviour
{
    public bool upgradingIF;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradingIF = false;
    }

    void AddDamage()
    {
        upgradingIF = false;
    }

    void AddFirerate()
    {
        upgradingIF = false;
    }

    void AddSpeed()
    {
        upgradingIF = false;
    }

    public IEnumerator Upgrading()
    {
        upgradingIF = true;
        yield break;
    }
}
