using UnityEngine;

public sealed class StateMachine
{

    private bool canChangeState = true;
    public EntityState CurrentState { get; private set; }


    public void InitStateMachine(EntityState state)
    {
        CurrentState = state;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState state)
    {
        if (!canChangeState) return;

        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }

}
