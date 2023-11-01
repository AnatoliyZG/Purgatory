using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[RequireComponent(typeof(Seeker))]

public class Unit : Entity
{
    public override EntityProperties properties => unitProperties;

    public UnitProperties unitProperties;

    public InputController inputController;

    //public Action onUnitReaction;

    private Vector3 initialPosition;

    public UnitType unitType;

    public AttackState attackState;

    private List<Unit> victims = new();

    public override void Start()
    {
        base.Start();

        SetProperties(unitProperties);

        if (type == EntityType.Ally || type == EntityType.Monster) 
        {
            inputController = new PlayerInput(this);
        }
        else
        {
            inputController = new BotInput(this);
        }

        if (attackState == AttackState.Holding && type == EntityType.Ally) 
            fighting.onGetHit += inputController.StartPath;
    }

    public void SetProperties(UnitProperties unitProperties)
    {
        VisionSphere.radius = unitProperties.AttackRange;

        this.unitProperties = unitProperties.Clone<UnitProperties>();
    }

    public void UnitComeIn(Unit unit)
    {
        if (type == EntityType.Ally && unit.type == EntityType.Monster && attackState == AttackState.Agressive) 
        {
            initialPosition = transform.position;

            victims.Add(unit);

            inputController.StartPath(victims[0]);
        }
        else if(type == EntityType.Monster && unit.type == EntityType.Ally)
        {
            if (victims.Count == 0)
                inputController.StartPath(unit);

            victims.Add(unit);
        }
    }

    public void UnitStayIn(Unit unit)
    {
        if(type == EntityType.Ally && attackState == AttackState.Passive && Vector3.Distance(unit.transform.position, transform.position) <= unitProperties.AttackRange) 
        {
            fighting.BeginAttacking(unit);
        }
    }

    public void UnitLeave(Unit unit)
    {
        victims.Remove(unit);

        if (unit != victims[0]) { }
        else
        {
            if (victims.Count == 0 && type == EntityType.Monster)
                inputController.StartPath(GameManager.instance.Base);
            else if (victims.Count == 0 && type == EntityType.Ally)
                inputController.StartPath(initialPosition);
            else
                inputController.StartPath(victims[0]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 || other.gameObject.layer == 7)
            UnitComeIn(other.GetComponent<Unit>());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11 || other.gameObject.layer == 7)
            UnitStayIn(other.GetComponent<Unit>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11 || other.gameObject.layer == 7)
            UnitLeave(other.GetComponent<Unit>());
    }

    public enum UnitType
    { 
        Worker,
        Capitan,
        Hero,
        Monster
    }

    public enum AttackState
    {
        Agressive,
        Passive,
        Holding,
        None
    }
}
