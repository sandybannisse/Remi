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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
     void Update()
    {
        // This method call should be removed as its logic is moved to ProcessMove
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y; // This was likely your intention, since player movement is typically in the x-z plane
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playervelocity.y += gravity * Time.deltaTime;
        if(isGrounded  && playervelocity.y < 0)
            playervelocity.y = -2f;
        controller.Move(playervelocity * Time.deltaTime);
        //Debug.Log(playervelocity.y); //just to view the player velocity
    }
    public void Jump()
    {
        RaycastHit hit;
        //if(isGrounded)
        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 2f)) 
        {
            playervelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity/4);
        }
    }
}
