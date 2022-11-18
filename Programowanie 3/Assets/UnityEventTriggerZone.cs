using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventTriggerZone : MonoBehaviour
{
    [SerializeField] UnityEvent onEnterEvent;
    [SerializeField] UnityEvent onExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        onEnterEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        onExitEvent.Invoke();
    }
}
