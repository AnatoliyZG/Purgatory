using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : Entity
{
    public override EntityProperties properties => buildProperties;

    public Vector2Int Size => buildProperties.Size;

    public BuildProperties buildProperties;

    public SphereCollider attackRadius;

    public List<Unit> workers = new List<Unit>();

    private Renderer MainRenderer;

    public Action OnPlace;

    public List<EntityAction<Building>> entityActions = new();

    public Action<Unit> OnEnter;

    public Action<Unit> OnQuit;

    public void AddWorker(Unit unit)
    {
        workers.Add(unit);
        OnEnter?.Invoke(unit);
    }

    public override void Start()
    {
        base.Start();

        MainRenderer = GetComponentInChildren<Renderer>();

        buildProperties = buildProperties.Clone<BuildProperties>();
    }

    public void SetStateColor(bool available)
    {
        if(MainRenderer == null)
        {
            return;
        }
        if (available) MainRenderer.material.color = Color.green;
        else MainRenderer.material.color = Color.red;

    }
    public void SetNormalColor()
    {
        MainRenderer.material.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < buildProperties.Size.x; x++)
        {
            for (int y = 0; y < buildProperties.Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0f, 1f, 0.22f, 1f);
                else Gizmos.color = new Color(0f, 0.48f, 0.1f, 1f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }

    public void SetPassiveActions()
    {
        foreach (var c in entityActions)
            if (!c.IsInterectable)
                c.Execute(this);
    }
}
