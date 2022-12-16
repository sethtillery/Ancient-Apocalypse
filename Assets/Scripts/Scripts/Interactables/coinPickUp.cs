using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickUp : MonoBehaviour, iPickUpObject
{
    [SerializeField] int count;

    public void onPickup(CharacterStats character)
    {
        character.coins.Add(count);
    }
}
