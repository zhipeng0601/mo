using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.3f;

    private CharacterController controller;
    private Vector3 velocity;
    private Transform thisTransform;

    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        thisTransform = transform;
    }

    private void Update()
    {
        isGrounded = CheckIfGrounded();
        MoveCharacter();
        ApplyGravity();
        KeepCharacterOnXAxis();
    }

    private bool CheckIfGrounded()
    {
        return controller.isGrounded;
    }

    private void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = thisTransform.right * horizontalInput * moveSpeed;
        controller.Move(moveDirection * Time.deltaTime);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ensure character sticks to the ground
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    private void KeepCharacterOnXAxis()
    {
        // Optional: Prevent character from moving vertically.
        velocity.x = 0;
    }
}
