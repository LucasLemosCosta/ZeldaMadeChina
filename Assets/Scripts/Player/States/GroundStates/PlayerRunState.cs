using UnityEngine;

public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        player.MovingPlayer(input.Direction, player.speed * 2f);
        if (!input.OnRun || input.Direction == Vector2.zero)
            stateMachine.ChangeState(player.IdleState);
        if (player.OnWallDown && player.OnWallUp && player.CanClimp)
            stateMachine.ChangeState(player.ClimbingState);


    }
}
