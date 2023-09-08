using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourcesPlaceProperties;
using System.Linq;

public class MiningFerm : Building
{
    public List<ResourcesPlace> ResAround = new();

    public MiningFermProperties MiningFermProperties;

    private void Start()
    {
        base.Start();

        GetResourcesAround();

        OnPlace += GetResourcesAround;

        OnEnter += SetToWork;
    }

    public void SetToWork(Unit unit)
    {
        StartCoroutine(BeginToWork());
    }

    public IEnumerator BeginToWork()
    {
        while (true)
        {
            if (ResAround.Count == 0)
                yield break;

            yield return new WaitForSeconds(MiningFermProperties.MiningTime);
            if (MiningFermProperties.resourcesType == ResourcesType.Wood)
                ResourceController.Wood += MiningFermProperties.Efficiency;
            else if (MiningFermProperties.resourcesType == ResourcesType.Rock)
                ResourceController.Rock += MiningFermProperties.Efficiency;

            if (ResAround[0].Resource > 0)
                ResAround[0].Resource -= MiningFermProperties.Efficiency;

            if (ResAround[0].Resource <= 0)
                ResAround.RemoveAt(0);
        }
    }

    public void GetResourcesAround()
    {
        ResAround.AddRange(Physics.OverlapSphere(transform.position, MiningFermProperties.Radius, 1 << 9).Where(a=>a.GetComponent<ResourcesPlace>()).Select(a=>a.GetComponent<ResourcesPlace>()));

        ResAround = ResAround.OrderBy(a => Vector3.Distance(transform.position, a.transform.position)).ToList();
    }
}
