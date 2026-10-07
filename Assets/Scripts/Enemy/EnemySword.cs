using Unity.VisualScripting;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [SerializeField] private EnemyVariables _enemyVariables;
    public float _dmg;

    [SerializeField] private BoxCollider boxCollider;

    private WaveManager waveManager;

    private void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        _dmg = _enemyVariables.maxHp * waveManager.enemyDMGMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHP playerHP = other.GetComponent<PlayerHP>();    
            playerHP.TakeDamage(_dmg);
            boxCollider.enabled = false;
        }
    }
}
