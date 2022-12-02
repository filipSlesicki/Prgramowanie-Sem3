using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] PlayerInput input;

    string gameplayActionMap = "PlayerNormal";
    string uiActionMap = "UI";

    private void Awake()
    {
        instance = this;
    }

    public void SwitchToUIInput()
    {
        input.SwitchCurrentActionMap(uiActionMap);
        Cursor.lockState = CursorLockMode.None;
    }

    public void SwitchToGamplayInput()
    {
        input.SwitchCurrentActionMap(gameplayActionMap);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
