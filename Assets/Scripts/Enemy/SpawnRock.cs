using System.Collections;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class SpawnRock : MonoBehaviour
{
    [SerializeField] private GameObject _rock;

    private float lifetime = 0.7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRockCoroutine());
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnRockCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject rock = Instantiate(_rock, transform.position + new Vector3(0,-0.4f,0), Quaternion.identity);

        Debug.Log("Circle position: " + transform.position);
        Debug.Log("Rock position: " + rock.transform.position);

        yield return null;
    }
}
