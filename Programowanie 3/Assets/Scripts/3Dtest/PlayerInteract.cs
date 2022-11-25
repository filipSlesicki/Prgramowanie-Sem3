using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] float checkDistance = 2;
    [SerializeField] TMP_Text helpText;

    IInteractable currentInteraction;
    PlayerInput input;
    InputAction useAction;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        useAction = input.actions["Use"];
        helpText.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Physics.Raycast(ray, out RaycastHit hit, checkDistance, interactionLayer)) // je¿eli patrzymy na interakcjê
        {
            IInteractable interactable;
            if(hit.collider.TryGetComponent<IInteractable>(out interactable))
            {
                if(currentInteraction != interactable) // wczeœniej patrzyliœmy na inn¹ interakcjê (albo na nic)
                {
                    currentInteraction?.OnUnFocus(); 
                    currentInteraction = interactable;
                    currentInteraction.OnFocus();
                }
                
                helpText.gameObject.SetActive(true);
                helpText.text = string.Format("Press {0} to {1}",
                    useAction.GetBindingDisplayString(), interactable.GetUseText());

                if (useAction.WasPerformedThisFrame())
                {
                    interactable.Use();
                }
            }

        }
        else // nie patrzymy na ¿adn¹ interakcjê
        {
            if(currentInteraction != null)
            {
                currentInteraction.OnUnFocus();
                helpText.gameObject.SetActive(false);
                currentInteraction = null;
            }

        }

    }
}
