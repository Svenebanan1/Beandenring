using UnityEngine;

public class MålnLookAtPlayer : MonoBehaviour
{
    private Transform _mainCamera;
    [SerializeField] private float _lifeTime;
    [SerializeField] WeaponManager _weaponManager;

    public WeaponManager Manager => _weaponManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = Camera.main.transform;
        _lifeTime = _weaponManager.projectileLifetime;
        Destroy(gameObject, _lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + _mainCamera.rotation * Vector3.forward, _mainCamera.rotation * Vector3.up);
    }
}
