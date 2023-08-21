using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    public float MoveSpeed => entity.properties.MoveSpeed;
    private Entity entity;
    private Rigidbody rig;

    public void Move(Vector3 direction)
    {
        direction.y = rig.velocity.y;
        rig.velocity = direction * MoveSpeed;
    }

    public virtual void Start()
    {
        entity = GetComponent<Entity>();
        rig = GetComponent<Rigidbody>();
    }
}
