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
        player.JumpPlayer(player.jump * 2f);
        originalControllerHeight = controller.height;
        originalControllerCenter = controller.center;
        controller.height *= 0.1f;
        controller.center = new Vector3(0f, 3f,0f);
    }


    public override void UpdateState()
    {
        base.UpdateState();
        controller.Move(player.transform.forward * player.climbingSpeed * 1.3f * Time.deltaTime);

        if (triggerAnimationEnd)
        {
            player.JumpPlayer(6f);
            stateMachine.ChangeState(player.FallState);
        }
                
                

        
            
    }

    public override void Exit()
    {
        base.Exit();
        controller.height = originalControllerHeight;
        controller.center = originalControllerCenter;

    }


    

}
