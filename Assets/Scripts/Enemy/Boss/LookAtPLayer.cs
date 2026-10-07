using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.LookAt(_player);
    }
}