using UnityEngine;

public class EntityAnimation : MonoBehaviour
{

    private Entity entity;

    public virtual void Awake()
    {
        entity = GetComponent<Entity>();
    }
    public void AnimationEnd()
    {
        entity.SetAnableTriggerAnimation(true);
    }

}
