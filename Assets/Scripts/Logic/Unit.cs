using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Seeker))]

public class Unit : Entity
{
    public override EntityProperties properties => unitProperties;

    public UnitProperties unitProperties;

    public InputController inputController;

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
}
