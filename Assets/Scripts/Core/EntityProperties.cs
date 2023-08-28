using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Stats", menuName = "Entity Properties", order = 51)]
public abstract class EntityProperties : ScriptableObject
{
    public string Name;
    public float Hp;
    public int visionDistance;

    public float Damage;
    public DmgType DamageType;

    public float Armor;
    public DmgType ArmorType;

    public SpellType SpellArmorType;

    public enum SpellType
    {
        Fire,
        Water,
        Earth,
        Air,
        Light,
        Darkness
    }

    public EntityProperties Clone()
    {
        return MemberwiseClone() as EntityProperties;
    }
}

public enum DmgType
{
    Stabbing,   // ������� ���� ��� ����� � ������ � ��������
    Normal,     // ��� ������ � ������
    Heavy,      // ������� �����
    Magic,      // ����
    None        // ���������� ��������� ��� �������� ����

    // ��� ���� ����� ����: magic => heavy => normal => stabbing => magic
}