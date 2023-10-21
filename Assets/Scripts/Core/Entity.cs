using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Entity : MonoBehaviour
{
    public abstract EntityProperties properties { get; }

    public Fighting fighting;

    public EntityType type;

    public Action OnDead;

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

    private void OnDestroy()
    {
        OnDead?.Invoke();
    }
}
