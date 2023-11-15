using UnityEngine;
using UnityEngine.InputSystem;

// Moves the character on the XZ-plane.
// Not intended for slopes and/or gravity.
// Use with the components Rigidbody and PlayerInput.
// Rigidbody should be set to Freeze Position Y and Freeze Rotation X and Z.
// PlayerInput should be set to Behavior: Invoke Unity Events
public class SimpleMovementControllerXZ : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Animator animator;

    private Vector2 move;

    void Update()
    {
        rigidbody.velocity = transform.TransformDirection(new Vector3(move.x * speed, 0, move.y * speed));

        animator.SetBool("Moving", move.magnitude > 0);
    }

    // This method can be used through the UnityEvent in PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>().normalized;
    }
}
