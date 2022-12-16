using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name;
    public int armor;

    public void Equip(CharacterStats character)
    {
        character.armor += armor;
    }

    public void unEquip(CharacterStats character)
    {
        character.armor -= armor;
    }
}
