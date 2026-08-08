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
        player.stamina.IncreaseStamina();




    }

    protected override void ChangeState()
    {
        base.ChangeState();
        if (input.Direction != Vector2.zero && player.OnGround)
        {
            stateMachine.ChangeState(player.WalkState);
        }
    }

}
