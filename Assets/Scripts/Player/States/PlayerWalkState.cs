using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        player.MovingPlayer(input.Direction, player.speed);
        if (input.Run)
            stateMachine.ChangeState(player.RunState);
        if (input.Direction == Vector2.zero)
            stateMachine.ChangeState(player.IdleState);
    }
}
