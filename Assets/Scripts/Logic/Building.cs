using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : Entity, IMapObject
{
    public override EntityProperties properties => buildProperties;

    public int Size => buildProperties.Size;

    public BuildProperties buildProperties;

    public SphereCollider attackRadius;

    public BoxCollider boxCollider;

    public List<Unit> workers = new List<Unit>();

    public Action OnPlace;

    public Action<Unit> OnEnter;

    public Action<Unit> OnQuit;

    public int x { get; set; } = 0;

    public int y { get; set; } = 0;

    public int angle { get; set; } = 0;

    private Transform buildingTranform => transform.GetChild(0);

    private Renderer mainRenderer;

    public override void Start()
    {
        base.Start();

        OnPlace += PayCost;

        mainRenderer = GetComponentInChildren<Renderer>();

        SetProperties(buildProperties);

        StartCoroutine(BuildingHeal());

        OnChangeHp += SetHpSlider;

        OnDead += () =>
        {
            foreach (var b in buildProperties.entityActions)
            {
                b.Cancel();
            }

            GameManager.instance.buildings.Remove(this);

            Dispose();
        };
 
    }

    public void SetProperties(BuildProperties buildProperties)
    {
        this.buildProperties = buildProperties.Clone<BuildProperties>();

        Vector3 size = new Vector3(Size, 1, Size);
        Vector3 offset = size / 2f;

        buildingTranform.localScale = size;
        buildingTranform.localPosition = offset;

        boxCollider.size = size;
        boxCollider.center = offset;

        attackRadius.radius = buildProperties.AttackRange;
        attackRadius.center = offset;
    }

    public void AddWorker(Unit unit)
    {
        workers.Add(unit);
        OnEnter?.Invoke(unit);
    }

    public void SetStateColor(bool available)
    {
        if(mainRenderer == null)
        {
            return;
        }
        if (available) mainRenderer.material.color = Color.green;
        else mainRenderer.material.color = Color.red;

    }
    public void SetNormalColor()
    {
        mainRenderer.material.color = Color.white;
    }

    public void Dispose()
    {
        BuildingController.BuildingAction(this, x, y, (x, y) =>
        {
            GameManager.instance.GetCell(x, y) = false;

            return true;
        });
    }

    public void PayCost()
    {
        ResourceController.wood -= buildProperties.WoodCost;

        ResourceController.rock -= buildProperties.RockCost;
    }

    private IEnumerator BuildingHeal()
    {
        while (true)
        {
            if (workers.Count > 0 && GameManager.instance.currentPhase == DayPhase.day) 
            {
                yield return new WaitForSeconds(buildProperties.HealTime);

                buildProperties.Hp = +buildProperties.HealScale * workers.Count;
            }

            yield return new WaitForSeconds(.5f);
        }
    }
}
