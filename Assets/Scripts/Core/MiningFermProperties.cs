using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourcesPlaceProperties;

[CreateAssetMenu(fileName = "new minig propertries", menuName = "Minig Ferm Properties", order = 51)]
public class MiningFermProperties : BuildProperties
{
    public float Efficiency;

    public ResourcesType resourcesType;

    public float MiningTime;

    public float Radius;
}
