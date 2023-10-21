using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiningFerm : Building
{
    public List<ResourcesPlace> ResAround = new();

    public MiningFermProperties MiningFermProperties => properties as MiningFermProperties;

    public GameObject exlamation;

    private void Awake()
    {
        OnPlace += () =>
        {
            GetResourcesAround();

            OnEnter += SetToWork;
        };
    }

    public void SetToWork(Unit unit)
    {
        if (exlamation != null && ResAround.Count == 0) { }
        else if (exlamation != null) 
            Destroy(exlamation);

        StartCoroutine(BeginToWork());
    }

    public IEnumerator BeginToWork()
    {
        while (true)
        {
            if (ResAround.Count == 0 || workers.Count == 0)
            {
                StopWorking();
                exlamation.SetActive(true);

            }

            yield return new WaitForSeconds(MiningFermProperties.MiningTime);
            ResourcesPlace current = ResAround[0];

            if (current.Resource > 0)
            {
                switch (MiningFermProperties.resourcesType)
                {
                    case ResourcesType.Wood:
                        ResourceController.wood += MiningFermProperties.Efficiency;
                        break;
                    case ResourcesType.Rock:
                        ResourceController.rock += MiningFermProperties.Efficiency;
                        break;
                }

                current.Resource -= MiningFermProperties.Efficiency;
            }

            if(current.Resource <= 0)
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

    public void StopWorking()
    {
        StopAllCoroutines();

        if (workers.Count == 0)
            exlamation.SetActive(true);
    }
}
