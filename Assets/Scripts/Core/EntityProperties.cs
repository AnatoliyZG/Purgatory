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
        Stabbing,   // колющий урон для челов с пиками и лучников
        Normal,     // для юнитов с мечами
        Heavy,      // тяжелые юниты
        Magic,      // маги
        None        // неспособен атоковать или получать урон

        // Кто кого лучше бъёт: magic => heavy => normal => stabbing => magic
    }

    public EntityProperties Clone()
    {
        return (EntityProperties)MemberwiseClone();
    }
}
