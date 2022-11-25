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
}
