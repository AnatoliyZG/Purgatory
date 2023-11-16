using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyReaction : UnitReaction
{
    protected override void Start()
    {
        base.Start();

        StartCoroutine(seekCoroutine());
    }

    private IEnumerator seekCoroutine()
    {
        while (true)
        {
            if (victims.Count > 0)
            {

            }

            yield return new WaitForSeconds(.5f);
        }
    }

    public override void UnitComeIn(Entity entity)
    {
        base.UnitComeIn(entity);
    }

    public override void UnitLeave(Entity entity)
    {
        base.UnitLeave(entity);
    }

}
