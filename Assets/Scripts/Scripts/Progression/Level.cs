using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int experience = 0;
    public int currentLevel = 1;
    public int counter = 0;
    [SerializeField] ExpBar expBar;
    [SerializeField] UpgradeManager upgrade;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrade;

    [SerializeField] List<UpgradeData> acquiredUpgrades;

    WeaponManager weaponManager;
    passiveItems passiveItems;

    [SerializeField] List<UpgradeData> startUpgrades;
    LevelCompletion levelCompletion;

    Coins Coins;

    int TO_LEVEL_UP
    {
        get
        {
            if (currentLevel >= 2 && currentLevel <= 20)
                return currentLevel * 5 + counter * 5;
            else if (currentLevel > 20 && currentLevel <= 40)
                return currentLevel * 6 + counter * 6;
            
            return  5; 
        }
    }

    internal void AddUpgradesToAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        if(upgradesToAdd == null) { return; }

        this.upgrades.AddRange(upgradesToAdd);
    }

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItems = GetComponent<passiveItems>();
        AddUpgradesToAvailableUpgrades(startUpgrades);
    }

    private void Start()
    {
        expBar.updateExpSlider(experience, TO_LEVEL_UP);
        expBar.setLevelText(currentLevel);
        levelCompletion = GameObject.Find("World").GetComponent<LevelCompletion>();
        Coins = GetComponent<Coins>();
    }

    internal void Upgrade(int selectedUpgradeID)
    {
        UpgradeData upgradeData = selectedUpgrade[selectedUpgradeID];

        if (acquiredUpgrades == null)
            acquiredUpgrades = new List<UpgradeData>();

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.weaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUpgrade:
                passiveItems.UpgradeItem(upgradeData);
                break;
            case UpgradeType.WeaponUnlock:
                weaponManager.addWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.ItemUnlock:
                passiveItems.Equip(upgradeData.item);
                AddUpgradesToAvailableUpgrades(upgradeData.item.upgrades);
                break;
        }

        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    public void addExperience(int amount)
    {
        experience += amount;
        checkLevelUp();
        expBar.updateExpSlider(experience, TO_LEVEL_UP);
    }

    public void checkLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            counter++;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if(selectedUpgrade == null)
            selectedUpgrade = new List<UpgradeData>();

        selectedUpgrade.Clear();
        selectedUpgrade.AddRange(GetUpgrades(3));


        currentLevel += 1;
        if(currentLevel < levelCompletion.winLevel)
            upgrade.openPanel(selectedUpgrade);
        experience = 0;
        expBar.setLevelText(currentLevel);
        awardCoins(currentLevel);
    }

    private void awardCoins(int level)
    {
        switch(level)
        {
            case 2:
                Coins.Add(level);
                break;
            case 10:
                Coins.Add(level);
                break;
            case 15:
                Coins.Add(level);
                break;
            case 20:
                Coins.Add(level);
                break;
            case 25:
                Coins.Add(level);
                break;
            case 30:
                Coins.Add(level);
                break;
            case 35:
                Coins.Add(level);
                break;
            case 40:
                Coins.Add(level);
                break;
        }
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if(count > upgrades.Count)
            count = upgrades.Count;
        

        for(int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }

        return upgradeList;
    }
}
