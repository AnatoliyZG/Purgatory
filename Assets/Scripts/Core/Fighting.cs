using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fighting : MonoBehaviour
{
    public event Action<Impact> onGetHit;
    public event Action<Impact> onAttack;

    public float Damage;
    public float ChanceCritDamage = 10f;

    public void Attack(Entity target)
    {
        Impact impact = new Impact(transform.GetComponent<Entity>(), target);
        onAttack?.Invoke(impact);

        target.fighting.GetHit(impact);
    }

    public void GetHit(Impact impact)
    {
        onGetHit?.Invoke(impact);

        transform.GetComponent<EntityProperties>().Hp -= impact.Damage;
    }
}

public class Impact
{
    public float Damage;
    private float _changeDamage = 1;

    public Impact(Entity agressor, Entity target)
    {
        DifferentType(agressor, target);

        Damage = (agressor.fighting.Damage - target.properties.Armor) * _changeDamage;

        // Определяем будет ли крит
        if (Random.Range(0f, 100f) < agressor.fighting.ChanceCritDamage)
            Damage *= 2;
    }

    private void DifferentType(Entity agressor, Entity target)
    {
        var damageType = agressor.properties.DamageType;
        var offenceType = target.properties.ArmorType;
        
        if (damageType == EntityProperties.DmgType.None || offenceType == EntityProperties.DmgType.None)
            _changeDamage = 0;
        else
        {
            switch (damageType)
            {
                case EntityProperties.DmgType.Stabbing:
                    switch (offenceType)
                    {
                        case EntityProperties.DmgType.Normal:
                            _changeDamage = 0.75f;
                            break;
                        case EntityProperties.DmgType.Magic:
                            _changeDamage = 1.25f;
                            break;
                    }
                    break;

                case EntityProperties.DmgType.Normal:
                    switch (offenceType)
                    {
                        case EntityProperties.DmgType.Heavy:
                            _changeDamage = 0.75f;
                            break;
                        case EntityProperties.DmgType.Stabbing:
                            _changeDamage = 1.25f;
                            break;
                    }
                    break;

                case EntityProperties.DmgType.Heavy:
                    switch (offenceType)
                    {
                        case EntityProperties.DmgType.Magic:
                            _changeDamage = 0.75f;
                            break;
                        case EntityProperties.DmgType.Normal:
                            _changeDamage = 1.25f;
                            break;
                    }
                    break;

                case EntityProperties.DmgType.Magic:
                    switch (offenceType)
                    {
                        case EntityProperties.DmgType.Stabbing:
                            _changeDamage = 0.75f;
                            break;
                        case EntityProperties.DmgType.Heavy:
                            _changeDamage = 1.25f;
                            break;
                    }
                    break;
            }
        }
    }
}