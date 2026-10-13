using Unity.VisualScripting;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    private Transform player;
    private float moveSpeed = 25f;
    [SerializeField] private bool inRange;
    [SerializeField] float range = 7;
       
    float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inRange = false;

        // Find the Player object in the scene and get its Transform
        Player playerComponent = Object.FindAnyObjectByType<Player>();
        if (playerComponent != null)
        {
            player = playerComponent.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
         distance = Vector3.Distance(transform.position, player.position);

        if(distance < range) 
          inRange = true;

         

        if (player != null && inRange)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
