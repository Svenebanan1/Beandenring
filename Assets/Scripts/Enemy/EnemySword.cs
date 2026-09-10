using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [SerializeField] private EnemyVariables _enemyVariables;

    [SerializeField] private BoxCollider boxCollider;

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
            playerHP.TakeDamage(_enemyVariables.dmg);
            boxCollider.enabled = false;
        }
    }
}
