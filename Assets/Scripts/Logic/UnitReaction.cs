using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitReaction : MonoBehaviour
{
    public AttackState attackState;

    protected Unit reactor;

    protected List<Entity> victims = new();

    protected virtual void Start()
    {
        reactor = GetComponent<Unit>();

        reactor.onTriggerEnter += UnitComeIn;
        reactor.onTriggerExit += UnitLeave;
    }
        
    public virtual void UnitComeIn(Entity entity)
    {
        if(reactor.type != entity.type)
        {
            victims.Add(entity);
        }
    }

    public virtual void UnitLeave(Entity entity) 
    {
        if (victims.Contains(entity))
        {
            victims.Remove(entity);
        }
    }

    public enum AttackState
    {
        Agressive,
        Passive,
        Holding,
        None
    }
}
