using UnityEngine;

public class HealthBoostItem : Item
{
    public int healthBoost;
    public override void Use(GameObject player)
    {
        HealthSystem.Instance.IncreaseHealth(15);
        HealthSystem.Instance.UpdateUI();
        Debug.Log($"Использован предмет {itemName}");
    }
}
