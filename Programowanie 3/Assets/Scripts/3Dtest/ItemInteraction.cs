using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour, IInteractable
{
    public string GetUseText()
    {
        return "Take " + gameObject.name;
    }

    public void Use()
    {
        Destroy(gameObject);
    }
}
