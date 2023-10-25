using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : Entity, IMapObject
{
    public override EntityProperties properties => buildProperties;

    public int Size => buildProperties.SizeX;

    public BuildProperties buildProperties;

    public SphereCollider attackRadius;

    private Renderer mainRenderer;

    public List<Unit> workers = new List<Unit>();

    public List<EntityAction<Building>> entityActions = new();

    public Action OnPlace;

    public Action<Unit> OnEnter;

    public Action<Unit> OnQuit;

    public int x { get; set; } = 0;

    public int y { get; set; } = 0;

    public int angle { get; set; } = 0;

    public override void Start()
    {
        base.Start();

        mainRenderer = GetComponentInChildren<Renderer>();

        buildProperties = buildProperties.Clone<BuildProperties>();

        OnDead += () =>
        {
            foreach (var b in entityActions)
            {
                b.Cancel();
            }

            GameManager.instance.buildings.Remove(this);

            Dispose();
        };
 
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
}
