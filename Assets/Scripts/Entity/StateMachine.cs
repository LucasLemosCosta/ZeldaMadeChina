using UnityEngine;

public class StateMachine
{

    private bool canChangeState = true;
    private EntityState currentState;


    public void InitStateMachine(EntityState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(EntityState state)
    {
        if (!canChangeState) return;

        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

}
