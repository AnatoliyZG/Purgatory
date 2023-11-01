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

    private Renderer mainRenderer;

    public List<Unit> workers = new List<Unit>();

    public Action OnPlace;

    public Action OnSell;

    public Action<Unit> OnEnter;

    public Action<Unit> OnQuit;

    private Transform buildingTranform => transform.GetChild(0);

    public int x { get; set; } = 0;

    public int y { get; set; } = 0;

    public int angle { get; set; } = 0;

    public override void Start()
    {
        base.Start();

        OnPlace += PayCost;

        mainRenderer = GetComponentInChildren<Renderer>();

        SetProperties(buildProperties);

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
}
