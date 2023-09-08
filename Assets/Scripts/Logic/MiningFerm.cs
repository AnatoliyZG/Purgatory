using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiningFerm : Building
{
    public List<ResourcesPlace> ResAround = new();

    public MiningFermProperties MiningFermProperties => properties as MiningFermProperties;

    private void Start()
    {
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
            yield return new WaitForSeconds(MiningFermProperties.MiningTime);

            if (ResAround.Count == 0)
                yield break;

            if(ResAround[0].Resource > 0)
            {
                if (MiningFermProperties.resourcesType == ResourcesPlaceProperties.ResourcesType.Wood)
                    ResourceController.Wood += MiningFermProperties.Efficiency;
                else if (MiningFermProperties.resourcesType == ResourcesPlaceProperties.ResourcesType.Rock)
                    ResourceController.Rock += MiningFermProperties.Efficiency;

                ResAround[0].Resource -= MiningFermProperties.Efficiency;
            }

            if(ResAround[0].Resource <= 0)
            {
                ResAround.RemoveAt(0);
            }
        }
    }

    public void GetResourcesAround()
    {
        ResAround.AddRange(Physics.OverlapSphere(transform.position, MiningFermProperties.Radius, 1 << 9).Where(a=>a.GetComponent<ResourcesPlace>()).Select(a=>a.GetComponent<ResourcesPlace>()));

        ResAround = ResAround.OrderBy(a => Vector3.Distance(transform.position, a.transform.position)).ToList();
    }
}
