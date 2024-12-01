using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TimerScript : MonoBehaviour
{
    [Header("UI & Timer Settings")]
    [SerializeField] private TextMeshProUGUI TimerTxt;               
    public float TimeLeft = 30f;                                     
    public bool TimerOn = false;

    [Header("Object Replacement")]
    [SerializeField] private List<GameObject> objectsToReplace;      
    [SerializeField] private List<GameObject> prefabsToReplaceWith;  

    [Header("Skybox Settings")]
    [SerializeField] private Material lightWorldSkybox;              
    [SerializeField] private Material darkWorldSkybox;               

    [Header("Audio Settings")]
    [SerializeField] public AK.Wwise.Event Music;                   
    [SerializeField] public AK.Wwise.State Light;                   
    [SerializeField] public AK.Wwise.State Dark;                    
    [SerializeField] public AK.Wwise.State Life;                    

    private bool isPlaying = false;                                 

    [Header("Canvas Settings")]
    [SerializeField] private GameObject flickerCanvas;              

    public enum WorldState
    {
        Light,
        Dark
    }

    [Header("World State")]
    public WorldState currentWorldState = WorldState.Light;         
    private Dictionary<GameObject, GameObject> objectPrefabMapping = new Dictionary<GameObject, GameObject>();

    private int currentWorld = 1;                                   

    private void Start()
    {
        TimerOn = true;
        UpdateWorldDisplay();
        InitializeObjectMapping();
        UpdateSkybox();
        InitializeAudio();

        if (flickerCanvas != null)
        {
            flickerCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        HandleTimer();
        HandleAudioPauseResume();
    }

    private void HandleTimer()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                TimerOn = false;
                TimeLeft = 0;
                StartCoroutine(HandleTimerEnd());
            }
        }
    }

    private IEnumerator HandleTimerEnd()
    {
        Debug.Log("Time is UP! Transitioning to the next world...");

        currentWorld++;
        currentWorldState = (currentWorldState == WorldState.Light) ? WorldState.Dark : WorldState.Light;

        Debug.Log($"World state changed to: {currentWorldState}");

        UpdateAudioWorldState();
        UpdateSkybox();
        ReplaceAllClonesWithPrefabs();

        if (flickerCanvas != null)
        {
            flickerCanvas.SetActive(true);
            yield return StartCoroutine(FlickerCanvas(13f));
            flickerCanvas.SetActive(false);
        }

        ResetTimer();
    }

    public void ResetTimer()
    {
        TimeLeft = 30f;
        TimerOn = true;
        Debug.Log("Timer has been reset.");
    }

    private IEnumerator FlickerCanvas(float duration)
    {
        float elapsed = 0f;
        bool isVisible = false;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (elapsed % 0.2f < 0.1f)
            {
                isVisible = !isVisible;
                flickerCanvas.SetActive(isVisible);
            }

            yield return null;
        }

        flickerCanvas.SetActive(false);
    }

    private void HandleAudioPauseResume()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPlaying)
            {
                Music.ExecuteAction(gameObject, AkActionOnEventType.AkActionOnEventType_Pause, 500, AkCurveInterpolation.AkCurveInterpolation_Log1);
                Debug.Log("Music paused.");
            }
            else
            {
                Music.ExecuteAction(gameObject, AkActionOnEventType.AkActionOnEventType_Resume, 500, AkCurveInterpolation.AkCurveInterpolation_Log1);
                Debug.Log("Music resumed.");
            }
            isPlaying = !isPlaying;
        }
    }

    private void InitializeAudio()
    {
        if (Music.IsValid())
        {
            Light.SetValue();
            Life.SetValue();
            Music.Post(gameObject);
            isPlaying = true;
            Debug.Log("Music started with Light and Life states.");
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateWorldDisplay()
    {
        Debug.Log("Current World: World " + currentWorld);
    }

    private void InitializeObjectMapping()
    {
        if (objectsToReplace.Count == prefabsToReplaceWith.Count)
        {
            for (int i = 0; i < objectsToReplace.Count; i++)
            {
                objectPrefabMapping[objectsToReplace[i]] = prefabsToReplaceWith[i];
            }
        }
        else
        {
            Debug.LogError("The number of objects to replace and prefabs to replace with do not match!");
        }
    }

    private void ReplaceAllClonesWithPrefabs()
    {
        List<GameObject> sourceList = currentWorldState == WorldState.Dark ? objectsToReplace : prefabsToReplaceWith;
        List<GameObject> targetList = currentWorldState == WorldState.Dark ? prefabsToReplaceWith : objectsToReplace;

        if (sourceList.Count != targetList.Count)
        {
            Debug.LogError("Source and target lists do not match in size.");
            return;
        }

        Debug.Log($"Replacing objects: {sourceList.Count} items to process.");

        for (int i = 0; i < sourceList.Count; i++)
        {
            GameObject sourceObject = sourceList[i];
            GameObject targetPrefab = targetList[i];

            if (sourceObject == null || targetPrefab == null)
            {
                Debug.LogError($"Null reference in object replacement: Source: {sourceObject}, Target: {targetPrefab}");
                continue;
            }

            GameObject[] allObjects = FindObjectsOfType<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == sourceObject.name || obj.name == sourceObject.name + "(Clone)")
                {
                    Vector3 originalPosition = obj.transform.position;
                    Quaternion originalRotation = obj.transform.rotation;

                    GameObject newObject = Instantiate(targetPrefab, originalPosition, originalRotation);
                    newObject.transform.parent = obj.transform.parent;

                    Objects objectsScript = newObject.GetComponent<Objects>();
                    if (objectsScript != null)
                    {
                        objectsScript.FindLand();
                    }

                    Destroy(obj);
                }
            }
        }
    }

    private void UpdateSkybox()
    {
        RenderSettings.skybox = currentWorldState == WorldState.Light ? lightWorldSkybox : darkWorldSkybox;
        DynamicGI.UpdateEnvironment();
    }

    private void UpdateAudioWorldState()
    {
        if (currentWorldState == WorldState.Light)
        {
            Light.SetValue();
            Debug.Log("Audio state changed to Light.");
        }
        else if (currentWorldState == WorldState.Dark)
        {
            Dark.SetValue();
            Debug.Log("Audio state changed to Dark.");
        }
    }

    public void AddTime(float extraTime)
    {
        TimeLeft += extraTime;
        Debug.Log($"Added {extraTime} seconds. New time: {TimeLeft} seconds.");

        if (TimeLeft > 0 && !TimerOn)
        {
            TimerOn = true;
            Debug.Log("Timer restarted with added time.");
        }
    }
}
