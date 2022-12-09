using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public Item itemData;
    public InventoryItem itemUI;
    public int quantity = 1;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (droppedItem == null)
            return;

        if (droppedItem == itemUI)
            return;

        ItemSlot droppedItemSlot = droppedItem.slot;
        Item droppedItemData = droppedItemSlot.itemData;

        //Je¿eli slot jest pusty
        if(itemData == null)
        {
            //Wk³adamy item w ten slot
            SetItemUI(droppedItem);
            itemData = droppedItemData;
            quantity = droppedItemSlot.quantity;
            itemUI.UpdateQuantityText();
            //Czyœcimy poprzedni slot
            droppedItemSlot.EmptySlot();
            return;
        }

        //Je¿eli slot nie jest pusty

        //Je¿eli to jest ten sam typ przedmiotu, to zwiêkszamy iloœæ
        if(itemData == droppedItemData)
        {
            //Do tego slotu dodajemy iloœæ z poprzedniego slotu
            quantity += droppedItemSlot.quantity;
            itemUI.UpdateQuantityText();
            droppedItemSlot.EmptySlot();
            Destroy(droppedItem.gameObject);
        }
        else
        {
            //Zamieniamy je miejscami
            //Item z tego slotu wk³adamy w poprzedni slot upuszczanego przedmiotu
            droppedItemSlot.itemData = this.itemData;
            droppedItemSlot.SetItemUI(this.itemUI);
            int droppedSlotQuantity = droppedItemSlot.quantity;
            droppedItemSlot.quantity = this.quantity;
            itemUI.UpdateQuantityText();
            //Przesuwamy UI itemu z tego slota na slot upuszczanego przedmiotu
            itemUI.MoveToSlot();

            //Wk³adamy upuszczony item w ten slot
            SetItemUI(droppedItem);
            itemData = droppedItemData;
            this.quantity = droppedSlotQuantity;
            itemUI.UpdateQuantityText();
        }


        
    }

    public void EmptySlot()
    {
        itemData = null;
        itemUI = null;
        quantity = 1;
    }

    public void SetItemUI(InventoryItem itemUI)
    {
        this.itemUI = itemUI;
        itemUI.slot = this;
    }
}
