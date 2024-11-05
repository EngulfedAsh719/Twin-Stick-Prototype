using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public float speed;

    public void IncreaseSpeed(float multiplier, float duration)
    {
        StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        PlayerController.Instance.moveSpeed += multiplier;
        Debug.Log("Скорость увеличена на: " + multiplier);
        yield return new WaitForSeconds(duration);
        PlayerController.Instance.moveSpeed -= multiplier;
        Debug.Log("Скорость вернулась к норме.");
    }
}
