using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] private Animator animator;

    private Rigidbody rb;

    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private EnemyVariables _enemyVariables;

    [SerializeField] private GameObject _redCircle;

    private bool _isAttacking = false;

    [SerializeField] private float attackDuration = 0.5f;
    [SerializeField] private float attackCooldown = 1.5f;

    private bool _canAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.Find("Character").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        _enemyVariables.speed = agent.speed;
    }
    void Update()
    {
        if (!_isAttacking)
        {
            agent.SetDestination(player.position);
        }
    }

    public void SlowEffect()
    {
        Debug.Log("slowed");
        StartCoroutine(Slowed());
    }

    IEnumerator Slowed()
    {
        agent.speed = agent.speed * 0.5f;

        yield return new WaitForSeconds(0.5f);

        agent.speed = _enemyVariables.speed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _canAttack)
        {
            _canAttack = false;
            StartCoroutine(AttackAnimation());
        }
    }
    IEnumerator AttackAnimation()
    {
        _isAttacking = true;
        agent.isStopped = true;

        animator.SetTrigger("Attack");

        RaycastHit hit;

        if (Physics.Raycast(player.position, Vector3.down, out hit, 100f, whatIsGround))
        {
            Instantiate(_redCircle, hit.point + Vector3.up * 0.05f, Quaternion.identity);
        }

        yield return new WaitForSeconds(attackDuration);

        agent.isStopped = false;
        _isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);

        _canAttack = true;
    }
}
