using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float Damage;
    public float Radius;

    public DmgType type;

    public void Execute(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, Radius);
        if (colliders != null)
        {
            foreach (Collider c in colliders)
            {
                Entity entity = c.GetComponent<Entity>();

                if(entity != null)
                {
                    Execute(entity);
                }
            }
        }
    }

    public void Execute(Entity target)
    {
        target.fighting.GetHit(new Impact(target, type, Damage));
    }
}
