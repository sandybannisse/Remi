using UnityEngine;
using UnityEngine.Events;

public class InteractionEvent : MonoBehaviour
{
    public UnityEvent OnInteract;  // UnityEvent to trigger when interaction happens
    private bool isInteracting = false;  // Flag to prevent multiple interactions
    public float interactionCooldown = 300f;  // Cooldown period in seconds

    // Called when the interaction happens (could be from player input or other sources)
    public void TriggerInteract()
    {
        if (!isInteracting)  // Ensure we're not triggering interactions too fast
        {
            isInteracting = true;
            OnInteract.Invoke();  // Trigger the UnityEvent
            
            // After the cooldown period, allow interaction again
            Invoke("ResetInteractionFlag", interactionCooldown);  
        }
    }

    private void ResetInteractionFlag()
    {
        isInteracting = false;  // Allow the interaction to happen again after the cooldown
    }
}
