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

        if(input.Direction != Vector2.zero)
        {
            player.stamina.DecriseStamina();
        }

        if (player.yVelocity > 0)
            player.HandleGravity();
        player.PlayerWallMoving(input.Direction, player.climbingSpeed);
        if (input.OnRun || !player.OnWallUp || !player.OnWallDown
            || (player.OnGround && player.yVelocity <= 0) 
            || player.stamina.currentStamina <= 0.1f || !player.stamina.canSpandStamina
            
            )
        {
            player.JumpPlayer(0);
            stateMachine.ChangeState(player.FallState);
        }

        if (player.OnWallDown && !player.OnWallUp)
            stateMachine.ChangeState(player.ClimpingAutState);
    }


    public override void Exit()
    {
        base.Exit();
        player.EnableCanClimbing(false);

    }
}
