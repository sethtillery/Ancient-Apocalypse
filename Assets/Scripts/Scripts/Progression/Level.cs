using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int experience = 0;
    [SerializeField] int level = 1;
    public int counter = 0;
    [SerializeField] ExpBar expBar;
    [SerializeField] UpgradeManager upgrade;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrade;

   [SerializeField] List<UpgradeData> acquiredUpgrades;

    WeaponManager weaponManager;

    int TO_LEVEL_UP
    {
        get
        {
            if (level >= 2 && level <= 20)
                return  level * 5 + counter * 5;
            
            return  5; 
        }
    }

    internal void AddUpgradesToAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        this.upgrades.AddRange(upgradesToAdd);
    }

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Start()
    {
        expBar.updateExpSlider(experience, TO_LEVEL_UP);
        expBar.setLevelText(level);
    }

    internal void Upgrade(int selectedUpgradeID)
    {
        UpgradeData upgradeData = selectedUpgrade[selectedUpgradeID];

        if (acquiredUpgrades == null)
            acquiredUpgrades = new List<UpgradeData>();

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.weaponUpgrade:
                break;
            case UpgradeType.ItemUpgrade:
                break;
            case UpgradeType.WeaponUnlock:
                weaponManager.addWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.ItemUnlock:
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
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if(selectedUpgrade == null)
            selectedUpgrade = new List<UpgradeData>();

        selectedUpgrade.Clear();
        selectedUpgrade.AddRange(GetUpgrades(3));


        upgrade.openPanel(selectedUpgrade);
        counter++;
        experience = 0;
        level += 1;
        expBar.setLevelText(level);
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
