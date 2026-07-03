using UnityEngine;

public class PlayerJumpIdleState : PlayerGroundState
{
    public PlayerJumpIdleState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpPlayer(player.jump);
    }
}
