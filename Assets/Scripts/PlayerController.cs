using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Movement")]
    public float walkSpeed = 11f;
    public float runSpeed = 6f; 
    public float crouchSpeed = 5f;
    public float gravity = 20f;
    public float jumpSpeed = 13f;

    [Header("Crouch Settings")]
    public float crouchHeight = 0.8f;
    public float standingHeight = 1.8f;
    public float crouchTransitionSpeed = 10f;
    
    [Header("Camera Settings")]
    [Tooltip("Lower this value if the camera is too high. Try -0.2 or -0.3")]
    public float cameraHeightOffset = -0.2f; 

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool isCrouching = false;
    private float currentHeight;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentHeight = standingHeight;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 🚨 PAUSE FIX: Stop processing movement/looking if the game is paused
        if (PauseMenu.GameIsPaused) return;

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        if (Input.GetKeyDown(KeyCode.C)) isCrouching = !isCrouching;

        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        characterController.height = currentHeight;

        // HEIGHT FIX APPLIED HERE
        playerCamera.transform.localPosition = new Vector3(0, currentHeight + cameraHeightOffset, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = isCrouching ? crouchSpeed : runSpeed;
        float curSpeedY = isCrouching ? crouchSpeed : runSpeed;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * Input.GetAxis("Vertical") * curSpeedX) + (right * Input.GetAxis("Horizontal") * curSpeedY);

        if (Input.GetButton("Jump") && characterController.isGrounded)
            moveDirection.y = jumpSpeed;
        else
            moveDirection.y = movementDirectionY;

        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}