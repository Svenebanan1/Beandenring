using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TankEnemyAI : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] private Animator animator;

    public SphereCollider arms;

    private Rigidbody rb;

    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private EnemyVariables _enemyVariables;

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
        agent.SetDestination(player.position);
    }

    IEnumerator AttackAnim()
    {
        animator.SetTrigger("Attack");

        yield return null;
    }

    public void Attack()
    {
        arms.enabled = true;
    }

    IEnumerator Slowed()
    {
        agent.speed = agent.speed * 0.5f;

        yield return new WaitForSeconds(0.5f);

        agent.speed = _enemyVariables.speed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(AttackAnim());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            arms.enabled = false;
            StopCoroutine(AttackAnim());
        }
    }

    public void SlowEffect()
    {
        Debug.Log("slowed");
        StartCoroutine(Slowed());
    }

}