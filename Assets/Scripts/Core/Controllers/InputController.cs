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
        direction = new Vector3(direction.x * MoveSpeed, rig.velocity.y, direction.z * MoveSpeed);
        rig.velocity = direction;
    }

    public virtual void Start()
    {
        entity = GetComponent<Entity>();
        rig = GetComponent<Rigidbody>();
    }
}
