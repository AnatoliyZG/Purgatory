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

    public Action<Entity> onTriggerExit;

    public Action<Entity> onTriggerEnter;

    public Action<Entity> onTriggerStay;

    public UnitType unitType;

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
    }

    public void SetProperties(UnitProperties unitProperties)
    {
        VisionSphere.radius = unitProperties.AttackRange;

        this.unitProperties = unitProperties.Clone<UnitProperties>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<Entity>();
        if (entity!=null)
            onTriggerEnter?.Invoke(entity);
    }

    private void OnTriggerStay(Collider other)
    {
        var entity = other.GetComponent<Entity>();
        if (entity != null)
            onTriggerStay?.Invoke(entity);
    }

    private void OnTriggerExit(Collider other)
    {
        var entity = other.GetComponent<Entity>();
        if (entity != null)
            onTriggerExit?.Invoke(entity);
    }

    public enum UnitType
    { 
        Worker,
        Capitan,
        Hero,
        Monster
    }
}
