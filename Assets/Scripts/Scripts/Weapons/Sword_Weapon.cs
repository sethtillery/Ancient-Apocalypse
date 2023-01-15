using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Weapon : WeaponBase
{
    [SerializeField] private Animator hero_anim;
    public GameObject swordRight;
    public GameObject swordLeft;
    public HeroKnight hero;
    [SerializeField] Vector2 swordSize = new Vector2(2f, 2f);
    [SerializeField] CharacterStats character;
    Collider2D[] colliders;


    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        hero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();
        hero_anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        swordSize.x += character.attackRadiusBonus;
        swordSize.y += character.attackRadiusBonus;
    }

    // Update is called once per frame
  //  void Update()
   // {
        /**
        if(character.isAlive)
        {
            if (hero.movement.x > 0)
            {
                swordRight.gameObject.SetActive(true);
                swordLeft.gameObject.SetActive(false);
            }

            else if (hero.movement.x < 0)
            {
                swordRight.gameObject.SetActive(false);
                swordLeft.gameObject.SetActive(true);
            }
        }
        **/
  //  }

    private void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamage();
        for (int i = 0; i < colliders.Length; i++)
        {
           // Debug.Log(colliders[i].gameObject.name);
            Damageable e = colliders[i].GetComponent<Damageable>();
            if(e != null)
            {
                PostDamage(damage, colliders[i].transform.position);
                e.TakeDamage(damage);
            }
        }       
    }

    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        for(int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            if (hero.lastHorizontalVector > 0)
            {
                colliders = Physics2D.OverlapBoxAll(swordRight.transform.position, swordSize, 0f);
                ApplyDamage(colliders);
            }

            else
            {
               // Debug.Log("Swing left sword");
                colliders = Physics2D.OverlapBoxAll(swordLeft.transform.position, swordSize, 0f);
                ApplyDamage(colliders);
            }

            if (i == 0)
            {
                hero_anim.Play("Attack1");
                yield return new WaitForSeconds(.5f);
            }
            if (i > 0)
            {
                hero_anim.Play("Attack2");
                yield return new WaitForSeconds(.5f);
            }
        }

    }
}
