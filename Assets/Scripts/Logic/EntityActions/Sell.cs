using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : EntityAction<Building>
{
    public override void Execute(Building building)
    {
        Destroy(building.gameObject);

        ResourceController.Money += building.buildProperties.Cost * 0.5f;

        GameManager.instance.buildings.Remove(building);
    }
}
