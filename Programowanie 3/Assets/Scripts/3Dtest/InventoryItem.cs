using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] GameObject descriptionWindow;

    public void Setup(Item item)
    {
        image.sprite = item.icon;
        itemName.text = item.itemName;
        itemDescription.text = item.description;
    }

    public void ShowDescription(bool show)
    {
        descriptionWindow.SetActive(show);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ShowDescription(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ShowDescription(false);
    }
}
