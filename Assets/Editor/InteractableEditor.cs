using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    // This method is called to draw the custom inspector
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if(target.GetType() == typeof(EventOnlyinteractable))
        {
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyinteractable only uses UnityEvents", MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                InteractionEvent interactionEvent = interactable.GetComponent<InteractionEvent>();
            }
        }
        else
        {
            // Draw the default inspector fields
            base.OnInspectorGUI();

            // Check if useEvents is true
            if (interactable.useEvents)
            {
                // Check if the InteractionEvent component is already attached
                // - InteractionEvent interactionEvent = interactable.GetComponent<InteractionEvent>();
                if (interactable.GetComponent<InteractionEvent>() == null)
                {
                    // If not, add the InteractionEvent component and register the action in the Undo system
                    // - Undo.RecordObject(interactable, "Add InteractionEvent");
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }
            else
            {
                // If useEvents is false, ensure InteractionEvent component is removed
                // - InteractionEvent interactionEvent = interactable.GetComponent<InteractionEvent>();
                if (interactable.GetComponent<InteractionEvent>() != null)
                {
                    // If it's there, remove it and record this action for Undo
                    // - Undo.RecordObject(interactable, "Remove InteractionEvent");
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
                }
            }
        }
    }
}
