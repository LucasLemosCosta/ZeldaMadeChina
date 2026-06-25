using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }


    public override void UpdateState()
    {
        base.UpdateState();
        player.MovingPlayer(Vector2.zero, 0);


        if (input.Direction != Vector2.zero)
        {

            stateMachine.ChangeState(player.WalkState);
        }

    }

}
