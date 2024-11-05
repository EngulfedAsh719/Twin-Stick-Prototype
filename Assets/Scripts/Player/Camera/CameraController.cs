using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; set; }

    [SerializeField] private string joystickName = "CameraMovementJoystick";

    public float sensitivity = 5f;
    public float distance = 10f;
    public float yOffset = 2f;

    private float currentX = 0f;
    private float currentY = 0f;

    public Joystick joystick;
    public GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        if (joystick != null && player != null)
        {
            currentX += joystick.Horizontal * sensitivity;
            currentY -= joystick.Vertical * sensitivity;
            currentY = Mathf.Clamp(currentY, 0f, 60f);

            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            transform.position = player.transform.position + rotation * dir + new Vector3(0, yOffset, 0);
            transform.LookAt(player.transform.position + Vector3.up * yOffset);
        }
    }

    public void FindJoystick()
    {
        GameObject joystickObject = GameObject.Find(joystickName);
        if (joystickObject != null)
        {
            joystick = joystickObject.GetComponent<Joystick>();
            if (joystick != null)
            {
                Debug.Log("Joystick найден");
            }
            else
            {
                Debug.Log("Joystick не найден");
                return;
            }
        }
        else
            Debug.Log($"Обьект с именем {joystickName} не найден");
    }

    public void FindPlayer(string namePerson)
    {
        player = GameObject.Find(namePerson);
    }
}
