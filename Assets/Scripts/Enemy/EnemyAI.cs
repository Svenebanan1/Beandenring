using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform player   ;

    [SerializeField] private Animator animator;

    [SerializeField] private BoxCollider enemySword;

    private Rigidbody rb;

    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private EnemyVariables _enemyVariables;

    [SerializeField] private float rotationSpeed = 720f;

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

        _enemyVariables.speed = agent.speed;

        agent.updateRotation = false;
    }

    void Update()
    {
        agent.SetDestination(player.position);

        // Get direction towards player
        Vector3 direction = player.position - transform.position;

        // Prevent enemy from looking up or down
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
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
        enemySword.enabled = true;
    }

    public void SlowEffect()
    {
        Debug.Log("slowed");
        StartCoroutine(Slowed());
    }

}