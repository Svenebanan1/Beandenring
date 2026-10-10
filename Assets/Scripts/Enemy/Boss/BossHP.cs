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

    void Start()
    {
        currentHealth = maxHealth;
        normalColor = enemyRenderer.material.color;
    }

    void Update()
    {
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