using UnityEngine;

public class PlayerJumpRunningState : PlayerGroundState
{

    public PlayerJumpRunningState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpPlayer(player.jump);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.stamina.ShowStamina();
        player.MovingPlayer(input.Direction, player.speed * 2);
        if (player.OnWallDown)
            stateMachine.ChangeState(player.ClimbingState);


    }

    
}
