using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] Material unfocusedMat;
    [SerializeField] Material focusedMat;
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
        Destroy(gameObject);
    }

    public void OnFocus()
    {
        rend.material = focusedMat;
        Debug.Log("Focus");
    }

    public void OnUnFocus()
    {
        rend.material = unfocusedMat;
        Debug.Log("UnFocus");
    }
}
