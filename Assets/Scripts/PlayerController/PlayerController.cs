using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movementControl;
    [SerializeField] private InputActionReference jumpControl;
    private CharacterController controller;
    private Animator animator;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float maxSpeed = 6.0f;
    private float previousSpeed;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private Transform cameraMainTransform;
    private int lastSound = 0;
    private float targetTime;

    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
    }

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            previousSpeed = playerSpeed;
            playerSpeed = maxSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = previousSpeed;
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraMainTransform.forward.normalized * move.z + cameraMainTransform.right.normalized * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (jumpControl.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.SetBool("Jump", true);
            int random = Random.Range(1, 3);
            FindObjectOfType<AudioManager>().Play("jump" + random);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        targetTime -= Time.deltaTime;

        if (movementControl.action.ReadValue<Vector2>() != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y; ;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            animator.SetFloat("Speed", playerSpeed);
            if (targetTime <= 0 && groundedPlayer) PlayFootsteps();
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    private void PlayFootsteps()
    {
        int randomNumber = Random.Range(1, 7);
        if (randomNumber == lastSound) return;
        lastSound = randomNumber;
        FindObjectOfType<AudioManager>().Play("PF" + randomNumber.ToString());
        targetTime += 1f / playerSpeed;
        if (targetTime <= 0.3f) targetTime = 0.3f;
    }
}