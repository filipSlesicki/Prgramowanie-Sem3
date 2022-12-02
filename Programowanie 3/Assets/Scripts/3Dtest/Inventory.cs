using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    [SerializeField] GameObject inventoryWindow;
    [SerializeField] InventoryItem inventoryItemPrefab;

    List<GameObject> createdIcons = new List<GameObject>();

    public void OpenInventory(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            inventoryWindow.SetActive(true);
            UIManager.instance.SwitchToUIInput();
            foreach (var item in items)
            {
                InventoryItem inventoryItem = Instantiate(inventoryItemPrefab, inventoryWindow.transform);
                inventoryItem.Setup(item);
                createdIcons.Add(inventoryItem.gameObject);
            }
        }
    }
    public void CloseInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventoryWindow.SetActive(false);
            UIManager.instance.SwitchToGamplayInput();
            foreach (var icon in createdIcons)
            {
                Destroy(icon);
            }
            createdIcons.Clear();
        }
    }

}