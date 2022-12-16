using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemPickUpObject : MonoBehaviour, iPickUpObject
{
    [SerializeField] int amount;

    public void onPickup(CharacterStats character)
    {
        character.level.addExperience(amount);
    }
}
