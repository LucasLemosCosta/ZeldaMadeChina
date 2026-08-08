using UnityEngine;

public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        //Settings
        player.EquipedSword(false);
        player.stamina.DecriseStamina(15f);

        //Moving
        player.MovingPlayer(input.Direction, player.speedMovingGroundRun);


        ChangeState();

    }

    protected override void ChangeState()
    {
        base.ChangeState();
        if (!input.OnRun || input.Direction == Vector2.zero)
            stateMachine.ChangeState(player.IdleState);

        if (player.stamina.currentStamina <= 0.1f || !player.stamina.canSpandStamina)
            stateMachine.ChangeState(player.IdleState);

        if (player.OnWallDown && player.OnWallUp && player.CanClimp)
            stateMachine.ChangeState(player.ClimbingState);
    }
}
