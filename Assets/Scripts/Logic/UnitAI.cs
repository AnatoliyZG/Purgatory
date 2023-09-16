using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;
using static UnityEngine.UI.CanvasScaler;

public class UnitAI : MonoBehaviour
{
    private Unit unit;
    private Entity target;

    private void Start()
    {
        unit = GetComponent<Unit>();
        GameManager.instance.dayChange += OnDayChange;
    }

    public void OnDayChange(DayPhase dayPhase)
    {
        if (dayPhase == DayPhase.night)
        {
            unit.inputController.StartPath(Vector3.zero);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.GetComponent<Unit>();

        if (entity != null && entity.type == Entity.EntityType.Ally)
        {
            target = entity;
            unit.inputController.StartPath(target);
        }
    }
}