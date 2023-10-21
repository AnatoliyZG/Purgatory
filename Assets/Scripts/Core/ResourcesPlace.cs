using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPlace : MonoBehaviour
{
    public ResourcesType resourcesType;

    public float resource;

    public float Resource
    {
        get => resource;
        set
        {
            if (value <= 0)
                Destroy(gameObject);
            resource = value;
        }
    }
}
