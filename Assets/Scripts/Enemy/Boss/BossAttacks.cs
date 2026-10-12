using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    private int randomAttack;
    private bool canAttack;

    public Animator animator1;
    public Animator animator2;

    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] Transform player;

    [SerializeField] private GameObject _shadowStrikePrefab;

    [SerializeField] private Renderer enemyRenderer;

    [SerializeField] private Color holyLight;
    [SerializeField] private Color holyDark;
    [SerializeField] private float flashTime = 0.1f;

    private Color normalColor;

    [SerializeField] private List<Transform> teleportPoints;

    [SerializeField] private GameObject parent;

    private int lastTeleportIndex = -1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack)
        {
            randomAttack = (int)Random.Range(1, 11);
            Debug.Log("Random Attack: " + randomAttack);

            if (randomAttack >= 1 && randomAttack <= 3)
            {
                StartCoroutine(HolyLight());
                // Perform Attack 1
                Debug.Log("Performing Attack 1");
            }
            else if (randomAttack >= 4 && randomAttack <= 6)
            {
                StartCoroutine(WingAttack());
                // Perform Attack 2
                Debug.Log("Performing Attack 2");
            }
            else if (randomAttack >= 7 && randomAttack <= 9)
            {

                StartCoroutine(ShadowStrike());
                // Perform Attack 3
                Debug.Log("Performing Attack 3");
            }
            else
            {
                StartCoroutine(Teleport());
            }
        }
    
    }

    IEnumerator HolyLight()
    {
        canAttack = false;

        enemyRenderer.material.color = holyLight;
        yield return new WaitForSeconds(flashTime);
        enemyRenderer.material.color = normalColor;

        yield return new WaitForSeconds(1f);

        //animator1.SetTrigger("HolyLight");

        float randomCount = Random.Range(2f, 5f);
        yield return new WaitForSeconds(randomCount); // Wait for 4 seconds before allowing the next attack

        canAttack = true;
    }

    IEnumerator WingAttack()
    {
        canAttack = false;
        animator2.SetTrigger("WingAttack");

        float randomCount = Random.Range(2f, 5f);
        yield return new WaitForSeconds(randomCount); // Wait for 4 seconds before allowing the next attack

        canAttack = true;
    }

    IEnumerator ShadowStrike()
    {
        canAttack = false;

        enemyRenderer.material.color = holyDark;
        yield return new WaitForSeconds(flashTime);
        enemyRenderer.material.color = normalColor; 

        yield return new WaitForSeconds(1f);


        RaycastHit hit;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        
        for (int i = 0; i < 10; i++)
        {
             if (Physics.Raycast(player.position, Vector3.down, out hit, 100f, whatIsGround))
             {
                 Instantiate(_shadowStrikePrefab, hit.point + Vector3.up * 0.05f, Quaternion.identity);
             }

             yield return new WaitForSeconds(0.25f); // Wait for 0.2 seconds before spawning the next Shadow Strike
        }

        float randomCount = Random.Range(2f, 5f);
        yield return new WaitForSeconds(randomCount); // Wait for 4 seconds before allowing the next attack

        canAttack = true;
    }

    IEnumerator Teleport()
    {
        canAttack = false;

        if (teleportPoints.Count > 1)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, teleportPoints.Count);
            }
            while (randomIndex == lastTeleportIndex);

            lastTeleportIndex = randomIndex;

            parent.transform.position = teleportPoints[randomIndex].position;
        }

        float randomCount = Random.Range(2f, 5f);
        yield return new WaitForSeconds(randomCount);

        canAttack = true;
    }

}
    