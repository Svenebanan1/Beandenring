using UnityEngine;

public class RedRock : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

    [SerializeField] private GameObject fireBallPrefab;

    public RangeCheck rangeCheck;

    private float _spawnTimer;

    private void Start()
    {
        _spawnTimer = _weaponManager.fireRate;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f && rangeCheck.isInRange)
        {
            Transform enemy = rangeCheck.GetNearestEnemy();

            if (enemy != null)
            {
                ShootEnemy(enemy);
            }

            _spawnTimer = _weaponManager.fireRate;
        }
    }

    private void ShootEnemy(Transform enemy)
    {
        Vector3 direction = (enemy.position - transform.position).normalized;

        Vector3 spawnPosition = transform.position + direction; // Adjust the offset as needed

        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject fireBallObject = Instantiate(fireBallPrefab, spawnPosition, rotation);

        FireBall fireBall = fireBallObject.GetComponent<FireBall>();

        if (fireBall != null)
        {
            fireBall.SetTarget(enemy);
        }
    }

}

