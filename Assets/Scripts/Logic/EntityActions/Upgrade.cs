using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade action", menuName = "Actions/Upgrade action", order = 52)]
public class Upgrade : EntityAction<Building>
{
    public override bool IsInterectable => true;

    public override string Description => $"Улучшает здание";


    public override void Execute(Building obj)
    {
        /*
        obj.buildProperties.Hp += BoostHp;
        ResourceController.rock -= CostRock;
        ResourceController.wood -= CostWood;
        */
    }
}
