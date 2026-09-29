using UnityEngine;

public class ShootSword : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;

    public WeaponManager Manager => _weaponManager;

    [SerializeField] private GameObject _swordPrefab;

    public RangeCheck rangeCheck;

    public float _spawnTimer;

    public UpgradingScript upgrading;

    private void Start()
    {
        upgrading = GameObject.Find("UI Items Active").GetComponent<UpgradingScript>();
        _spawnTimer = Manager.fireRate * upgrading.firefateMultiplier;
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

            _spawnTimer = _weaponManager.fireRate * upgrading.firefateMultiplier;
        }
    }

    private void ShootEnemy(Transform enemy)
    {
        Vector3 direction = (enemy.position - transform.position).normalized;

        Vector3 spawnPosition = transform.position + direction; // Adjust the offset as needed

        Quaternion rotation = Quaternion.LookRotation(direction);

        // Fix: Use Quaternion.Euler to create a rotation with a 90 degree z-axis offset
        GameObject swordObject = Instantiate(_swordPrefab, spawnPosition, rotation * Quaternion.Euler(0, 0, 90));

        Sword sword = swordObject.GetComponent<Sword>();

        if (sword != null)
        {
            sword.SetTarget(enemy);
        }
    }

}

