using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwKnife : WeaponBase
{
    HeroKnight playerMove;

    [SerializeField] GameObject knifePrefab;

    private void Awake()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();
        playerMove.lastHorizontalVector = 0;
        playerMove.lastVerticalVector = 0;
    }

    public override void Attack()
    {
        GameObject thrownKnife = Instantiate(knifePrefab);
        thrownKnife.transform.position = playerMove.transform.position;
        ThrowingDaggerProjectile throwingDaggerProjectile = thrownKnife.GetComponent<ThrowingDaggerProjectile>();
        throwingDaggerProjectile.damage = weaponStats.damage;
        if (playerMove.movement.x == 0 && playerMove.movement.y != 0)
            thrownKnife.GetComponent<ThrowingDaggerProjectile>().setDirection(0f, playerMove.lastVerticalVector);
        else
            thrownKnife.GetComponent<ThrowingDaggerProjectile>().setDirection(playerMove.lastHorizontalVector, 0f);     
    }
}
