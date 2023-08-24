using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float Damage;
    public float Radius;
    public float Force = 0;

    public EntityProperties.SpellType type;


    public void Execute(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, Radius);
        if (colliders != null)
            foreach (Collider c in colliders)
                if (c.attachedRigidbody != null)
                {
                    c.attachedRigidbody.AddExplosionForce(Force, position, Radius);

                    Entity goal = c.transform.GetComponent<Entity>();
                    goal.fighting.Attack(goal, Damage, EntityProperties.DmgType.Magic);
                }
    }

    public void Execute(Entity[] targets)
    {

    }
}
