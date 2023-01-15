using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum PlayerPermanentUpgrades
{
    Damage,
    Hp,
    Armor,
    FasterWeapons, 
    BiggerAttackRadius,
    FasterProjectiles,
    FasterMovement,
    moreXP
}

[Serializable]
public class PlayerUpgrades
{
    public PlayerPermanentUpgrades permanentUpgrades;
    public int level = 0;
    public int maxLevel = 0;
    public int costToUpgrade = 100;
}

[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    public int coins;

    public List<PlayerUpgrades> playerUpgrades;

    public int GetUpgradeLevel(PlayerPermanentUpgrades persistantUpgrade)
    {
        Debug.Log(playerUpgrades[(int)persistantUpgrade].level.ToString());
        return playerUpgrades[(int)persistantUpgrade].level;
    }
}
