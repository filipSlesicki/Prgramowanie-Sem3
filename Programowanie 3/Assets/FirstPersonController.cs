using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float sprintSpeed = 8;
    [SerializeField] float crouchSpeed = 3;
    [SerializeField] float jumpForce = 5;
    float verticalVelocity;
    MoveState moveState;

    [Header("Looking")]
    [SerializeField] Transform cam;
    [SerializeField] float rotationSpeed = 10;
    [Range(0,90)]
    [SerializeField] float maxCameraRot = 70;
    float cameraPitch;

    Vector2 moveInput;
    Vector2 rotInput;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        ApplyGravity();
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

        //float targetSpeed = sprinting ? sprintSpeed : moveSpeed;
        //if(sprinting)
        //{
        //    targetSpeed = sprintSpeed;
        //}
        //else
        //{
        //    targetSpeed = moveSpeed;
        //}
        float currentSpeed = targetSpeed;
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
        //cam.localRotation = Quaternion.Euler(new Vector3(cameraPitch, 0, 0));
    }

    float GetMovementSpeed()
    {
        switch (moveState)
        {
            case MoveState.Standing:
                return moveSpeed;
            case MoveState.Sprinting:
                return sprintSpeed;
            case MoveState.Crouching:
                return crouchSpeed;
            default:
                return 0;
        }
    }

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
            moveState = MoveState.Sprinting;
        }
        if(context.canceled)
        {
            moveState = MoveState.Standing;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveState = MoveState.Crouching;
        }
        if (context.canceled)
        {
            moveState = MoveState.Standing;
        }
    }

    enum MoveState
    {
        Standing,
        Sprinting,
        Crouching
    }

}
