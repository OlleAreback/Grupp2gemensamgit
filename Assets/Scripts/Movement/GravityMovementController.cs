using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

// This component can be used to move a character that should be affected by gravity
// Use with the components CharacterController and PlayerInput.
// PlayerInput should be set to Behavior: Invoke Unity Events
public class GravityMovementController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform horizontal;

    private Vector2 moveInput;
    private Vector3 velocity;
    private bool wasGrounded;
    private Animator animator;

    private int lastSound = 0;
    private float targetTime;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        ApplyGravity();

        velocity = transform.TransformDirection(TranslateInputToVelocity(moveInput));

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

        targetTime -= Time.deltaTime;
        if (velocity != new Vector3(0, velocity.y, 0))
        {
            animator.SetFloat("Speed", speed);
            if (targetTime <= 0 && isGrounded) PlayFootsteps();
        }
        else if (velocity == new Vector3(0, velocity.y, 0))
        {
            animator.SetFloat("Speed", 0f);
        }

        speed = Input.GetKey(KeyCode.LeftShift) ? 6f : 2f;

        if (Input.GetKeyDown("space") && isGrounded)
        {
            velocity.y = 8f;
            animator.SetBool("Jump", true);
            int random = Random.Range(1, 3);
            FindObjectOfType<AudioManager>().Play("jump" + random);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        wasGrounded = isGrounded;
    }

    private void PlayFootsteps()
    {
        int randomNumber = Random.Range(1, 7);
        if (randomNumber == lastSound) return;
        lastSound = randomNumber;
        FindObjectOfType<AudioManager>().Play("PF" + randomNumber.ToString());
        targetTime += 1f / speed;
        if (targetTime <= 0.3f) targetTime = 0.3f;
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

    private Vector3 TranslateInputToVelocity(Vector2 input)
    {
        // Make the character move on the ground (XZ-plane)
        return new Vector3(input.x * speed, velocity.y, input.y * speed);
    }

    // Handle Move-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
    }

    // Handle Fire-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("FIRE!");
            // Play fire-animation and/or trigger sound etc
        }
    }
}