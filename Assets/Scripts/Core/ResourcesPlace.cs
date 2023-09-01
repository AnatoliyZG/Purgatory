using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPlace : MonoBehaviour
{
    public ResourcesPlaceProperties resourcesPlaceProperties;

    private float _resource;

    public float Resource
    {
        get => _resource;
        set
        {
            if (value <= 0)
                Destroy(GetComponent<GameObject>());
            else
                _resource = value;
        }
    }

    private void Start()
    {
        _resource = resourcesPlaceProperties.resource;
    }
}
