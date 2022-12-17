using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwKnife : WeaponBase
{
    HeroKnight playerMove;

    [SerializeField] GameObject knifePrefab;
    [SerializeField] float spread = 0.5f;

    private void Awake()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();
        playerMove.lastHorizontalVector = 0;
        playerMove.lastVerticalVector = 0;
    }

    public override void Attack()
    {

        for(int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject thrownKnife = Instantiate(knifePrefab);

            Vector3 newKnifePosition = transform.position;

            thrownKnife.transform.position = newKnifePosition;

            ThrowingDaggerProjectile throwingDaggerProjectile = thrownKnife.GetComponent<ThrowingDaggerProjectile>();
            throwingDaggerProjectile.damage = weaponStats.damage;
            if (playerMove.movement.x == 0 && playerMove.movement.y != 0)
            {
                if (weaponStats.numberOfAttacks > 1)
                {
                    newKnifePosition.x -= (spread * (weaponStats.numberOfAttacks - 1)) / 2; //calculates offset
                    newKnifePosition.x += i * spread; // spreads the knives
                    thrownKnife.transform.position = newKnifePosition;
                }
                thrownKnife.GetComponent<ThrowingDaggerProjectile>().setDirection(0f, playerMove.lastVerticalVector);
            }
            else
            {
                if (weaponStats.numberOfAttacks > 1)
                {
                    newKnifePosition.y -= (spread * (weaponStats.numberOfAttacks - 1)) / 2; //calculates offset
                    newKnifePosition.y += i * spread; // spreads the knives
                    thrownKnife.transform.position = newKnifePosition;
                }
                thrownKnife.GetComponent<ThrowingDaggerProjectile>().setDirection(playerMove.lastHorizontalVector, 0f);     
            }
        }

    }
}
