using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public static CharacterHealth Instance;

    private void Awake()
    {
        Instance = this;
    }
    public int health = int.MaxValue;

    private ViewModel viewModel;

    public int Health
    {
        get => health;
        set
        {
            if (health == value) return;
            health = value;
            if (viewModel != null) viewModel.Health = health.ToString();
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        health = 100;
        viewModel = FindObjectOfType<ViewModel>();
    }
}