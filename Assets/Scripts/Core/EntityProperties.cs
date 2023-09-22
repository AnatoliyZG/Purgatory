using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//[CreateAssetMenu(fileName = "Stats", menuName = "Entity Properties", order = 51)]

public abstract class EntityProperties : ScriptableObject
{
    public string Name;
    public float Hp;
    public float MaxHp;
    public float Mp;
    public float MaxMp;
    public int VisionDistance;

    public float AttackRange;
    public float AttackFrequency;
    public float Damage;
    public DmgType DamageType;

    public float Armor;
    public DmgType ArmorType;

    public T Clone<T>() where T: EntityProperties
    {
        return MemberwiseClone() as T;
    }

    public Sprite Image;
}

public enum DmgType
{
    Stabbing,   // stabbe damage and archers
    Normal,     // with swords
    Heavy,      // strong and slow unit
    Magic,      // use magic
    None        // This unit can't damaging - 0 dmg

    // Balance: magic => heavy => normal => stabbing => magic
}