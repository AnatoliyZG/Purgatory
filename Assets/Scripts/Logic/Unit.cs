using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnitProperties;
using static UnityEngine.UI.CanvasScaler;

[RequireComponent(typeof(Seeker))]

public class Unit : Entity
{
    public override EntityProperties properties => unitProperties;

    public UnitProperties unitProperties;

    public InputController inputController;

    public override void Start()
    {
        base.Start();

        unitProperties = unitProperties.Clone<UnitProperties>();

        if(type == EntityType.Ally)
        {
            inputController = new PlayerInput(this);
        }
        else
        {
            inputController = new BotInput(this);
        }

        if (type == EntityType.Ally && unitProperties.Type != UnitType.Worker)
        {
            GroupController.Add(this);
        }
    }
}
