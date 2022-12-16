using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStats c = collision.GetComponent<CharacterStats>();
        if(c != null)
        {
            GetComponent<iPickUpObject>().onPickup(c);
            Destroy(gameObject);
        }
    }
}
