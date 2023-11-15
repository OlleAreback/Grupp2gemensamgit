using UnityEngine;
using UnityEngine.InputSystem;

// This component lets you rotate an object based on Look-input (mouse movement)
// Use with the component PlayerInput
// You will need to assign which transforms should be rotated horizontally and vertically
public class RotationController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 3f;
    [SerializeField] private float verticalSpeed = -0.2f;
    [SerializeField] private Transform rotateHorizontally;
    [SerializeField] private Transform rotateVertically;
    
    private Vector2 lookInput;

    // Locks cursor on enable and unlocks on disable
    // Suggestion: Disable this component when entering a menu and enable when entering gameplay
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // Only rotate horizontally if a transform has been assigned
        if (rotateHorizontally != null)
        {
            rotateHorizontally.Rotate(Vector3.up, lookInput.x * 180 * horizontalSpeed * Time.deltaTime);
        }
        // Only rotate vertically if a transform has been assigned
        if (rotateVertically != null)
        {
            rotateVertically.Rotate(Vector3.right, lookInput.y * 180 * verticalSpeed * Time.deltaTime);
        }
    }
    
    // Handle Look-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>().normalized;
    }
}
