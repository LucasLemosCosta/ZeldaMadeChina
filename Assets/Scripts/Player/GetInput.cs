using UnityEngine;
using UnityEngine.InputSystem;

public class GetInput : MonoBehaviour
{
    public Vector2 Direction { get; protected set; }



    public void InputDirection(InputAction.CallbackContext ctx) => Direction = ctx.ReadValue<Vector2>();
}
