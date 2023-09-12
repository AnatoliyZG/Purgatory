using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;
using static UnityEngine.UI.CanvasScaler;

public class UnitAI : MonoBehaviour
{
    private Unit unit;
    private SphereCollider SphereCollider;
    private BoxCollider BoxCollider;
    private Entity target;
    private Coroutine PersecutionCoroutine;

    private void Start()
    {
        unit = GetComponent<Unit>();
        // SphereCollider = GetComponent<SphereCollider>();
        // BoxCollider = GetComponent<BoxCollider>();
        GameManager.instance.dayChange += OnDayChange;
    }

    public void OnDayChange(DayPhase dayPhase)
    {
        if (dayPhase == DayPhase.night)
        {
            unit.inputController.StartPath(Vector3.zero);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.GetComponent<Unit>();

        if (unit != null && entity.type == Entity.EntityType.Ally)
        {
            target = entity;

            if (PersecutionCoroutine != null)
                StartCoroutine(Persecution());

            PersecutionCoroutine = StartCoroutine(Persecution());
        }
    }*/

    private IEnumerator Persecution()
    {
        while (true)
        {
            unit.inputController.StartPath(target.transform.position);

            yield return new WaitForSeconds(.3f);
        }
    }
}