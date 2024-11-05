using UnityEngine;

public class SpeedBoostItem : Item
{
    public float speedMultiplier;
    public float duration;

    public override void Use(GameObject player)
    {
        player.GetComponent<PlayerStats>().IncreaseSpeed(speedMultiplier, duration);
        Debug.Log($"Использован предмет: {itemName}");
    }
}
