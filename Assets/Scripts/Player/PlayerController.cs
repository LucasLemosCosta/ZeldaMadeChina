using UnityEngine;

public class PlayerController : Entity
{

    //States
    public PlayerIdleState IdleState { get; protected set; }

    //Componets
    public GetInput input { get; protected set; }
    public CharacterController controller;

    public override void Awake()
    {
        base.Awake();
        input = GetComponent<GetInput>();
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
    }

    public override void Start()
    {
        base.Start();
        StateMachine.InitStateMachine(IdleState);
    }


}
