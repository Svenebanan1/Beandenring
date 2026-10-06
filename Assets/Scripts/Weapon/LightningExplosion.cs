using UnityEngine;

public class LightningExplosion : MonoBehaviour
{

    private Rigidbody _rb;
    private Transform _target;

    public EnemyHP enemy;

    [SerializeField] private GameObject _targetGameObject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Instantiate(_targetGameObject, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
