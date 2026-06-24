using UnityEngine;

public abstract class EntityState
{

    protected string stateBoolName;
    protected StateMachine stateMachine;
    protected Animator anim;
    
    public EntityState(StateMachine stateMachine, string stateBoolName)
    {
        this.stateBoolName = stateBoolName;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        anim.SetBool(stateBoolName, true);
        Debug.Log(stateBoolName);
    }

    public virtual void UpdateState()
    {

    }


    public virtual void Exit()
    {
        anim.SetBool(stateBoolName, false);

    }



}
