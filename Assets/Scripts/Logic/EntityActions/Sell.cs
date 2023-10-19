using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new sell action", menuName = "Sell Action", order = 51)]
public class Sell : EntityAction<Building>
{
    public float Percent = 0.5f;

    public override void Execute(Building building)
    { 
        ResourceController.wood += (int)(building.buildProperties.WoodCost * Percent);
        ResourceController.rock += (int)(building.buildProperties.RockCost * Percent);

        GameManager.instance.buildings.Remove(building);

        Destroy(building.gameObject);
    }
}
