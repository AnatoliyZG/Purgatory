using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAction<T> : ScriptableObject
{
    public string Description;

    public abstract void Execute(T obj);
}
