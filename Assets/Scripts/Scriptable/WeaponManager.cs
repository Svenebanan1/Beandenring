using UnityEngine;

[CreateAssetMenu(fileName = "WeaponManager", menuName = "Scriptable Objects/WeaponManager")]
public class WeaponManager : ScriptableObject
{
    public float damage;

    public float projectileSpeed;

    public float fireRate;

    public float projectileAmount;

    public float projectileLifetime;

    public float range;
}
