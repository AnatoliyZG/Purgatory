using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Entity Properties", order = 51)]
public class EntityProperties : ScriptableObject
{
    public string Name;
    public float Hp;
    public float Armor;
    public float MoveSpeed;

    public DmgType DamageType;
    public DmgType ArmorType;

    public enum DmgType
    {
        Stabbing,   // ������� ���� ��� ����� � ������ � ��������
        Normal,     // ��� ������ � ������
        Heavy,      // ������� �����
        Magic,      // ����
        None        // ���������� ��������� ��� �������� ����

        // ��� ���� ����� ����: magic => heavy => normal => stabbing => magic
    }

    public EntityProperties Clone()
    {
        return (EntityProperties)MemberwiseClone();
    }
}
