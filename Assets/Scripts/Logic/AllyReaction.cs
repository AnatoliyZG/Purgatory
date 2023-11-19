using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllyReaction : UnitReaction
{
    protected override void Start()
    {
        base.Start();

        StartCoroutine(seekCoroutine());

        if (attackState == AttackState.Holding)
            reactor.fighting.onGetHit += reactor.inputController.StartPath;
    }

    protected override IEnumerator seekCoroutine()
    {
        while (true)
        {
            if (victims.Count > 0)
            {
                if (attackState == AttackState.Agressive)
                    reactor.inputController.StartPath(victims[0]);
                else if (attackState == AttackState.Passive)
                {
                    victims = (List<Entity>)(from p in victims orderby Vector3.Distance(p.transform.position,reactor.transform.position) select p);
                    if (Vector3.Distance(victims[0].transform.position, reactor.transform.position) <= reactor.unitProperties.AttackRange) 
                    reactor.inputController.StartPath(victims[0]);
                }
            }

            yield return new WaitForSeconds(.5f);
        }
    }
}
