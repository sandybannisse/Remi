using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections; // Required for IEnumerator


public class Collection : MonoBehaviour
{
    private int coinCount = 0;
    private int coinCountOne = 0;
    private int coinCountTwo = 0;
    private int coinCountThree = 0;

    private bool hasCollectedOne = false;
    private bool hasCollectedTwo = false;
    private bool hasCollectedThree = false;

    public int level = 1; // Tracks the player's level
    private int timerMissCount = 0; // Tracks how many times the timer hit 0 without success
    private bool levelUpTriggered = false; // Flag to check if level-up was achieved

    // TextMeshPro references for displaying counts
    public TextMeshProUGUI coinCountOneText;
    public TextMeshProUGUI coinCountTwoText;
    public TextMeshProUGUI coinCountThreeText;
    public TextMeshProUGUI levelText; // TextMeshProUGUI for level display

    public GameObject blackScreen; // Reference to the black screen UI
    public AK.Wwise.Event gameOverSound; // Wwise Game Over sound event

    private TimerScript timerScript;

    private void Start()
    {
        // Initialize TextMeshPro displays
        UpdateTextDisplays();

        // Find the TimerScript in the scene
        timerScript = FindObjectOfType<TimerScript>();

        if (timerScript == null)
        {
            Debug.LogError("TimerScript not found in the scene!");
        }

        // Update the level display at the start
        UpdateLevelDisplay();

        // Ensure the black screen is initially hidden
        if (blackScreen != null)
        {
            blackScreen.SetActive(false);
        }
    }

    private void Update()
    {
        // Check for timer expiration
        if (timerScript != null && timerScript.TimeLeft <= 0 && !timerScript.TimerOn)
        {
            if (levelUpTriggered)
            {
                // Reset counters when the timer reaches 0 after level up
                ResetCounters();
                levelUpTriggered = false; // Reset the flag
            }
            else
            {
                // Handle regular timer expiration (no level-up)
                timerMissCount++;
                Debug.Log($"Timer expired! Miss count: {timerMissCount}");

                if (timerMissCount >= 3)
                {
                    Debug.Log("Failed to collect 3 coins in time. Game Over!");
                    TriggerGameOver();
                }
            }

            // Restart the timer
            timerScript.ResetTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("canPickUp"))
        {
            string objectName = other.gameObject.name;

            // Play Wwise events based on object name
            if (objectName.Contains("IceCream"))
            {
                AkSoundEngine.PostEvent("IcecreamCollect", gameObject);
                Debug.Log("Collected Ice Cream! Event Triggered.");
            }
            else if (objectName.Contains("Potion"))
            {
                AkSoundEngine.PostEvent("PotionCollect", gameObject);
                Debug.Log("Collected Potion! Event Triggered.");
            }

            // Increment specific coin counters if not already collected
            if (objectName.Contains("_1") && !hasCollectedOne)
            {
                coinCountOne++;
                hasCollectedOne = true;
                Debug.Log("coinCountOne increased: " + coinCountOne);
            }
            else if (objectName.Contains("_2") && !hasCollectedTwo)
            {
                coinCountTwo++;
                hasCollectedTwo = true;
                Debug.Log("coinCountTwo increased: " + coinCountTwo);
            }
            else if (objectName.Contains("_3") && !hasCollectedThree)
            {
                coinCountThree++;
                hasCollectedThree = true;
                Debug.Log("coinCountThree increased: " + coinCountThree);
            }

            // Update text displays after each increment
            UpdateTextDisplays();

            // Increment total coin count
            coinCount++;
            Debug.Log("Total Coins Collected: " + coinCount);

            // Check for level progression
            if (coinCount >= 3 && !levelUpTriggered)
            {
                Debug.Log("Level Up Achieved! Waiting for timer reset.");
                LevelUp();
            }

            // Destroy the collected object
            Destroy(other.gameObject);
        }
    }

    private void LevelUp()
    {
        level++;
        levelUpTriggered = true; // Mark that a level-up has been triggered
        UpdateLevelDisplay(); // Update the level display

        Debug.Log($"Level increased to {level}");
    }

    private void ResetCounters()
    {
        coinCount = 0;
        coinCountOne = 0;
        coinCountTwo = 0;
        coinCountThree = 0;

        hasCollectedOne = false;
        hasCollectedTwo = false;
        hasCollectedThree = false;

        // Update text displays after resetting counters
        UpdateTextDisplays();

        Debug.Log("Counters reset: coinCount, coinCountOne, coinCountTwo, coinCountThree are now 0.");
    }

    private void UpdateTextDisplays()
    {
        if (coinCountOneText != null)
        {
            coinCountOneText.text = "" + coinCountOne;
        }
        if (coinCountTwoText != null)
        {
            coinCountTwoText.text = "" + coinCountTwo;
        }
        if (coinCountThreeText != null)
        {
            coinCountThreeText.text = "" + coinCountThree;
        }
    }

    private void UpdateLevelDisplay()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + level; // Update the level text
        }
    }

    private void TriggerGameOver()
    {
        // Play the Wwise Game Over sound
        if (gameOverSound != null)
        {
            gameOverSound.Post(gameObject);
        }

        // Activate the black screen
        if (blackScreen != null)
        {
            blackScreen.SetActive(true);
        }

        // Wait briefly before loading the Game Over scene
        StartCoroutine(DelayedGameOver());
    }

    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(10f); // Delay before scene transition
        SceneManager.LoadScene("GameOver");
    }
}
