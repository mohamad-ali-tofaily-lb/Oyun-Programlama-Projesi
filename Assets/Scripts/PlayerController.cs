using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 6f; // No walk, just run
    public float crouchSpeed = 2f;
    public float gravity = 20f;
    public float jumpSpeed = 8f;

    [Header("Crouch Settings")]
    public float crouchHeight = 0.8f;
    public float standingHeight = 1.4f;
    public float crouchTransitionSpeed = 10f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool isCrouching = false;
    private float currentHeight;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentHeight = standingHeight;
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- MOUSE LOOK ---
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        // --- CROUCHING ---
        if (Input.GetKeyDown(KeyCode.C)) // Press C to toggle crouch
        {
            isCrouching = !isCrouching;
        }

        // Smoothly adjust the CharacterController's height
        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        characterController.height = currentHeight;

        // Adjust camera position to match height
        playerCamera.transform.localPosition = new Vector3(0, currentHeight - 0.2f, 0);

        // --- MOVEMENT & JUMPING ---
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // No walking, only running (or crouch speed)
        float curSpeedX = isCrouching ? crouchSpeed : runSpeed;
        float curSpeedY = isCrouching ? crouchSpeed : runSpeed;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * Input.GetAxis("Vertical") * curSpeedX) + (right * Input.GetAxis("Horizontal") * curSpeedY);

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}