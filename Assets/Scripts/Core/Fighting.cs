using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fighting : EntityFeature<Entity>
{
    public event Action<Impact> onGetHit;
    public event Action<Impact> onAttack;

    public float Damage;

    public void Attack(Entity target)
    {
        Impact impact = new Impact(target, entity);
        onAttack?.Invoke(impact);

        target.fighting.GetHit(impact);
        _lastAttack = Time.time;
    }

    public void GetHit(Impact impact)
    {
        onGetHit?.Invoke(impact);

        entity.properties.Hp -= impact.damage;
    }

    public Fighting(Entity entity) : base(entity)
    {

    }

    public void BeginAttacking(Entity target)
    {
        if (Time.time - _lastAttack >= entity.properties.AttackFrequency)
            Attack(target);
    }

    private float _lastAttack;
}

public class Impact
{
    public Entity offender;
    public Entity target;

    public DmgType dmgType;

    public float damage;

    private float damageMultily = 1;

    public Impact(Entity target, Entity offender)
    {
        this.target = target;
        this.offender = null;

        dmgType = offender.properties.DamageType;
        damage = offender.properties.Damage;

        DifferentType(target.properties.ArmorType, dmgType);

       // Damage = (currentDamege - target.properties.Armor) * damageMultily;
    }

    public Impact(Entity target, DmgType dmgType, float damage)
    {
        this.target = target;
        this.dmgType = dmgType;
        this.damage = damage;

        DifferentType(target.properties.ArmorType, dmgType);
    }


    private void DifferentType(DmgType targetType, DmgType damageType)
    {
        if (damageType == DmgType.None || targetType == DmgType.None)
            damageMultily = 0;
        else
        {
            switch (damageType)
            {
                case DmgType.Stabbing:
                    switch (targetType)
                    {
                        case DmgType.Normal:
                            damageMultily -= 0.25f;
                            break;
                        case DmgType.Magic:
                            damageMultily += 0.25f;
                            break;
                    }
                    break;

                case DmgType.Normal:
                    switch (targetType)
                    {
                        case DmgType.Heavy:
                            damageMultily -= 0.25f;
                            break;
                        case DmgType.Stabbing:
                            damageMultily += 0.25f;
                            break;
                    }
                    break;

                case DmgType.Heavy:
                    switch (targetType)
                    {
                        case DmgType.Magic:
                            damageMultily -= 0.25f;
                            break;
                        case DmgType.Normal:
                            damageMultily += 0.25f;
                            break;
                    }
                    break;

                case DmgType.Magic:
                    switch (targetType)
                    {
                        case DmgType.Stabbing:
                            damageMultily -= 0.25f;
                            break;
                        case DmgType.Heavy:
                            damageMultily += 0.25f;
                            break;
                    }
                    break;
            }
        }
    }

    private bool IsCrit(float chance)
    {
        return (Random.Range(0f, 100f) < chance) ;
    }
}