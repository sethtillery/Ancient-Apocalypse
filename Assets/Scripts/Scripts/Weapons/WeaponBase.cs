using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public weaponData weaponData;

    public WeaponStats weaponStats;

    float timer;

    CharacterStats weaponWielder;

    public void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0f)
        {
            Attack();
            timer = weaponStats.timeToAttack;
        }
    }

    public virtual void SetData(weaponData weapon)
    {
        weaponData = weapon;
               
        weaponStats = new WeaponStats(weapon.stats.damage, weapon.stats.timeToAttack - weaponWielder.weaponAttackBonus, weapon.stats.numberOfAttacks);        
    }

    public abstract void Attack();

    public int GetDamage()
    {
        int damage = (weaponData.stats.damage);
        if((int)weaponWielder.damageBonus == 0)
            return damage;
        else
            return damage * (int)weaponWielder.damageBonus;
    }
    
    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    internal void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

    internal void addCharacter( CharacterStats character)
    {
        weaponWielder = character;
    }
}
