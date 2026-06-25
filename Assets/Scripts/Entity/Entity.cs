using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [Header("Settings Moving")]
    public float speed = 5f;
    public float rotationSpeed = 7f;
    public float gravity = 9.7f;


    //Components
    public Animator Anim { get; protected set; }
    public StateMachine StateMachine { get; protected set; }


    //Controllers
    [Header("Collider Ground Settings")]
    [SerializeField] private float sizeRaySphereGround = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform targetGround;
    public bool OnGround { get; protected set; }

    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        StateMachine = new StateMachine();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        HandleGravity();
        HandleCollider();
    }

    public virtual void OnDrawGizmos()
    {
        if (targetGround == null) targetGround = transform;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetGround.position, sizeRaySphereGround);
    }


    public virtual void HandleGravity()
    {

    }

    public virtual void HandleCollider()
    {
        CheckGround();
    }


    private void CheckGround()
    {
        OnGround = Physics.CheckSphere(targetGround.position, sizeRaySphereGround, whatIsGround);
    }


}
