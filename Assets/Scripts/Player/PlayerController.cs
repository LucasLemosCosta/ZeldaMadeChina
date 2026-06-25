using Unity.VisualScripting;
using UnityEngine;

public sealed class PlayerController : Entity
{

    //States
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }

    //Componets
    public GetInput input { get; private set; }
    public CharacterController controller { get; private set; }

    //Controllers
    private float yVelocity = 0;
    private float currentSpeed;

    [Header("Settings Moving")]
    [Range(0,1)][SerializeField] private float acel = 0.1f;


    //Events
    public override void Awake()
    {
        base.Awake();

        //Get Componests
        input = GetComponentInChildren<GetInput>();
        controller = GetComponent<CharacterController>();

        //Instatiate the states 
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        WalkState = new PlayerWalkState(this, StateMachine, "Walk");
        RunState = new PlayerRunState(this, StateMachine, "Run");
    }

    public override void Start()
    {
        base.Start();
        StateMachine.InitStateMachine(IdleState);
    }

    

    //Methods 

    public override void HandleGravity()
    {
        base.HandleGravity();

        //Decrease yVelocity
        if(yVelocity > -gravity)
        {
            yVelocity += Time.fixedDeltaTime * -gravity;
        }


        //Make player fall
        Vector3 down = new Vector3(0, yVelocity, 0);
        controller.Move(down * Time.fixedDeltaTime);
    }

    public void MovingPlayer(Vector2 direction,float speed)
    {
        //Get Direction
        Vector3 inputDirection = new Vector3(direction.x, 0f, direction.y).normalized;
        Vector3 moviment = Camera.main.transform.TransformDirection(inputDirection);
        moviment.y = 0;

        //Rotation Player
        if(inputDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moviment);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //Moving Player
        currentSpeed = Mathf.Lerp(currentSpeed, speed, acel);
        Anim.SetFloat("Speed", currentSpeed);
        controller.Move(moviment * currentSpeed * Time.deltaTime);
        

    }







}
