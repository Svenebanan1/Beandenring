using UnityEngine;
using System.Collections;

public class SpawnHolyLight : MonoBehaviour
{
    [SerializeField] private GameObject _light;

    private float lifetime = 1.1f;

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
        yield return new WaitForSeconds(1f);

        GameObject light = Instantiate(_light, transform.position + new Vector3(0, -0.4f, 0), Quaternion.identity);

        yield return null;
    }
}
