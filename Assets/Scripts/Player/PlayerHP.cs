using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private float playerHP;
    [SerializeField] private float playerMaxHP = 100;

    public Image healthImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHP = playerMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        float targetHealth = playerHP / playerMaxHP;

        healthImage.fillAmount = Mathf.MoveTowards(
            healthImage.fillAmount,
            targetHealth,
            0.3f * Time.deltaTime
        );
    }

    public void TakeDamage(float dmg)
    {
        playerHP -= dmg;
    }

}
