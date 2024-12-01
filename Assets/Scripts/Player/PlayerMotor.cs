using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playervelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    public float playerHeight = 2f;

    public AK.Wwise.Event JumpSoundEvent;
    public AK.Wwise.Event StepSoundEvent;

    private bool canPlayStepSound = true; // Step sound cooldown control
    public float stepSoundCooldown = 0.5f; // Time in seconds between step sounds
    private bool isBoostingSpeed = false; // To prevent multiple speed boosts

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        // Check for U key press and trigger speed boost
        if (Keyboard.current.uKey.wasPressedThisFrame && !isBoostingSpeed)
        {
            StartCoroutine(SpeedBoost());
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y; // This was likely your intention, since player movement is typically in the x-z plane
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playervelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playervelocity.y < 0)
            playervelocity.y = -2f;
        controller.Move(playervelocity * Time.deltaTime);

        // Play step sound if moving and cooldown allows
        if (canPlayStepSound && (input.x != 0 || input.y != 0))
        {
            StepSoundEvent.Post(gameObject);
            StartCoroutine(ResetStepSoundCooldown());
        }
    }

    public void Jump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 2f))
        {
            playervelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity / 4);
        }

        JumpSoundEvent.Post(gameObject);

        Debug.Log("JUMPING!!");
    }

    // Coroutine to handle step sound cooldown
    private IEnumerator ResetStepSoundCooldown()
    {
        canPlayStepSound = false;
        yield return new WaitForSeconds(stepSoundCooldown);
        canPlayStepSound = true;
    }

    // Coroutine to handle speed boost
    private IEnumerator SpeedBoost()
    {
        isBoostingSpeed = true; // Prevent multiple boosts
        float originalSpeed = speed;
        speed = 20f; // Set speed to 20
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        speed = originalSpeed; // Reset speed back to original
        isBoostingSpeed = false; // Allow for future boosts
    }
}
