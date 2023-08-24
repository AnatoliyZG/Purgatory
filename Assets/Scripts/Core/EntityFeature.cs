using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityFeature<T>
{
    public T entity;

    public EntityFeature(T entity)
    {
        this.entity = entity;
    }

}
