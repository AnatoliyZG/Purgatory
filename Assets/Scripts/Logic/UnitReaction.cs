using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Entity;

public class UnitReaction : MonoBehaviour
{
    public AttackState attackState;

    public Unit reactor;

    private List<Entity> victims = new();

    private Vector3 initialPosition;

    private void Start()
    {
        if (attackState == AttackState.Holding && reactor.type == EntityType.Ally)
            reactor.fighting.onGetHit += reactor.inputController.StartPath;

        if (reactor.type == EntityType.Monster)
        {
            reactor.inputController.StartPath(GameManager.instance.Base);
        }

        reactor.onTriggerEnter += UnitComeIn;
        reactor.onTriggerExit += UnitLeave;
        reactor.onTriggerStay += UnitStayIn;
    }
        
    public void UnitComeIn(Entity entity)
    {
        if (reactor.type == EntityType.Ally && entity.type == EntityType.Monster && attackState == AttackState.Agressive)
        {
            if (victims.Count > 0)
            {
                victims.Add(entity);
            }
            else
            {
                victims.Add(entity);

                initialPosition = reactor.transform.position;

                reactor.inputController.StartPath(victims[0]);
            }
        }
        else if (reactor.type == EntityType.Monster && entity.type == EntityType.Ally)
        {
            if (victims.Count == 0)
                reactor.inputController.StartPath(entity);

            victims.Add(entity);
        }
    }

    public void UnitStayIn(Entity entity)
    { 
        if (reactor.type == EntityType.Ally && attackState == AttackState.Passive && Vector3.Distance(entity.transform.position, transform.position) <= reactor.unitProperties.AttackRange)
        {
            reactor.fighting.BeginAttacking(entity);
        }
    }

    public void UnitLeave(Entity entity) 
    {
        if (entity == victims[0] && victims.Count > 1) 
        {

        }

        victims.Remove(entity);
    }

    public enum AttackState
    {
        Agressive,
        Passive,
        Holding,
        None
    }
}
