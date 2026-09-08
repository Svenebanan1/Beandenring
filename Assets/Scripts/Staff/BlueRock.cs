using UnityEngine;

public class BlueRock : MonoBehaviour
{
    [SerializeField] private GameObject icePrefab;
    [SerializeField] private float _spawnInterval = 1f;

    public RangeCheck rangeCheck;

    private float _spawnTimer;

    private void Start()
    {
        _spawnTimer = _spawnInterval;
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

            _spawnTimer = _spawnInterval;
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

