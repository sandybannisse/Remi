using UnityEngine;

public class WorldAudioManager : MonoBehaviour
{ /*
    [SerializeField] public AK.Wwise.Event Music;       // Wwise Music Event
    [SerializeField] public AK.Wwise.State Light;       // Wwise State for Light world
    [SerializeField] public AK.Wwise.State Dark;        // Wwise State for Dark world
    [SerializeField] public AK.Wwise.State Life;        // Wwise State for Life
    //[SerializeField] public AK.Wwise.State Dead;      // Uncomment if using Dead state

    public enum WorldState
    {
        Light,
        Dark
    }

    [Header("Select World State")]
    public WorldState currentWorldState = WorldState.Light; // Default to Light world

    private bool isPlaying = false; // Tracks if music is currently playing

    private void Start()
    {
        // Initialize the music and set default states
        if (Music.IsValid())
        {
            Light.SetValue(); // Default to Light world state
            Life.SetValue();  // Set Life state
            Music.Post(gameObject); // Start playing the music
            isPlaying = true;
            Debug.Log("Music started with Light and Life states.");
        }
    }

    private void Update()
    {
        // Handle pause/resume functionality with key input (for debugging/testing)
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

    /// <summary>
    /// Updates the world state and applies the corresponding Wwise state.
    /// </summary>
    /// <param name="newState">The new world state (Light or Dark).</param>
    public void UpdateWorldState(WorldState newState)
    {
        currentWorldState = newState;

        if (currentWorldState == WorldState.Light)
        {
            Light.SetValue(); // Set Wwise state to Light
            Debug.Log("WorldAudioManager: State changed to Light.");
        }
        else if (currentWorldState == WorldState.Dark)
        {
            Dark.SetValue(); // Set Wwise state to Dark
            Debug.Log("WorldAudioManager: State changed to Dark.");
        }
    }*/
}
