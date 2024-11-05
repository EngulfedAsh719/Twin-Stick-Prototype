using UnityEngine.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem Instance { get; set; }

    public float currentHealth, maxHealth = 100f, targetHealth;

    public float lerpSpeed = 3f;

    [SerializeField] private Image fillImage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHealth = targetHealth = maxHealth;

        UpdateUI();
    }

    private void Update()
    {
        if (currentHealth != targetHealth)
        {
            currentHealth = Mathf.Lerp(currentHealth, targetHealth, Time.deltaTime * lerpSpeed);
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = currentHealth / maxHealth;
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
