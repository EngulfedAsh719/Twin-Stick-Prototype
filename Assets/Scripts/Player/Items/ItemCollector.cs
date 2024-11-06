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
            if (speedBoost != null)
            {
                speedBoost.Use(gameObject);
            }

            else if (healthBoost != null)
            {
                healthBoost.Use(gameObject);
            }
        }
    }
}
