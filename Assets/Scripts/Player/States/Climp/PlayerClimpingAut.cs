using UnityEngine;

public class PlayerClimpingAut : PlayerState
{

    protected float originalControllerHeight;
    protected Vector3 originalControllerCenter;
    public PlayerClimpingAut(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpPlayer(6.5f);
        originalControllerHeight = controller.height;
        originalControllerCenter = controller.center;



    }


    public override void UpdateState()
    {
        base.UpdateState();
        controller.Move(player.transform.forward * player.speed * 1.5f * Time.deltaTime);

        player.HandleGravity();

        if (triggerAnimationEnd)
        {
            player.JumpPlayer(0f);
            stateMachine.ChangeState(player.IdleState);
        }
                
                

        
            
    }

    public override void Exit()
    {
        base.Exit();
        controller.height = originalControllerHeight;
        controller.center = originalControllerCenter;

    }
    

    

}
