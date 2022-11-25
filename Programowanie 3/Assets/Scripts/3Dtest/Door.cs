using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    Animator anim;
    bool isOpen;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Use()
    {
        Vector3 dirToPlayer = FirstPersonController.instance.transform.position - transform.position;
        float dot = Vector3.Dot(transform.forward, dirToPlayer);
        anim.SetFloat("Dot", dot);
        anim.SetTrigger("Open");
        isOpen = !isOpen;
    }

    public string GetUseText()
    {
        if(isOpen)
        {
            return "close";
        }
        else
        {
            return "open";
        }
    }

    public void OnFocus()
    {
        Debug.Log("Focus");
    }

    public void OnUnFocus()
    {
        Debug.Log("UnFocus");
    }
}
