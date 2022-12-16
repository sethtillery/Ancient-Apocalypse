using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healPickUpObject : MonoBehaviour, iPickUpObject
{
    [SerializeField] int healAmount;

    public void onPickup(CharacterStats character)
    {
        character.Heal(healAmount);
    }
}
