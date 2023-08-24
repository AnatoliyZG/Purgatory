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

    public void Attack(Entity target, float currentDamege, EntityProperties.DmgType type)
    {
        Impact impact = new Impact(target, currentDamege, type, ChanceCritDamage);
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

    public Impact(Entity target, float currentDamege, EntityProperties.DmgType offenceType, float chanceCrit = 0)
    {
        DifferentType(target.properties.ArmorType, offenceType);

        Damage = (currentDamege - target.properties.Armor) * _changeDamage;

        IsCrit(chanceCrit);
    }

    private void DifferentType(EntityProperties.DmgType targetType, EntityProperties.DmgType damageType)
    {
        
        if (damageType == EntityProperties.DmgType.None || targetType == EntityProperties.DmgType.None)
            _changeDamage = 0;
        else
        {
            switch (damageType)
            {
                case EntityProperties.DmgType.Stabbing:
                    switch (targetType)
                    {
                        case EntityProperties.DmgType.Normal:
                            _changeDamage -= 0.25f;
                            break;
                        case EntityProperties.DmgType.Magic:
                            _changeDamage += 0.25f;
                            break;
                    }
                    break;

                case EntityProperties.DmgType.Normal:
                    switch (targetType)
                    {
                        case EntityProperties.DmgType.Heavy:
                            _changeDamage -= 0.25f;
                            break;
                        case EntityProperties.DmgType.Stabbing:
                            _changeDamage += 0.25f;
                            break;
                    }
                    break;

                case EntityProperties.DmgType.Heavy:
                    switch (targetType)
                    {
                        case EntityProperties.DmgType.Magic:
                            _changeDamage -= 0.25f;
                            break;
                        case EntityProperties.DmgType.Normal:
                            _changeDamage += 0.25f;
                            break;
                    }
                    break;

                case EntityProperties.DmgType.Magic:
                    switch (targetType)
                    {
                        case EntityProperties.DmgType.Stabbing:
                            _changeDamage -= 0.25f;
                            break;
                        case EntityProperties.DmgType.Heavy:
                            _changeDamage += 0.25f;
                            break;
                    }
                    break;
            }
        }
    }

    private void IsCrit(float chance)
    {
        if (Random.Range(0f, 100f) < chance)
            Damage *= 2;
    }
}