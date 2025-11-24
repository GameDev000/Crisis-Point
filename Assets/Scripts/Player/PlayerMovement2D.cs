using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;

    [Header("Input Actions")]
    [SerializeField] private InputAction move = new InputAction(type: InputActionType.Value,expectedControlType: nameof(Vector2));
    [SerializeField] private InputAction jump = new InputAction(
        type: InputActionType.Button);

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(groundCheck.position,groundCheckRadius);
        isGrounded = false;
        foreach (var hit in hits)
        {
            if (hit.gameObject == this.gameObject)
                continue;

            if (hit.CompareTag("ground"))
            {
                isGrounded = true;
                break;
            }
        }
        if (jump.WasPerformedThisFrame() && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        Vector2 input = move.ReadValue<Vector2>();
        float moveX = input.x;
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }
}
