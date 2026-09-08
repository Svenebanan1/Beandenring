using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private float playerHP;
    [SerializeField] private float playerMaxHP = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHP = playerMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemySword"))
        {
            TakeDamage();
        }
    }
}
