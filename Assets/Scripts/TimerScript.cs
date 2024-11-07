using UnityEngine;
using TMPro;
using System.Collections.Generic;  // For using lists

public class TimerScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerTxt;               // TMP text for displaying the timer
    [SerializeField] private List<GameObject> objectsToReplace;       // List of objects to replace
    [SerializeField] private List<GameObject> prefabsToReplaceWith;   // List of prefabs to replace with
    public float TimeLeft = 30f;                                      // Starting time for the timer
    private bool TimerOn = false;

    void Start()
    {
        TimerOn = true;
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;  // Countdown timer
                updateTimer(TimeLeft);       // Update timer display
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;

                // When the timer hits zero, replace the objects with the corresponding prefabs
                ReplaceObjectsWithPrefabs();
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;  // Add 1 second buffer for display

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        // Update the timer text
        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Method to replace all objects in the objectsToReplace list with their corresponding prefabs
    private void ReplaceObjectsWithPrefabs()
    {
        if (objectsToReplace.Count == prefabsToReplaceWith.Count)
        {
            for (int i = 0; i < objectsToReplace.Count; i++)
            {
                GameObject objToReplace = objectsToReplace[i];
                GameObject prefabToReplaceWith = prefabsToReplaceWith[i];

                if (prefabToReplaceWith != null)
                {
                    // Instantiate the prefab at the same position and rotation as the current object
                    Instantiate(prefabToReplaceWith, objToReplace.transform.position, objToReplace.transform.rotation);

                    // Destroy the current object
                    Destroy(objToReplace);
                }
                else
                {
                    Debug.LogError("Prefab to replace with is not assigned for object " + objToReplace.name);
                }
            }
        }
        else
        {
            Debug.LogError("The number of objects to replace and prefabs to replace with do not match!");
        }
    }
}
