using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAction<T> : ScriptableObject
{
    public abstract string Description { get; }

    public abstract bool IsInterectable { get; }

    public abstract void Execute(T obj);

    public virtual void Cancel() { }
}
