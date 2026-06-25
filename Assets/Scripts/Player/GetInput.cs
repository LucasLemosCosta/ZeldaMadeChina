using UnityEngine;
using UnityEngine.InputSystem;

public class GetInput : MonoBehaviour
{
    public Vector2 Direction { get; protected set; }
    public bool Run { get; protected set; }



    public void InputDirection(InputAction.CallbackContext ctx) => Direction = ctx.ReadValue<Vector2>().normalized;
    public void InputRun(InputAction.CallbackContext ctx) => Run = ctx.performed;
}
