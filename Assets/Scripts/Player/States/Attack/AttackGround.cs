using UnityEngine;
using System;

public class AttackGround : PlayerState
{
    private int attackCombo;
    private bool oneMoreAttack;
    public AttackGround(PlayerController player, StateMachine stateMachine, string stateBoolName) : base(player, stateMachine, stateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();


        anim.SetInteger("AttackCombo", attackCombo);



        input.ConsumeAttack();

        Debug.Log(attackCombo);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        anim.SetLayerWeight(1, 0);
        controller.Move(player.transform.forward.normalized * 0.8f * Time.deltaTime);

        if(input.OnAttack && !oneMoreAttack)
        {
            oneMoreAttack = true;

        }

        if (triggerAnimationEnd)
        {
            if (attackCombo >= 1)
            {
                attackCombo = 0;
                stateMachine.ChangeState(player.IdleState);
            }

            if (oneMoreAttack && attackCombo != 1)
            {
                attackCombo++;
                stateMachine.ChangeState(player.AttackGround);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
        anim.SetLayerWeight(1, 1);
        oneMoreAttack = false;


    }
}
