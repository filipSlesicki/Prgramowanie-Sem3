using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemPrice;
    [SerializeField] TMP_Text quantityText;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] GameObject descriptionWindow;

    [HideInInspector]public ItemSlot slot;

    public void Setup(ItemSlot slot)
    {
        image.sprite = slot.itemData.icon;
        itemName.text = slot.itemData.itemName;
        itemPrice.text = slot.itemData.price.ToString();
        itemDescription.text = slot.itemData.description;
        slot.SetItemUI(this);
        UpdateQuantityText();
    }

    public void ShowDescription(bool show)
    {
        descriptionWindow.SetActive(show);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging)
            return;

        ShowDescription(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShowDescription(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ShowDescription(false);
        transform.SetParent(transform.root);
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        MoveToSlot();
        image.raycastTarget = true;
    }

    public void MoveToSlot()
    {
        transform.SetParent(slot.transform);
        transform.localPosition = Vector3.zero;
    }

    public void UpdateQuantityText()
    {
        if(slot)
        quantityText.text = slot.quantity.ToString();
    }
}
