using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReaction : UnitReaction
{
    protected override void Start()
    {
        base.Start();

        StartCoroutine(seekCoroutine());
    }

    protected override IEnumerator seekCoroutine()
    {
        while (true)
        {
            if (victims.Count > 0)
            {
                reactor.inputController.StartPath(victims[0]);
            }

            reactor.inputController.StartPath(GameManager.instance.Base);

            yield return new WaitForSeconds(.5f);
        }
    }
}
