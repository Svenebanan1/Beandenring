using UnityEngine;

public class Rock : MonoBehaviour
{
    public MeshCollider rockCollider;

    private WaveManager waveManager;

    [SerializeField] private EnemyVariables _enemyVariables;
    public float _dmg;

    private float lifeTime = 1.25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rockCollider = GetComponent<MeshCollider>();
        rockCollider.convex = false;

        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        _dmg = _enemyVariables.maxHp * waveManager.enemyDMGMultiplier;

        Destroy(gameObject, lifeTime);
    }

    void Attack()
    {
        rockCollider.convex = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHP playerHP = other.GetComponent<PlayerHP>();
            playerHP.TakeDamage(_dmg);
            rockCollider.convex = false;
        }
    }
}
