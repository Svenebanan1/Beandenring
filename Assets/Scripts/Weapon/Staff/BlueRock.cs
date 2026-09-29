using UnityEngine;

public class BlueRock : MonoBehaviour
{
    [SerializeField] WeaponManager _weaponManager;
    public WeaponManager Manager => _weaponManager;

    [SerializeField] private GameObject icePrefab;

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

        GameObject iceObject = Instantiate(icePrefab, spawnPosition, rotation);

        Ice ice = iceObject.GetComponent<Ice>();

        if (ice != null)
        {
            ice.SetTarget(enemy);
        }
    }
}

