using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    InputAction movementAction;
    InputAction dashAction;
    [SerializeField] PlayerController playerController;
    private GameManager gameManager;

    
    void Start()
    {
        gameManager = GameManager.Instance;
        movementAction = InputSystem.actions.FindAction("Movement");
        dashAction = InputSystem.actions.FindAction("Dash");
    }

    void Update()
    {
        if (dashAction.WasPressedThisFrame())
        {
            playerController.dashEvent.Invoke();
        }

        if (movementAction.IsPressed() && !playerController.dashing)
        {
            Vector2 movementValue = movementAction.ReadValue<Vector2>();
            playerController.movementEvent.Invoke(movementValue);
        }
    }
}
