using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

    // List to keep track of enemies in range
    private List<Transform> _enemiesInRange = new List<Transform>();

    // Property to check if there are any enemies in range
    public bool isInRange => _enemiesInRange.Count > 0;

    [SerializeField] private CapsuleCollider _capsuleCollider;

    // Returns the nearest enemy in range, or null if there are no enemies in range
    public Transform GetNearestEnemy()
    {
        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        // Iterate through the list of enemies in range to find the nearest one
        foreach (Transform enemy in _enemiesInRange)
        {
            if (enemy == null) continue; // Skip if the enemy has been destroyed

            float distance = Vector3.Distance(transform.position, enemy.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;

    }

    private void Update()
    {
        _capsuleCollider.radius = _weaponManager.range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!_enemiesInRange.Contains(other.transform))
            {
                _enemiesInRange.Add(other.transform);
            }

            Debug.Log("Enemy in range");
        }

        if(other.CompareTag("RangedEnemy"))
        {
            if (!_enemiesInRange.Contains(other.transform))
            {
                _enemiesInRange.Add(other.transform);
            }
            Debug.Log("Ranged Enemy in range");
        }

        if (other.CompareTag("TankEnemy"))
        {
            if (!_enemiesInRange.Contains(other.transform))
            {
                _enemiesInRange.Add(other.transform);
            }
            Debug.Log("Tank Enemy in range");
        }

        if (other.CompareTag("Boss"))
        {
            if (!_enemiesInRange.Contains(other.transform))
            {
                _enemiesInRange.Add(other.transform);
            }
            Debug.Log("Tank Enemy in range");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemiesInRange.Remove(other.transform);

            Debug.Log("Enemy out of range");
        }

        if (other.CompareTag("RangedEnemy"))
        {
            _enemiesInRange.Remove(other.transform);

            Debug.Log("Enemy out of range");
        }

        if (other.CompareTag("TankEnemy"))
        {
            _enemiesInRange.Remove(other.transform);

            Debug.Log("Enemy out of range");
        }

        if (other.CompareTag("Boss"))
        {
            _enemiesInRange.Remove(other.transform);

            Debug.Log("Enemy out of range");
        }
    }

}
