using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.HandleGravity();

        if (player.OnGround)
            stateMachine.ChangeState(player.IdleState);

        if (player.OnWallDown && player.OnWallUp && player.CanClimp)
            stateMachine.ChangeState(player.ClimbingState);
    }
}
