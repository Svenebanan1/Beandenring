using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] private BoxCollider attackRange;
    [SerializeField] private Animator animator;

    private Rigidbody rb;
    private bool inRangeOfAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        inRangeOfAttack = false;
    }

    void Update()
    {
        if (inRangeOfAttack)
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRangeOfAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRangeOfAttack = false;
        }
    }
}