using UnityEngine;

public class WingAttack : MonoBehaviour
{
    public float tickTime; // Time interval for damage ticks    
    private Transform _player;

    private Rigidbody rb;

    private float lifetime = 8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tickTime = 0.5f; // Set the tick interval to 0.5 seconds
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>(); 

        Destroy(gameObject, lifetime); // Destroy the object after 8 seconds
    }

    // Update is called once per frame
    void Update()
    {
        tickTime -= Time.deltaTime;

        Vector3 direction =
            (_player.position - transform.position).normalized;

        rb.linearVelocity = direction * 15;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHP playerHealth = other.GetComponent<PlayerHP>();
            if (playerHealth != null && tickTime <= 0)
            {
                playerHealth.TakeDamage(10); // Deal 10 damage to the player
                tickTime = 0.25f; // Reset the tick timer
            }
        }
    }
}
