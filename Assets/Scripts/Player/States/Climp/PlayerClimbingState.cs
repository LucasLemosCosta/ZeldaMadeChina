using UnityEngine;

public class PlayerClimbingState : PlayerState
{

    protected Quaternion fixRotation;
    public PlayerClimbingState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpPlayer(player.forceJumpGround * 1.6f);
        fixRotation = player.transform.rotation;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.EquipedSword(false);
        player.transform.rotation = fixRotation;

        if(input.Direction != Vector2.zero)
        {
            player.stamina.DecriseStamina(5f);
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
