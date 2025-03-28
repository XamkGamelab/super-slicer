using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    InputAction movementAction;
    InputAction dashAction;
    [SerializeField] PlayerController playerController;
    [SerializeField] DynamicJoystick joystick;
    [SerializeField] Transform joystickBackground;
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

        if (joystick.Direction != Vector2.zero)
        {
            playerController.movementEvent.Invoke(new Vector2(joystick.Horizontal, joystick.Vertical));
        }

        //Debug.Log(joystickBackground.localPosition);
        joystickBackground.localPosition = new Vector2(Mathf.Clamp(joystickBackground.position.x, 0, 1000), joystickBackground.position.y);
    }

    public void DashButton()
    {
        playerController.dashEvent.Invoke();
    }
}
