using UnityEngine;

public class HolyLight : MonoBehaviour
{
    [SerializeField] CapsuleCollider capsuleCollider;

    private float lifeTime = 1.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHP playerHealth = other.GetComponent<PlayerHP>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(35); // Deal 45 damage to the player
                capsuleCollider.enabled = false;
            }
        }
    }
}
