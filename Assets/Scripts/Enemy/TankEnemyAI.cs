using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TankEnemyAI : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] private Animator animator;

    [SerializeField] private SphereCollider Arms;

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
        Arms = GetComponentInChildren<SphereCollider>();

        Arms.enabled = false;

        _enemyVariables.speed = agent.speed;
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }

    IEnumerator Attack()
    {

        animator.SetTrigger("Attack");

        yield return null;
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
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(Attack());
        }
    }

    public void EnableHitbox()
    {
        Arms.enabled = true;
    }

    public void SlowEffect()
    {
        Debug.Log("slowed");
        StartCoroutine(Slowed());
    }

}