using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new build properties", menuName = "Build Properties", order = 51)]
public class BuildProperties : EntityProperties
{
    public int SizeX = 1;

    public int SizeY = 1;

    public int SizeZ = 1;

    public float WoodCost;

    public float RockCost;
}
