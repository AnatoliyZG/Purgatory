using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new build properties", menuName = "Build Properties", order = 51)]
public class BuildProperties : EntityProperties
{
    public int Size = 1;

    public uint BuildLevel = 1;

    public List<EntityAction<Building>> entityActions = new();

    public BuildProperties NextLevel;

    public int WoodCost;

    public int RockCost;

    public int HealTime;

    public int HealScale;
}
