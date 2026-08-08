using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();


    }
    public override void UpdateState()
    {
        base.UpdateState();
        
        player.MovingPlayer(input.Direction, player.speedMovingGroundNormal * 0.6f);



    }


}
