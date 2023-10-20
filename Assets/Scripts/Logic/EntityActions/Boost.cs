using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new boost action", menuName = "Boost Action", order = 51)]
public class Boost : EntityAction<Building>
{
    public override bool IsInterectable => true;

    public override string Description => $"Увеличивает хп на {BoostHp} за {CostWood} дерева и {CostRock} камня";

    public float BoostHp;

    public int CostWood;

    public int CostRock;

    public override void Execute(Building obj)
    {
        obj.buildProperties.Hp += BoostHp;
        ResourceController.rock -= CostRock;
        ResourceController.wood -= CostWood;
    }
}
