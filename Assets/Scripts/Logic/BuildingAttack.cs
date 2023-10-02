using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.CanvasScaler;

public class BuildingAttack : MonoBehaviour
{
    public Building building => GetComponent<Building>();


    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.GetComponent<Unit>();

        if (entity != null && entity.type != building.type)
        {
            foreach (Unit unit in building.workers)
            {
                unit.fighting.Attack(entity);
            }
        }
    }
}
