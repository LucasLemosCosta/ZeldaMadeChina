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
    }


    public virtual void HandleGravity()
    {

    }
}
