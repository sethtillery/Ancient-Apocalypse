using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passiveItems : MonoBehaviour
{
    [SerializeField] List<Item> items;
    CharacterStats character;

    private void Awake()
    {
        character = GetComponent<CharacterStats>();
    }
    private void Start()
    {
       
    }

    public void Equip(Item itemToEquip)
    {
        if(items == null)
        {
            items = new List<Item>();
        }
        Item newItemInstance = new Item();
        newItemInstance.Init(itemToEquip.Name);
        newItemInstance.stats.Sum(itemToEquip.stats);

        items.Add(newItemInstance);
        newItemInstance.Equip(character);
    }

    public void unEquip(Item itemToUnEquip)
    {
        
    }

    internal void UpgradeItem(UpgradeData upgradeData)
    {
        Item itemToUpgrade = items.Find(id => id.Name == upgradeData.item.name);
        itemToUpgrade.unEquip(character);
        itemToUpgrade.stats.Sum(upgradeData.stats);
        itemToUpgrade.Equip(character);
    }
}
