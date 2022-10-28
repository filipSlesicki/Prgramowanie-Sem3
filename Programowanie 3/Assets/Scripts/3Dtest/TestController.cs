using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    CharacterController controller;

    Vector2 moveInput;
    InputAction moveAction;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //moveAction = GetComponent<PlayerInput>().actions["Move"];
    }

    // Update is called once per frame
    void Update()
    {
        //moveInput = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, moveInput.y, 0);
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log(context.phase);
        }

    }

    void OnMove(InputValue value)
    {
        Debug.Log(value.Get<Vector2>());
        moveInput = value.Get<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
