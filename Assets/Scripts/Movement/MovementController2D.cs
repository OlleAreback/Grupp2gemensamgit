using UnityEngine;
using UnityEngine.InputSystem;

// This component can be used to move a character that should be affected by gravity
// Use with the components CharacterController and PlayerInput.
// PlayerInput should be set to Behavior: Invoke Unity Events
public class MovementController2D : MonoBehaviour
{ 
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private CharacterController controller;

    private Vector2 moveInput;
    private bool jumpInput;
    private Vector3 velocity;
    private bool wasGrounded;
    
    
    void Update()
    {
        ApplyGravity();
        
        velocity = TranslateInputToVelocity(moveInput);
        
        // Apply jump-input:
        if (jumpInput)
        {
            velocity.y = jumpForce;
            jumpInput = false;
        }
        
        controller.Move(velocity * Time.deltaTime);

        bool isGrounded = controller.isGrounded;
        
        // Check if character lost contact with ground this frame
        if (wasGrounded == true && isGrounded == false)
        {
            // Has fallen. Play fall sound and/or trigger fall animation etc
        }
        // Check if character gained contact with ground this frame
        else if (wasGrounded == false && isGrounded == true)
        {
            // Has landed. Play landing sound and/or trigger landing animation etc
        }
        
        wasGrounded = isGrounded;
    }

    private void ApplyGravity()
    {
        // Applies a set gravity for when player is grounded
        if (controller.isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -1.0f;
        }
        // Updates fall speed with gravity if object isn't grounded
        else
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }
    }
    
    Vector3 TranslateInputToVelocity(Vector2 input)
    {
        // Make the character move on the ground (XZ-plane)
        return new Vector3(input.x * speed, velocity.y, 0.0f);
    }

    // Handle Move-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
    }

    // Handle Jump-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && wasGrounded)
        {
            Debug.Log("Jump!");
            jumpInput = true;
            // Jumps: Set animation parameters etc here
        }
    }
    
    // Add additional methods to handle additional input
}

