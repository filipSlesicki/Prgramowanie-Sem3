using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] Material unfocusedMat;
    [SerializeField] Material focusedMat;
    [SerializeField] Item item;
    MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    public string GetUseText()
    {
        return "Take " + gameObject.name;
    }

    public void Use()
    {
        FindObjectOfType<Inventory>().AddItem(item);
        Destroy(gameObject);
    }

    public void OnFocus()
    {
        rend.material = focusedMat;
    }

    public void OnUnFocus()
    {
        if(rend)
        {
            rend.material = unfocusedMat;
        }
    }
}
