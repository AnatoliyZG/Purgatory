using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : MonoBehaviour
{
    public event Action<Impact> onGetHit;
    public event Action<Entity> onAttack;

    public float Damage;
    public float ChanceCreteDamage = 10f;

    public void Attack(Entity target)
    {
        onAttack?.Invoke(target);

        target.GetComponent<Fighting>().GetHit(new Impact(transform.GetComponent<Entity>(), target));
    }

    public void GetHit(Impact impact)
    {
        onGetHit?.Invoke(impact);

        transform.GetComponent<EntityProperties>().Hp -= Damage;
    }
}

public class Impact
{
    public float Damage;

    private float _changeDamage = 1;

    private EntityProperties.DmgType damageType;
    private EntityProperties.DmgType offenceType;

    public Impact(Entity agressor, Entity offender)
    {
        DifferentType(agressor, offender);

        Damage = (agressor.GetComponent<Fighting>().Damage - offender.GetComponent<EntityProperties>().Armor) * _changeDamage;

        // Определяем будет ли крит
        if (UnityEngine.Random.Range(0f, 100f) <= agressor.GetComponent<Fighting>().ChanceCreteDamage)
            Damage *= 2;
    }

    private void DifferentType(Entity agressor, Entity offender)
    {
        damageType = agressor.GetComponent<EntityProperties>().DamageType;
        offenceType = offender.GetComponent<EntityProperties>().ArmorType;

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