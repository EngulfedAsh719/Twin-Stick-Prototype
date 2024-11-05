using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }
    
    public Joystick movementJoystick;
    
    public float moveSpeed = 2.6f;
    public float walkThreshold = 0.05f;
    public float runThreshold = 0.6f;
    public float rotationSpeed = 5f;
    public float detectionRadius = 5f;
    public float Magnitude;

    private PhotonView photonView;

    private Rigidbody rb;
    private float animationSpeed;
    private Animator animator;
    private Transform cameraTransform;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        photonView = PhotonView.Get(this.gameObject);
        Debug.Log(PhotonView.Get(this.gameObject).IsMine);
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(movementJoystick.Horizontal, 0f, movementJoystick.Vertical);
        float inputMagnitude = moveDirection.magnitude;
        Magnitude = inputMagnitude;
        animator.SetFloat("SpeedMultiplier", animationSpeed);

        if (inputMagnitude > 0.05f && inputMagnitude < 0.3f)
            animationSpeed = 1f;
        else if (inputMagnitude > 0.3f && inputMagnitude < 0.5f)
            animationSpeed = 1.25f;
        else
            animationSpeed = 1.5f;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        Transform targetEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Vector3 directionToEnemy = (hitCollider.transform.position - transform.position).normalized;
                float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);
                if (angleToEnemy < 85f)
                {
                    targetEnemy = hitCollider.transform;
                    break;
                }
            }
        }

        if (targetEnemy != null)
        {
            Vector3 directionToEnemy = (targetEnemy.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            if (inputMagnitude > 0)
            {
                Vector3 forward = cameraTransform.forward;
                forward.y = 0;
                forward.Normalize();
                Vector3 right = cameraTransform.right;
                right.y = 0;
                right.Normalize();
                moveDirection = forward * movementJoystick.Vertical + right * movementJoystick.Horizontal;

                Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }

        if (inputMagnitude > walkThreshold)
        {
            Vector3 move = moveDirection.normalized * moveSpeed * Time.deltaTime;
            transform.position += new Vector3(move.x, 0, move.z);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);

            if (inputMagnitude > runThreshold)
            {
                move = moveDirection.normalized * moveSpeed * Time.deltaTime;
                transform.position += new Vector3(move.x, 0, move.z);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }
}
