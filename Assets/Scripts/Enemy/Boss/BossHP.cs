using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private float currentHealth;

    [SerializeField] private Image healthBarFill;
    [SerializeField] private Animator animator;
    [SerializeField] private Renderer enemyRenderer;

    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float flashTime = 0.08f;

    private Color normalColor;
    private bool isDead;

    public BossAttacks bossAttacks;

    private float time = 5f; // Time in seconds before the boss starts attacking

    [SerializeField] SphereCollider sc1;
    [SerializeField] SphereCollider sc2;

    private int angelsKilled;

    void Start()
    {
        currentHealth = maxHealth;
        normalColor = enemyRenderer.material.color;

        bossAttacks.enabled = false; // Disable attacks at the start
        sc1.enabled = false; // Disable collider at the start
        sc2.enabled = false; // Disable collider at the start

        angelsKilled = PlayerPrefs.GetInt("Angels Killed",0);

    }

    void Update()
    {
        time -= Time.deltaTime;

        if(time <= 0f && !isDead)
        {
            bossAttacks.enabled = true; // Enable attacks after the time has passed
            sc1.enabled = true; // Enable collider after the time has passed
            sc2.enabled = true; // Enable collider after the time has passed
        }

        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        if (isDead) return;

        currentHealth = Mathf.Max(currentHealth - dmg, 0f);
        StartCoroutine(HitFlash());

        if (currentHealth <= 0f)
        {
            isDead = true;
            animator.SetTrigger("Die");
            StartCoroutine(DieRoutine());
        }
    }

    private IEnumerator DieRoutine()
    {
        angelsKilled++;
        PlayerPrefs.SetInt("Angels Killed", angelsKilled);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(2f); // roughly your death animation length
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator HitFlash()
    {
        enemyRenderer.material.color = hitColor;
        yield return new WaitForSeconds(flashTime);
        enemyRenderer.material.color = normalColor;
    }

}