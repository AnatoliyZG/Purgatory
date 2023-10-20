using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "new sell action", menuName = "Sell Action", order = 51)]
public class Sell : EntityAction<Building>
{
    public float Percent = 0.5f;

    public Action OnSell;

    public override bool IsInterectable => true;

    public override string Description => $"Разрушить здание(Возвращает {Percent * 100}% от стоимости)";

    public override void Execute(Building building)
    { 
        ResourceController.wood += (int)(building.buildProperties.WoodCost * Percent);
        ResourceController.rock += (int)(building.buildProperties.RockCost * Percent);

        GameManager.instance.buildings.Remove(building);

        OnSell?.Invoke();

        Destroy(building.gameObject);
    }
}
