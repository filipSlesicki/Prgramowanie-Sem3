using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    [SerializeField] GameObject inventoryWindow;
    

    public void OpenInventory(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            inventoryWindow.SetActive(true);
            UIManager.instance.SwitchToUIInput();
        }
    }
    public void CloseInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventoryWindow.SetActive(false);
            UIManager.instance.SwitchToGamplayInput();
        }
    }

}