using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void UpdateState()
    {
        player.HandleGravity();
        base.UpdateState();
        if (input.OnJump && player.OnGround && stateMachine.CurrentState != player.RunningJumpState && input.Direction != Vector2.zero)
            stateMachine.ChangeState(player.RunningJumpState);
        if (input.OnJump && player.OnGround && stateMachine.CurrentState != player.RunningJumpState && input.Direction == Vector2.zero)
            stateMachine.ChangeState(player.IdleJumpState);
        if (!player.OnGround && player.yVelocity < 0f)
            stateMachine.ChangeState(player.FallState);

        if(player.OnGround && input.OnAttack && player.SwordEquiped)
        {
            stateMachine.ChangeState(player.AttackGround);
        }



    }
}
