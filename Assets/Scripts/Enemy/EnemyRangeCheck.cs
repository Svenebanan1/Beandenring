using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyRangeCheck : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public NavMeshAgent agent;

    [SerializeField] private BoxCollider enemySword;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        enemySword = GetComponentInChildren<BoxCollider>();

        enemySword.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
