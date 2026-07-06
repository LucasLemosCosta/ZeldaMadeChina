using UnityEngine;
using UnityEngine.InputSystem;

public class GetInput : MonoBehaviour
{
    public Vector2 Direction { get; protected set; }
    public bool OnRun { get; protected set; }
    public bool OnJump { get; protected set; }



    public void InputDirection(InputAction.CallbackContext ctx) => Direction = ctx.ReadValue<Vector2>().normalized;
    public void InputRun(InputAction.CallbackContext ctx) => OnRun = ctx.performed;
    public void InputJump(InputAction.CallbackContext ctx) => OnJump = ctx.performed;
}
