using UnityEngine;

public abstract class EntityState
{

    protected string stateBoolName;
    protected StateMachine stateMachine;
    protected Animator anim;

    protected int mirror = 1;
    protected bool triggerAnimationEnd;



    public EntityState(StateMachine stateMachine, string stateBoolName)
    {
        this.stateBoolName = stateBoolName;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        anim.SetBool(stateBoolName, true);
    }

    public virtual void UpdateState()
    {

    }


    public virtual void Exit()
    {
        anim.SetBool(stateBoolName, false);
        triggerAnimationEnd = false;

    }
    public void SetAnableTriggerAnimation(bool anable) => triggerAnimationEnd = anable;





}
