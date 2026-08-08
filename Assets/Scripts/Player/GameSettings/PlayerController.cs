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
    public AttackGround AttackGround { get; private set; }


    //Componets
    public GetInput input { get; private set; }
    public CharacterController controller { get; private set; }
    public Stamina stamina { get; private set; }



    //Equipement
    public bool SwordEquiped;

    [Header("Settings Moving")]
    [Range(0, 1)][SerializeField] private float acelOfMovingGroundSpeed = 0.1f;
    public float climbingSpeed = 3f;


    [Header("Climping Settings")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float timeToCanClimb;
    [SerializeField] private float rayWallDistance;
    [SerializeField] private float sizeRayWallSphere;
    [SerializeField] private Transform targetSourceWallUp;
    [SerializeField] private Transform targetSourceWallDown;

    [Header("Sword")]
    [SerializeField] private GameObject equippedSword;
    [SerializeField] private GameObject unequipSword;

    [Header("Shild")]
    [SerializeField] private GameObject equippedShild;
    [SerializeField] private GameObject unequipShild;

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
        AttackGround = new AttackGround(this, StateMachine, "Attack");
    }

    public override void Start()
    {
        base.Start();
        StateMachine.InitStateMachine(IdleState);
    }
    public override void Update()
    {
        base.Update();

        HandleClimb();
        HandleEquipedWeapon();


    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(targetSourceWallUp.position + transform.forward * rayWallDistance, sizeRayWallSphere);
        Gizmos.DrawWireSphere(targetSourceWallDown.position + transform.forward * rayWallDistance, sizeRayWallSphere);

    }


    //Methods 

    public void EquipedSword(bool anable)
    {
        if (anable == false)
        {
            SwordEquiped = anable;
        }

        if(anable)
            Anim.SetLayerWeight(1, 1);
        else
            Anim.SetLayerWeight(1, 0);

        if(anable && !SwordEquiped)
        {
            Anim.SetTrigger("Equiped");

        }
    }


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
        OnWallUp = Physics.SphereCast(targetSourceWallUp.position, sizeRayWallSphere, transform.forward, out wallHitUp, rayWallDistance, whatIsWall);
        OnWallDown = Physics.SphereCast(targetSourceWallDown.position, sizeRayWallSphere, transform.forward, out wallHitDown, rayWallDistance, whatIsWall);


    }
    public void MovingPlayer(Vector2 direction, float speed)
    {
        //Get Direction
        Vector3 inputDirection = new Vector3(direction.x, 0f, direction.y).normalized;
        Vector3 filterDirectionWithCamera = Camera.main.transform.TransformDirection(inputDirection);
        filterDirectionWithCamera.y = 0;


        if (inputDirection != Vector3.zero)
        {
            //Rotation Player
            Quaternion targetFolowRotation = Quaternion.LookRotation(filterDirectionWithCamera);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetFolowRotation, rotationBodySpeed * Time.deltaTime);

        }

        //Acelerate
        currentSpeed = Mathf.Lerp(currentSpeed, speed, acelOfMovingGroundSpeed);
        Anim.SetFloat("Speed", currentSpeed);
        //Moving Player
        controller.Move(filterDirectionWithCamera * currentSpeed * Time.deltaTime);

    }

    public void PlayerWallMoving(Vector2 direction, float climSpeed)
    {

        int mirror = -1;

        Vector3 wallDirectionNormal = Vector3.Cross(Vector3.up, wallHitDown.normal).normalized;

        Vector3 movingX = wallDirectionNormal * direction.x * mirror;
        Vector3 movingY = Vector3.up * direction.y;
  

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


        controller.Move((movingX + movingY) * currentClimpSpeed * Time.deltaTime);

    }




    public void EnableCanClimbing(bool enable) => CanClimp = enable;

    public void JumpPlayer(float forceJump)
    {
        yVelocity = forceJump;
        jumpMirror *= -1;
        Anim.SetFloat("JumpMirror", jumpMirror);
    }


    private void HandleClimb()
    {

        if (!CanClimp)
        {
            timerToCanClimp += Time.deltaTime;
            if (timerToCanClimp >= timeToCanClimb)
            {
                timerToCanClimp = 0;
                CanClimp = true;
            }
        }

    }

    private void HandleEquipedWeapon()
    {
        //test sword
        equippedSword.SetActive(SwordEquiped);
        unequipSword.SetActive(!SwordEquiped);
        equippedShild.SetActive(SwordEquiped);
        unequipShild.SetActive(!SwordEquiped);
    }





}
