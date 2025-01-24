using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    InputAction movementAction;
    InputAction dashAction;
    [SerializeField] PlayerController playerController;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        movementAction = InputSystem.actions.FindAction("Movement");
        dashAction = InputSystem.actions.FindAction("Dash");
    }

    // Update is called once per frame
    void Update()
    {
        if (dashAction.WasPressedThisFrame())
        {
            playerController.dashEvent.Invoke();
        }

        if (movementAction.IsPressed())
        {
            Vector2 movementValue = movementAction.ReadValue<Vector2>();
            playerController.movementEvent.Invoke(movementValue);
        }
    }
}
