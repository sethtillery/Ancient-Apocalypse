using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemPickUpObject : MonoBehaviour, iPickUpObject
{
    [SerializeField] int amount;

    public void onPickup(CharacterStats character)
    {
        if ((int)character.xpBonus == 0)
            character.level.addExperience(amount);
        else
            character.level.addExperience(amount * (int)character.xpBonus);
    }
}
