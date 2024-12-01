using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this is a template method pattern
public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    [SerializeField]
    public string promptMessage;
    public void BaseInteract()
    {
        if(useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }
    protected virtual void Interact()
    {
        // This function is going to be overridden by our subclass
    }
}
