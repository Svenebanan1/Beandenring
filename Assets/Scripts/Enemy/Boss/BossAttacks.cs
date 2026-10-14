
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    private bool canAttack = true;

    public Animator animator1;
    public Animator animator2;

    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private Transform player;

    [Header("Shadow Strike")]
    [SerializeField] private GameObject _shadowStrikePrefab;
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private Color holyDark;
    [SerializeField] private float flashTime = 0.1f;

    private Color normalColor;
    private Material enemyMaterial;

    [Header("Teleport")]
    [SerializeField] private List<Transform> teleportPoints;
    [SerializeField] private GameObject parent;

    private int lastTeleportIndex = -1;

    [Header("Wing Attack")]
    [SerializeField] private GameObject wingAttackPrefab;
    [SerializeField] private GameObject firePoint;

    [Header("Holy Light")]
    [SerializeField] private GameObject _redCircle;
    [SerializeField] private float minSpawnRange = 2f;
    [SerializeField] private float maxSpawnRange = 8f;
    [SerializeField] private float groundOffset = 0.05f;

    [Header("Cooldown")]
    [SerializeField] private float minCooldown = 0f;
    [SerializeField] private float maxCooldown = 2.5f;

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }

        enemyMaterial = enemyRenderer.material;
        normalColor = enemyMaterial.color;
    }

    private void Update()
    {
        if (!canAttack || player == null) return;

        StartCoroutine(AttackSequence());
    }

    // Controls all boss attacks
    private IEnumerator AttackSequence()
    {
        canAttack = false;

        int randomAttack = Random.Range(0, 8);

        Debug.Log("Random Attack: " + randomAttack);

        // Decide which attacks should run
        bool useHolyLight =
            randomAttack == 0 ||
            randomAttack == 3 ||
            randomAttack == 4 ||
            randomAttack == 6;

        bool useWingAttack =
            randomAttack == 1 ||
            randomAttack == 3 ||
            randomAttack == 5 ||
            randomAttack == 6;

        bool useShadowStrike =
            randomAttack == 2 ||
            randomAttack == 4 ||
            randomAttack == 5 ||
            randomAttack == 6;

        Coroutine holy = null;
        Coroutine wing = null;
        Coroutine shadow = null;

        // Start selected attacks simultaneously
        if (useHolyLight)
            holy = StartCoroutine(HolyLight());

        if (useWingAttack)
            wing = StartCoroutine(WingAttack());

        if (useShadowStrike)
            shadow = StartCoroutine(ShadowStrike());

        // Teleport is a separate action
        if (randomAttack == 7)
            Teleport();

        // Wait until ALL selected attacks finish
        if (holy != null)
            yield return holy;

        if (wing != null)
            yield return wing;

        if (shadow != null)
            yield return shadow;

        // Cooldown before the next attack sequence
        yield return new WaitForSeconds(
            Random.Range(minCooldown, maxCooldown)
        );

        canAttack = true;
    }

    // HOLY LIGHT
    private IEnumerator HolyLight()
    {
        for (int i = 0; i < 5; i++)
        {
            if (player == null) yield break;

            Vector2 randomDirection =
                Random.insideUnitCircle.normalized;

            float randomDistance =
                Random.Range(minSpawnRange, maxSpawnRange);

            Vector3 spawnPosition = player.position +
                new Vector3(
                    randomDirection.x * randomDistance,
                    0f,
                    randomDirection.y * randomDistance
                );

            Vector3 rayOrigin =
                spawnPosition + Vector3.up * 10f;

            if (Physics.Raycast(
                rayOrigin,
                Vector3.down,
                out RaycastHit hit,
                100f,
                whatIsGround))
            {
                Vector3 groundPosition =
                    hit.point + Vector3.up * groundOffset;

                Instantiate(
                    _redCircle,
                    groundPosition,
                    Quaternion.identity
                );
            }

            yield return new WaitForSeconds(0.15f);
        }
    }

    // WING ATTACK
    private IEnumerator WingAttack()
    {
        animator2.SetTrigger("WingAttack");

        yield return new WaitForSeconds(0.65f);

        Instantiate(
            wingAttackPrefab,
            firePoint.transform.position,
            Quaternion.identity
        );
    }

    // SHADOW STRIKE
    private IEnumerator ShadowStrike()
    {
        enemyMaterial.color = holyDark;

        yield return new WaitForSeconds(flashTime);

        enemyMaterial.color = normalColor;

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 7; i++)
        {
            if (player == null) yield break;

            Vector3 rayOrigin =
                player.position + Vector3.up * 10f;

            if (Physics.Raycast(
                rayOrigin,
                Vector3.down,
                out RaycastHit hit,
                100f,
                whatIsGround))
            {
                Instantiate(
                    _shadowStrikePrefab,
                    hit.point + Vector3.up * 0.05f,
                    Quaternion.identity
                );
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    // TELEPORT
    private void Teleport()
    {
        if (teleportPoints == null ||
            teleportPoints.Count == 0)
            return;

        int randomIndex;

        do
        {
            randomIndex =
                Random.Range(0, teleportPoints.Count);
        }
        while (
            teleportPoints.Count > 1 &&
            randomIndex == lastTeleportIndex
        );

        lastTeleportIndex = randomIndex;

        parent.transform.position =
            teleportPoints[randomIndex].position;
    }
}
