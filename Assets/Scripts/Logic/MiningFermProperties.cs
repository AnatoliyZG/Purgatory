using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new minig propertries", menuName = "Minig Ferm Properties", order = 51)]
public class MiningFermProperties : BuildProperties
{
    public int Efficiency;

    public ResourcesType resourcesType;

    public float MiningTime;

    public float Radius;
}
public enum ResourcesType
{
    Wood,
    Rock,
}