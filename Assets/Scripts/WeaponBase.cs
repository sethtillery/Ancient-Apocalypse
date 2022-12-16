using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public weaponData weaponData;

    public WeaponStats weaponStats;

    public float TimeToAttack = 1f;
    float timer;

    public void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0f)
        {
            Attack();
            timer = TimeToAttack;
        }
    }

    public virtual void SetData(weaponData weapon)
    {
        weaponData = weapon;
        TimeToAttack = weaponData.stats.timeToAttack;

        weaponStats = new WeaponStats(weapon.stats.damage, weapon.stats.timeToAttack);
    }

    public abstract void Attack();
    
    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }
}
