using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract EntityProperties properties { get; }

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
        fighting = new Fighting(this);
    }
}
