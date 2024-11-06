using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class HealthSystem : MonoBehaviourPunCallbacks
{
    public static HealthSystem Instance { get; private set; }

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
        if (Mathf.Abs(currentHealth - targetHealth) > 0.01f)
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

    [PunRPC]
    public void DecreaseHealthRPC(int amount)
    {
        if (photonView.IsMine)
        {
            targetHealth -= amount;
            targetHealth = Mathf.Clamp(targetHealth, 0, maxHealth);
            UpdateUI();
        }
    }
}
