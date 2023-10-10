using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new build properties", menuName = "Build Properties", order = 51)]
public class BuildProperties : EntityProperties
{
    public Vector2Int Size = Vector2Int.one;

    public float Cost;
}
