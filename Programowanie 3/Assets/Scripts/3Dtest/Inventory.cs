using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField] ItemSlot[] slots;
    [SerializeField] GameObject inventoryWindow;
    [SerializeField] InventoryItem inventoryItemPrefab;


    private void Start()
    {
        foreach (var slot in slots)
        {
            if(slot.itemData != null)
            {
                InventoryItem inventoryItem = Instantiate(inventoryItemPrefab, slot.transform);
                inventoryItem.Setup(slot);
            }
        }
    }

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

    public void AddItem(Item item)
    {
        foreach (var slot in slots)
        {
            if(slot.itemData == null)
            {
                slot.itemData = item;
                InventoryItem inventoryItem = Instantiate(inventoryItemPrefab, slot.transform);
                inventoryItem.Setup(slot);
                return;
            }
        }
        Debug.LogWarning("Nie zmieœci³ siê nam w ekwipunku");
    }

}