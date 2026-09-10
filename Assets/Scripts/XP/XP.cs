using UnityEngine;

public class XP : MonoBehaviour
{
    private Transform _mainCamera;
    private PlayerLvL _playerLvL;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
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
