using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 8f;  // Slightly increased speed
    public float jumpForce = 3f;  // More powerful jump
    public float gravity = -20f;  // Faster gravity
    public Transform cam;  // Reference to Cinemachine Camera

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    // Ground Check
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    public float rotationSpeed = 10f;  // Faster rotation speed

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground Detection
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Handle Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Instant Rotation: Player should turn fast in the direction of movement
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the Player
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        // Handle Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
