using UnityEngine;

public class PlayerState : EntityState
{

    protected PlayerController player;
    protected GetInput input;
    protected CharacterController controller;
    public PlayerState(PlayerController player, StateMachine stateMachine,string stateBoolName ) : base(stateMachine,stateBoolName)
    {
        this.player = player;
        anim = player.Anim;
        input = player.input;
        controller = player.controller;

    }


    public override void Enter()
    {
        base.Enter();

    }





    
}
