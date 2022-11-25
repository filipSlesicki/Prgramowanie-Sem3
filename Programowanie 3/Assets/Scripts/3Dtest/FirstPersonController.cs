using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    public static FirstPersonController instance;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float sprintSpeed = 8;
    [SerializeField] float crouchSpeed = 3;
    [SerializeField] float acceleration = 10;
    [SerializeField] float jumpForce = 5;
    float lastSpeed;
    float verticalVelocity;
    [SerializeField] MoveState moveState;

    [Header("Looking")]
    [SerializeField] Transform cam;
    [SerializeField] float rotationSpeed = 10;
    [Range(0,90)]
    [SerializeField] float maxCameraRot = 70;
    float cameraPitch;

    [Header("Crouching")]
    [SerializeField] float crouchAmount = 1;
    [SerializeField] float crouchTime = 0.3f;
    [SerializeField] float crouchProgress;
    bool wantToCrouch = false;

    //Te zmienne ustawimy w Starcie
    float standingHeight;
    float crouchingHeight;
    Vector3 standingCenter;
    Vector3 crouchingCenter;
    Vector3 standingCameraPosition;
    Vector3 crouchingCameraPosition;

    Vector2 moveInput;
    Vector2 rotInput;
    bool sprintPressed;
    bool crouchPressed;

    CharacterController controller;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        standingHeight = controller.height;
        crouchingHeight = standingHeight - crouchAmount;
        standingCenter = controller.center;
        crouchingCenter = standingCenter + Vector3.down * crouchAmount / 2;
        standingCameraPosition = cam.localPosition;
        crouchingCameraPosition = cam.localPosition + Vector3.down * crouchAmount;
    }

    void Update()
    {
        ApplyGravity();
        UpdateCrouch();
        Move();
        BodyLook();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void ApplyGravity()
    {
        if(controller.isGrounded)
        {
            if(verticalVelocity < -2)
            {
                //land
                verticalVelocity = 0;
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
       
    }

    void Move()
    {
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        float targetSpeed = GetMovementSpeed();

        float currentSpeed = Mathf.MoveTowards(lastSpeed, targetSpeed, acceleration * Time.deltaTime);
        lastSpeed = moveDirection.magnitude * currentSpeed;
        Vector3 moveVector = moveDirection * currentSpeed + Vector3.up * verticalVelocity;
        controller.Move(moveVector * Time.deltaTime);
    }

    void BodyLook()
    {
        transform.Rotate(0, rotInput.x * rotationSpeed * Time.deltaTime, 0);
    }

    void CameraLook()
    {
        cameraPitch += rotInput.y * rotationSpeed * Time.deltaTime;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxCameraRot, maxCameraRot);
        cam.localEulerAngles = new Vector3(cameraPitch, 0, 0);
    }

    float GetMovementSpeed()
    {
        switch (moveState)
        {
            case MoveState.Walking:
                return moveSpeed;
            case MoveState.Sprinting:
                return sprintSpeed;
            case MoveState.Crouching:
                return crouchSpeed;
            default:
                return 0;
        }
    }

    void UpdateCrouch()
    {
         if(wantToCrouch) //Kucamy
        {
            if (crouchProgress >= 1) // ca³kowite kucniêcie
                return;

            crouchProgress += Time.deltaTime / crouchTime;
        }
         else // Wstajemy
        {
            if (crouchProgress <= 0) // ca³kowite wstanie
                return;

            if(Physics.Raycast(transform.position,Vector3.up,standingHeight/2))
            {
                return;
            }
            crouchProgress -= Time.deltaTime / crouchTime;

            if (crouchProgress <= 0)
            {
                if (sprintPressed)
                    moveState = MoveState.Sprinting;
                else
                    moveState = MoveState.Walking;
            }
        }
        controller.height = Mathf.Lerp(standingHeight,crouchingHeight,crouchProgress);
        controller.center = Vector3.Lerp(standingCenter, crouchingCenter, crouchProgress);
        cam.localPosition = Vector3.Lerp(standingCameraPosition, crouchingCameraPosition, crouchProgress);

    }

    #region Get Inputs

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        rotInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(controller.isGrounded)
            {
                verticalVelocity = jumpForce;
            }
   
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            sprintPressed = true;

            wantToCrouch = false;
            if(moveState != MoveState.Crouching)
            {
                moveState = MoveState.Sprinting;
            }

        }
        if(context.canceled)
        {
            sprintPressed = false;
            if (moveState != MoveState.Sprinting)
                return;

            if (crouchPressed)
            {
                moveState = MoveState.Crouching;
                wantToCrouch = true;
            }
            else
            moveState = MoveState.Walking;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            crouchPressed = true;
            moveState = MoveState.Crouching;
            wantToCrouch = true;
        }
        if (context.canceled)
        {
            crouchPressed = false;
            wantToCrouch = false;
        }
    }
    #endregion

    enum MoveState
    {
        Walking,
        Sprinting,
        Crouching
    }

}
