using UnityEngine;

public class PlayerState : EntityState
{

    protected PlayerController player;
    protected GetInput input;
    public PlayerState(PlayerController player, StateMachine stateMachine,string stateBoolName ) : base(stateMachine,stateBoolName)
    {
        this.player = player;
        anim = player.Anim;
        input = player.input;
    }

    
}
