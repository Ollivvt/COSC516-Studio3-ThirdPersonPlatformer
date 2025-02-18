using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private CharacterController controller;
    private bool isDashing = false;
    private float dashTime = 0.2f;
    private float dashSpeed = 20f;
    private float dashCooldown = 1.0f;
    private float lastDashTime;

    private Vector3 dashDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            StartDash();
        }

        if (isDashing)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
        }
    }

    void StartDash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        dashDirection = transform.forward;  // Dash in player's forward direction
        Invoke("StopDash", dashTime);  // Stop dash after duration
    }

    void StopDash()
    {
        isDashing = false;
    }
}
