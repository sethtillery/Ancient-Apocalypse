using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType
{
    weaponUpgrade, ItemUpgrade, WeaponUnlock, ItemUnlock
}

[CreateAssetMenu]

public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public string Name;
    public Sprite icon;
    public string description;

    public weaponData weaponData;
    public WeaponStats weaponUpgradeStats;

    public Item item;
    public ItemStats stats;
    internal Text description1;
}

