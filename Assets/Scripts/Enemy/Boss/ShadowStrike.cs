using UnityEngine;

public class ShadowStrike : MonoBehaviour
{
    private float dmg = 150f;

    [SerializeField] private SphereCollider sC;

    [SerializeField] private GameObject oBject;

    private void Start()
    {
        sC.enabled = false;
    }

    void Damage()
    {
        sC.enabled = true;
    }

    void Die()
    {
        Destroy(oBject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           PlayerHP playerHP = other.GetComponent<PlayerHP>();
            if (playerHP == null) 
                return;
            playerHP.TakeDamage(dmg);
            
            sC.enabled = false;
        }
    }
}
