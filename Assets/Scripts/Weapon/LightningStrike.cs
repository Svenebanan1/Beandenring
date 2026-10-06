using UnityEngine;

public class LightningStrike : MonoBehaviour
{

    [SerializeField] WeaponManager _weaponManager;

    public WeaponManager Manager => _weaponManager;

    public RangeCheck rangeCheck;

    public float _spawnTimer;
   

    public UpgradingScript upgrading;

    [SerializeField] private GameObject _lightningCloudPrefab;
    [SerializeField] private float _spawnHeight = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgrading = GameObject.Find("UI Items Active").GetComponent<UpgradingScript>();
        _spawnTimer = Manager.fireRate * upgrading.firefateMultiplier;
    }
        
    // Update is called once per frame
    void Update()
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
        Vector3 spawnPosition = enemy.position + Vector3.up * _spawnHeight;

        Instantiate(
            _lightningCloudPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

}
