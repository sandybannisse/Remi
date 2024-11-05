using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook look;

   void Awake() 
   {
    playerInput = new PlayerInput();
    onFoot = playerInput.OnFoot;
    motor = GetComponent<PlayerMotor>();
    onFoot.Jump.performed += ctx => motor.Jump();
    look = GetComponent<PlayerLook>();

   }

   void FixedUpdate()
   {
    motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
   }

   void LateUpdate()
   {
    look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
   }

    void Update() 
    {

    }

    private void OnEnable()
    {
        onFoot.Enable();

    }

    private void OnDisable()
    {
        onFoot.Disable();

    }

}