using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : Building
{
    public float DamageToTrap;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Unit>().properties.Hp -= properties.Damage;

        properties.Hp -= DamageToTrap;
    }
}
