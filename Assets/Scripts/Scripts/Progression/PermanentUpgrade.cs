using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PermanentUpgrade : MonoBehaviour
{
    [SerializeField] PlayerPermanentUpgrades upgrade;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] DataContainer dataContainer;

    private void Update()
    {
        UpdateElement();
    }

    public void Upgrade()
    {
        PlayerUpgrades playerUpgrades = dataContainer.playerUpgrades[(int)upgrade];

        if(dataContainer.coins >= playerUpgrades.costToUpgrade && playerUpgrades.maxLevel > playerUpgrades.level)
        {
            dataContainer.coins -= playerUpgrades.costToUpgrade;
            playerUpgrades.level += 1;
            UpdateElement();
        }
    }

    void UpdateElement()
    {
        PlayerUpgrades playerUpgrades = dataContainer.playerUpgrades[(int)upgrade];
       // upgradeName.text = upgrade.ToString();
        level.text = "Level: " + playerUpgrades.level.ToString();
        price.text = "Price: " + playerUpgrades.costToUpgrade.ToString();
    }
}
