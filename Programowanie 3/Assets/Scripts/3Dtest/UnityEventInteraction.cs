using UnityEngine;
using UnityEngine.Events;

public class UnityEventInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent useEvent;
    [SerializeField] string useText;

    public string GetUseText()
    {
        return useText;
    }

    public void Use()
    {
        useEvent.Invoke();
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
