using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private List<Entity> victims;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity>().type == EntityType.Ally)
        {
            if (victims.Count==0)
                inputController.StartPath(other.GetComponent<Entity>());

            victims.Add(other.GetComponent<Entity>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Entity>().type == EntityType.Ally)
        {
            victims.Remove(other.GetComponent<Entity>());

            if (other.GetComponent<Entity>() != victims[0]) { }
            else
            {
                if (victims.Count == 0)
                    inputController.StartPath(GameManager.instance.Base);
                else
                    inputController.StartPath(victims[0]);
            }

        }
    }

    public void AttackBase()
    {
        inputController.StartPath(GameManager.instance.Base);
    }

    public override void Start()
    {
        base.Start();

        AttackBase();
    }

}
