using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passiveItems : MonoBehaviour
{
    [SerializeField] List<Item> items;
    CharacterStats character;
   // [SerializeField] Item armorTest;

    private void Awake()
    {
        character = GetComponent<CharacterStats>();
    }
    private void Start()
    {
       // Equip(armorTest);
    }

    public void Equip(Item itemToEquip)
    {
        if(items == null)
        {
            items = new List<Item>();
        }
        items.Add(itemToEquip);
        itemToEquip.Equip(character);
    }

    public void unEquip(Item itemToUnEquip)
    {
        
    }
}
