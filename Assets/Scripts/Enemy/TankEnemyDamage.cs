using UnityEngine;

public class TankEnemyDamage : MonoBehaviour
{
    [SerializeField] private EnemyVariables _enemyVariables;
    public float _dmg;

    public SphereCollider sphereCollider;

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
            sphereCollider.enabled = false;
        }
    }
}
