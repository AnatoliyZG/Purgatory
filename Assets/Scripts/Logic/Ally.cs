using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Unit
{
    public UnitType unitType;

    public AttackState attackState;

    public enum UnitType
    {
        Worker,
        Capitan,
        Hero
    }

    public enum AttackState
    {
        Agrassive,
        Passive,
        Holding
    }

    public override void Start()
    {
        base.Start();

        SetProperties(unitProperties);

        if (type == EntityType.Ally && unitType != UnitType.Worker)
        {
//            GroupController.Add(this);
        }

        if (attackState == AttackState.Holding)
            fighting.onGetHit += inputController.StartPath;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            switch (attackState)
            {
                case AttackState.Agrassive:
                    inputController.StartPath(other.GetComponent<Entity>());
                    break;
                case AttackState.Passive:
                    if (Vector3.Distance(other.transform.position, transform.position) <= unitProperties.AttackRange)
                        inputController.StartPath(other.GetComponent<Entity>());
                    break;
            }
        }
    }
}
