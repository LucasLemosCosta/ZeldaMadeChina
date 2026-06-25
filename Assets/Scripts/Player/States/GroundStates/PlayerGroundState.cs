using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (!player.OnGround)
            stateMachine.ChangeState(player.FallState);
    }
}
