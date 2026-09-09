using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    [SerializeField] private float _dmg = 5f;

    public PlayerHP playerHP;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHP.TakeDamage(_dmg);
            boxCollider.enabled = false;
        }
    }
}
