using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new resource properties", menuName = "Resource Properties", order = 51)]
public class ResourcesPlaceProperties : EntityProperties 
{
    public enum ResourcesType
    {
        Wood,
        Rock
    }

    public ResourcesType resourcesType;

    public float resource;
}
