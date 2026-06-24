using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [Header("Settings Moving")]
    public float speed;
    public float rotationSpeed;


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

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
