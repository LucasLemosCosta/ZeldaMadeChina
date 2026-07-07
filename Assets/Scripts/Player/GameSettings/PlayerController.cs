using Unity.VisualScripting;
using UnityEngine;

public sealed class PlayerController : Entity
{

    //States
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerJumpRunningState RunningJumpState { get; private set; }
    public PlayerJumpIdleState IdleJumpState { get; private set; }
    public PlayerClimbingState ClimbingState { get; private set; }
    public PlayerClimpingAut ClimpingAutState { get; private set; }


    //Componets
    public GetInput input { get; private set; }
    public CharacterController controller { get; private set; }
    public Stamina stamina { get; private set; }



    //Equipement
    public bool SwordEquiped { get; private set; }


    [Header("Settings Moving")]
    [Range(0, 1)][SerializeField] private float acel = 0.1f;
    public float climbingSpeed = 3f;


    [Header("Climping Settings")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float timeToCanClimb;
    [SerializeField] private float sizeRayWallDistance;
    [SerializeField] private float sizeRayWallSphere;
    [SerializeField] private Transform targetWallUp;
    [SerializeField] private Transform targetWallDown;

    [Header("Sword")]
    [SerializeField] private GameObject equippedSword;
    [SerializeField] private GameObject unequipSword;

    //Controllers

    public float yVelocity { get; private set; } = 0f;
    private float currentSpeed;
    private float currentClimpSpeed;
    private float jumpMirror = 1;
    private float lastDirectionY = 1f;
    private float lastDirectionX = 1f;


    //Wall Settings
    public bool OnWallUp { get; private set; }
    public bool OnWallDown { get; private set; }

    private RaycastHit wallHitUp;
    private RaycastHit wallHitDown;

    public bool CanClimp { get; private set; } = true;


    //Timers
    private float timerToCanClimp;


    //Events
    public override void Awake()
    {
        base.Awake();
        yVelocity = -gravity;
        //Get Componests
        stamina = GameObject.FindFirstObjectByType<Stamina>().GetComponent<Stamina>();
        input = GetComponentInChildren<GetInput>();
        controller = GetComponent<CharacterController>();

        //Instatiate the states 
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        WalkState = new PlayerWalkState(this, StateMachine, "Walk");
        RunState = new PlayerRunState(this, StateMachine, "Run");
        FallState = new PlayerFallState(this, StateMachine, "Fall");
        RunningJumpState = new PlayerJumpRunningState(this, StateMachine, "RunningJump");
        IdleJumpState = new PlayerJumpIdleState(this, StateMachine, "IdleJump");
        ClimbingState = new PlayerClimbingState(this, StateMachine, "Climbing");
        ClimpingAutState = new PlayerClimpingAut(this, StateMachine, "ClimbingAut");
    }

    public override void Start()
    {
        base.Start();
        StateMachine.InitStateMachine(IdleState);
    }
    public override void Update()
    {
        base.Update();


        if (!CanClimp)
        {
            timerToCanClimp += Time.deltaTime;
            if (timerToCanClimp >= timeToCanClimb)
            {
                timerToCanClimp = 0;
                CanClimp = true;
            }
        }


        //test sword
        equippedSword.SetActive(SwordEquiped);
        unequipSword.SetActive(!SwordEquiped);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(targetWallUp.position + transform.forward * sizeRayWallDistance, sizeRayWallSphere);
        Gizmos.DrawWireSphere(targetWallDown.position + transform.forward * sizeRayWallDistance, sizeRayWallSphere);

    }


    //Methods 

    public override void HandleGravity()
    {
        base.HandleGravity();

        //Decrease yVelocity
        if (yVelocity > -gravity)
        {
            yVelocity += Time.deltaTime * -gravity;
        }

        //Make player fall
        Vector3 down = new Vector3(0, yVelocity, 0);
        controller.Move(down * Time.deltaTime);
    }


    public override void HandleCollider()
    {
        base.HandleCollider();
        OnWallUp = Physics.SphereCast(targetWallUp.position, sizeRayWallSphere, transform.forward, out wallHitUp, sizeRayWallDistance, whatIsWall);
        OnWallDown = Physics.SphereCast(targetWallDown.position, sizeRayWallSphere, transform.forward, out wallHitDown, sizeRayWallDistance, whatIsWall);


    }
    public void MovingPlayer(Vector2 direction, float speed)
    {
        //Get Direction
        Vector3 inputDirection = new Vector3(direction.x, 0f, direction.y).normalized;
        Vector3 moviment = Camera.main.transform.TransformDirection(inputDirection);
        moviment.y = 0;


        if (inputDirection != Vector3.zero)
        {
            //Rotation Player
            Quaternion targetRotation = Quaternion.LookRotation(moviment);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }

        //Acelerate
        currentSpeed = Mathf.Lerp(currentSpeed, speed, acel);
        Anim.SetFloat("Speed", currentSpeed);
        //Moving Player
        controller.Move(moviment * currentSpeed * Time.deltaTime);

    }

    public void PlayerWallMoving(Vector2 direction, float climSpeed)
    {

        //Get Directions 

        Vector3 inputDirectionX = Vector2.zero;
        Vector3 inputDirectionY = new Vector3(0f, direction.y, 0f).normalized;

        if (transform.forward.x >= 0.8f)
        {
            inputDirectionX = new Vector3(0f, 0f, direction.x).normalized;

        }
        else if (transform.forward.z >= 0.8f)
        {
            inputDirectionX = new Vector3(direction.x, 0f, 0f).normalized;

        }

        Vector3 movingY = inputDirectionY;
        Vector3 movingX = inputDirectionX;


        if (movingX.magnitude == 0 && movingY.magnitude == 0)
        {
            currentClimpSpeed = Mathf.Lerp(currentClimpSpeed, 0f, 0.01f);
            Anim.SetFloat("MoveY", currentClimpSpeed * lastDirectionY);
            Anim.SetFloat("MoveX", currentClimpSpeed * lastDirectionX);

        }
        else
        {

            currentClimpSpeed = Mathf.Lerp(currentClimpSpeed, climSpeed, 0.01f);

            if (movingY.magnitude >= 1)
            {
                Anim.SetFloat("MoveY", direction.y * currentClimpSpeed);
                lastDirectionY = direction.y;
                lastDirectionX = 0;

            }
            
            


            if (movingX.magnitude >= 1)
            {
                Anim.SetFloat("MoveX", direction.x * currentClimpSpeed);
                lastDirectionX = direction.x;
                lastDirectionY = 0;
            }
        }

        controller.Move(movingY * Time.deltaTime * currentClimpSpeed);
        controller.Move(movingX * Time.deltaTime * currentClimpSpeed);

    }




    public void EnableCanClimbing(bool enable) => CanClimp = enable;

    public void JumpPlayer(float forceJump)
    {
        yVelocity = forceJump;
        jumpMirror *= -1;
        Anim.SetFloat("JumpMirror", jumpMirror);
    }







}
