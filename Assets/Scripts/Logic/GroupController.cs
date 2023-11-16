using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
public class GroupController : MonoBehaviour
{
    public static GroupController instance;

    public Action<Unit> onAddHero;

    public Action<Unit> onAddCaptain;

    public void Awake()
    {
        instance = this;
    }

    public static void Add(Unit unit)
    {
        if (instance == null)
            return;

        switch (unit.unitType)
        {
            case Unit.UnitType.Hero:
                instance.onAddHero?.Invoke(unit);
                return;
            case Unit.UnitType.Capitan:
                instance.onAddCaptain?.Invoke(unit);
                return;
        }
    }
}
*/
