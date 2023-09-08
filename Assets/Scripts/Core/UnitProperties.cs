using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new unit properties", menuName = "Unit Properties", order = 51)]
public class UnitProperties : EntityProperties
{
    public float MoveSpeed;



    public float CriticalChanse = 10f;
    public float CriticalDamage = 1.5f;
}
