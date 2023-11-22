using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Entity : MonoBehaviour
{
    public SphereCollider VisionSphere;

    public abstract EntityProperties properties { get; }

    public Fighting fighting;

    public GameObject Projector;

    public EntityType type;

    public Action OnDead;

    public GameObject HpSlider;

    public Action OnChangeHp;

    public enum EntityType
    {
        Ally,
        Monster,
        Neutral
    }

    public bool IsDamaged()
    {
        if (properties.Hp != properties.MaxHp)
            return true;
        else
            return false;
    }

    public virtual void Start()
    {
        fighting = new Fighting(this);
    }

    private void OnDestroy()
    {
        OnDead?.Invoke();
    }

    public void SetHpSlider()
    {
        if (IsDamaged())
        {
            HpSlider.SetActive(true);
        }
        else
            HpSlider.SetActive(false);
    }

}
