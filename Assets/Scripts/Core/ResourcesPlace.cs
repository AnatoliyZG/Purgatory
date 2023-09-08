using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPlace : Entity
{
    public override EntityProperties properties => resourcesPlaceProperties;

    public ResourcesPlaceProperties resourcesPlaceProperties;

    public float _resource;

    public float Resource
    {
        get => _resource;
        set
        {
            if (value <= 0) 
                Destroy(gameObject);
            else
                _resource = value;
        }
    }

    public override void Start()
    {
        _resource = resourcesPlaceProperties.resource;
    }
}
