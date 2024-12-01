using UnityEngine;
using UnityEngine.UI;
using System.Collections; 

public class PlayerHealth : MonoBehaviour
{
    // Public fields to be shown in the Inspector
    public float maxHealth = 100f;
    public Image frontHealthBar;  // The UI Image that will represent the health bar
    public Image backHealthBar;   // The UI Image that will represent the back of the health bar
    public float chipSpeed = 2f;  // Speed at which health decreases, if you plan to use this value

    private float health;         // The current health value
    private float lerpTimer;      // Timer used for smooth health bar transitions

    private bool canTakeDamage = true;  // Flag to control whether damage can be taken
    public float damageCooldown = 1f;   // Cooldown period for taking damage (in seconds)

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;  // Initialize health to maxHealth
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth); // Ensure health stays within valid range (0 - maxHealth)
        UpdateHealthUI(); // Update the health UI every frame

        // if (Input.GetKeyDown(KeyCode.A) && canTakeDamage){TakeDamage(Random.Range(5, 10)); // Take damage when A is pressed}if (Input.GetKeyDown(KeyCode.S)){RestoreHealth(Random.Range(5, 10)); // Restore health when S is pressed}
    }

    // Update the health UI (e.g., smooth transitions for health bar filling)
    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            health -= damage;  // Decrease health by the damage value
            lerpTimer = 0f;    // Reset the lerp timer to smoothly update UI

            // Ensure health doesn't go negative
            if (health <= 0)
            {
                Debug.Log("Player has died!");
                health = 0;
            }

            // Start damage cooldown to prevent repeated damage
            StartCoroutine(DamageCooldown());
        }
    }

    // Method to restore health
    public void RestoreHealth(float heal)
    {
        if (canTakeDamage)
        {
            health += heal;  // Decrease health by the damage value
            lerpTimer = 0f;    // Reset the lerp timer to smoothly update UI

            // Start damage cooldown to prevent repeated damage
            StartCoroutine(DamageCooldown());
        }
    }

    // Coroutine to implement damage cooldown
    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;  // Prevent further damage for the cooldown period
        yield return new WaitForSeconds(damageCooldown); // Wait for the cooldown to finish
        canTakeDamage = true;   // Allow damage again after the cooldown
    }
}
