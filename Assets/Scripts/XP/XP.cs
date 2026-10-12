using UnityEngine;

public class XP : MonoBehaviour
{
    public static int ActiveCount { get; private set; }

    private Transform _mainCamera;
    private PlayerLvL _playerLvL;

    private float lifetime = 10f;

    private void OnEnable()
    {
        ActiveCount++;
    }

    private void OnDisable()
    {
        ActiveCount--;
    }

    void Start()
    {
        _mainCamera = Camera.main.transform;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.LookAt(transform.position + _mainCamera.rotation * Vector3.forward, _mainCamera.rotation * Vector3.up);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerLvL = collision.gameObject.GetComponent<PlayerLvL>();
            _playerLvL.GainedXP();
            Destroy(gameObject);
        }
    }
}