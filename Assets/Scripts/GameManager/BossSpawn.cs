using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float time;

    public GameObject bossPrefab;
    public List<GameObject> Spawns; // List to hold multiple boss prefabs


    private bool bossSpawned = false;

    public GameObject waveManager;

    [Header("Components")]
    [SerializeField] public CinemachineCamera virtualCamera; // Use 'CinemachineVirtualCamera' in v2
    public CinemachineBasicMultiChannelPerlin multiChannelPerlin;

    [Header("Shake Settings")]
    [SerializeField] private float targetShakeIntensity = 2f;
    [SerializeField] private float shakeDuration = 0.5f;

    [Header("Lens Settings")]
    [SerializeField] private float targetLensSize = 80f; // Target FOV (3D) or Ortho Size (2D)
    [SerializeField] private float lensChangeSpeed = 5f;


    [SerializeField] private float _bossTimer;

    [SerializeField] private RangeCheck rangeCheck;

    private float shakeTimer;
    private float defaultLensSize;

    void Start()
    {

        time = 0f;
        if (virtualCamera == null)
        {
            virtualCamera = GetComponent<CinemachineCamera>();
        }

        // Get the noise component for screen shake
        multiChannelPerlin = virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        // Store the original lens size to revert back to it later
        defaultLensSize = virtualCamera.Lens.FieldOfView;
    }

    void Update()
    {
        time += Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (time >= _bossTimer && !bossSpawned)
        {
            bossSpawned = true;
            StartCoroutine(SpawnBoss());
        }

        HandleShakeTimer();
        HandleLensSize();
    }

    IEnumerator SpawnBoss()
    {
        foreach (GameObject spawn in Spawns)
        {
            Instantiate(spawn, spawn.transform.position, Quaternion.identity);
        }

        // Double the size of the capsule collider
        if (rangeCheck != null && rangeCheck._capsuleCollider != null)
        {
            rangeCheck._capsuleCollider.radius *= 2f;
            rangeCheck._capsuleCollider.height *= 2f;
        }

        // Trigger camera shake and zoom
        TriggerEffects();

        waveManager.SetActive(false);

        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy").Concat(GameObject.FindGameObjectsWithTag("RangedEnemy")).Concat(GameObject.FindGameObjectsWithTag("TankEnemy")));

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        yield return null;

        Instantiate(bossPrefab, transform.position, Quaternion.identity);
    }

    public void TriggerEffects()
    {
        shakeTimer = shakeDuration;

        if (multiChannelPerlin != null)
        {
            multiChannelPerlin.AmplitudeGain = targetShakeIntensity;
        }
    }

    private void HandleShakeTimer()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f && multiChannelPerlin != null)
            {
                multiChannelPerlin.AmplitudeGain = 0f;
            }
        }
    }

    private void HandleLensSize()
    {
        // Smoothly interpolate lens size based on whether the camera is shaking
        float currentTargetSize = (shakeTimer > 0) ? targetLensSize : defaultLensSize;

        // For 3D (Field of View)
        virtualCamera.Lens.FieldOfView = Mathf.Lerp(virtualCamera.Lens.FieldOfView, currentTargetSize, Time.deltaTime * lensChangeSpeed);

        // For 2D (Orthographic Size) - Uncomment below and comment out the FOV line if needed:
        // virtualCamera.Lens.OrthographicSize = Mathf.Lerp(virtualCamera.Lens.OrthographicSize, currentTargetSize, Time.deltaTime * lensChangeSpeed);
    }
}
