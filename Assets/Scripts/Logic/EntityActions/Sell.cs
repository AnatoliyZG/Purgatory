using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Sell action", menuName = "Actions/Sell", order = 51)]
public class Sell : EntityAction<Building>
{
    public float Percent = 0.5f;

    public override bool IsInterectable => true;

    public override string Description => $"Разрушить здание(Возвращает {Percent * 100}% от стоимости)";

    public override void Execute(Building building)
    { 
        ResourceController.wood += (int)(building.buildProperties.WoodCost * Percent);
        ResourceController.rock += (int)(building.buildProperties.RockCost * Percent);

        Destroy(building.gameObject);
    }
}
