using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPlace : MonoBehaviour, IMapObject
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

    public int x { get; set; }
    public int y { get; set; }
    public int angle { get; set; }

    public void Dispose()
    {
        GameManager.instance.GetCell(x, y) = false;
    }

    private void OnDestroy()
    {
        Dispose();
    }
}
