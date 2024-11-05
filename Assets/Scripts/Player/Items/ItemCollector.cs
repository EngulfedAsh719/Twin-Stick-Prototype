using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        SpeedBoostItem speedBoost = collision.gameObject.GetComponent<SpeedBoostItem>();
        HealthBoostItem healthBoost = collision.gameObject.GetComponent<HealthBoostItem>();
        Item item = collision.gameObject.GetComponent<Item>();

        if (item != null)
        {
            Destroy(collision.gameObject);
            InventorySystem.Instance.AddItem(item);

            if (speedBoost != null)
            {
                Debug.Log("Столкновение с SpeedBoostItem");
                speedBoost.Use(gameObject);
            }

            else if (healthBoost != null)
            {
                Debug.Log("Столкновение с HealthBoost");
                healthBoost.Use(gameObject);
            }
        }
    }
}
