using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 6f;
    public float acceleration = 20f;
    public float deceleration = 25f;

    [Header("Dash")]
    public float dashSpeed = 18f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    private Rigidbody rb;
    private InputSystem_Actions input;

    private Vector2 moveInput;
    private Vector3 velocity;

    private bool isDashing;
    private float dashTime;
    private float lastDashTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        moveInput = input.Player.Move.ReadValue<Vector2>();

        if (input.Player.Sprint.WasPressedThisFrame())
        {
            TryDash();
        }
    }
    
    private void FixedUpdate()
    {
        Vector3 targetDir = new Vector3(moveInput.x, 0, moveInput.y);

        if (targetDir.magnitude > 1)
            targetDir.Normalize();

        if (!isDashing)
        {
            Vector3 targetVelocity = targetDir * maxSpeed;

            float accel = (targetDir.magnitude > 0.1f)
                ? acceleration
                : deceleration;

            velocity = Vector3.MoveTowards(
                velocity,
                targetVelocity,
                accel * Time.fixedDeltaTime
            );
        }
        else
        {
            dashTime -= Time.fixedDeltaTime;

            if (dashTime <= 0)
                isDashing = false;
        }

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void TryDash()
    {
        if (Time.time < lastDashTime + dashCooldown)
            return;

        Vector3 dir = new Vector3(moveInput.x, 0, moveInput.y);

        if (dir.sqrMagnitude < 0.01f)
            dir = transform.forward;

        dir.Normalize();

        velocity = dir * dashSpeed;

        isDashing = true;
        dashTime = dashDuration;
        lastDashTime = Time.time;
    }
}