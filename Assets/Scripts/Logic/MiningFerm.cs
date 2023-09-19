using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiningFerm : Building
{
    public List<ResourcesPlace> ResAround = new();

    public GameObject Exclamation;

    public MiningFermProperties MiningFermProperties => properties as MiningFermProperties;

    public GameObject exlamation;

    private void Start()
    {
        GetResourcesAround();

        OnPlace += GetResourcesAround;

        OnEnter += SetToWork;

        exlamation = Instantiate(Exclamation, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(-180f, 0f, 0f));
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
                exlamation = Instantiate(Exclamation, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(-180f, 0f, 0f));
            }

            yield return new WaitForSeconds(MiningFermProperties.MiningTime);

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

    public void StopWorking()
    {
        StopAllCoroutines();

        if(workers.Count == 0)
            exlamation = Instantiate(Exclamation, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(-180f, 0f, 0f));
    }
}
