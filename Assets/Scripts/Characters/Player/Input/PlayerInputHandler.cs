using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool IsHoldingSprint { get; private set; }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
        IsHoldingSprint = context.ReadValueAsButton();
    }
}
