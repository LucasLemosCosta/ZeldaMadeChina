using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        player.MovingPlayer(input.Direction, player.speed * 2f);
        if (!input.Run)
            stateMachine.ChangeState(player.WalkState);
        if (input.Direction == Vector2.zero)
            stateMachine.ChangeState(player.IdleState);
    }
}
