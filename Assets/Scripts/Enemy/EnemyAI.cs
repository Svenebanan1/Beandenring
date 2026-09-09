using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] private Animator animator;

    [SerializeField] private BoxCollider enemySword;

    private Rigidbody rb;

    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    private void Awake()
    {
       player = GameObject.Find("Character").transform;
       agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
        enemySword = GetComponentInChildren<BoxCollider>();

        enemySword.enabled = false;
    }

    void Update()
    {
        Debug.Log(player.position);
        agent.SetDestination(player.position);
        agent.updateRotation = true;
    }

    IEnumerator Attack()
    {

        animator.SetTrigger("Attack");

        yield return null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.speed = agent.speed * 0.5f;
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.speed = agent.speed * 2f;
            StopCoroutine(Attack());
        }
    }

    public void EnableHitbox()
    {
        enemySword.enabled = true;
    }

}