using UnityEngine;

public class ShootSlash : MonoBehaviour
{
    [SerializeField] private WeaponManager _weaponManager;
    [SerializeField] private GameObject _slashPrefab;
    [SerializeField] private RangeCheck _rangeCheck;
    [SerializeField] private float _spawnOffset = 1f;

    public float _spawnTimer;

    private void Start()
    {
        _spawnTimer = _weaponManager.fireRate;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f && _rangeCheck.isInRange)
        {
            ShootInFourDirections();
            _spawnTimer = _weaponManager.fireRate;
        }
    }

    private void ShootInFourDirections()
    {
        Vector3[] directions =
        {
            transform.forward,   // Forward
            -transform.right,    // Left
            -transform.forward,  // Backward / down
            transform.right      // Right
        };

        foreach (Vector3 direction in directions)
        {
            SpawnSlash(direction);
        }
    }

    private void SpawnSlash(Vector3 direction)
    {
        direction.Normalize();

        Vector3 spawnPosition =
            transform.position + direction * _spawnOffset;

        Quaternion rotation =
            Quaternion.LookRotation(direction) *
            Quaternion.Euler(90f, 0f, 0f);

        GameObject slashObject = Instantiate(
            _slashPrefab,
            spawnPosition,
            rotation
        );

        Slash slash = slashObject.GetComponent<Slash>();

        if (slash != null)
        {
            slash.SetDirection(direction);
        }
    }

}
