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
    PlayerInput input;
    InputAction useAction;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        useAction = input.actions["Use"];
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Physics.Raycast(ray, out RaycastHit hit, checkDistance, interactionLayer)) // je�eli patrzymy na interakcj�
        {
            IInteractable interactable;
            if(hit.collider.TryGetComponent<IInteractable>(out interactable))
            {
                helpText.gameObject.SetActive(true);
                helpText.text = string.Format("Press {0} to {1}",
                    useAction.GetBindingDisplayString(), interactable.GetUseText());

                if (useAction.WasPerformedThisFrame())
                {
                    interactable.Use();
                }
            }

        }
        else // nie patrzymy na �adn� interakcj�
        {
            helpText.gameObject.SetActive(false);
        }

    }
}
