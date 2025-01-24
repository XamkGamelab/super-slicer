using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction movementAction;
    InputAction dashAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementAction = InputSystem.actions.FindAction("Movement");
        dashAction = InputSystem.actions.FindAction("Dash");
    }

    // Update is called once per frame
    void Update()
    {
        if (dashAction.WasPressedThisFrame())
        {
            Debug.Log($"Action: {dashAction.name} called");
        }
        
        if (movementAction.WasPressedThisFrame())
        {
            Debug.Log($"Action: {movementAction.name} called");
        }
    }
}
