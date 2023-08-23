using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public EntityProperties properties;

    public InputController controller;

    public Fighting fighting;

    public EntityType type;

    public enum EntityType
    {
        Ally,
        Monster,
        Neutral
    }

    public virtual void Start()
    {
        properties = properties.Clone();
    }
}
