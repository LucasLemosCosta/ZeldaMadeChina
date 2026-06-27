using UnityEngine;

public class PlayerClimbingState : PlayerState
{
    public PlayerClimbingState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpPlayer(player.jump * 1.6f);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(player.yVelocity > 0)
            player.HandleGravity();
        player.PlayerWallMoving(input.Direction, player.climbingSpeed);
        if (input.OnRun || !player.OnWallUp || !player.OnWallDown|| (player.OnGround && player.yVelocity <= 0))
        {
            player.JumpPlayer(0);
            stateMachine.ChangeState(player.FallState);
        }
    }


    public override void Exit()
    {
        base.Exit();
        player.EnableCanClimbing(false);

    }
}
