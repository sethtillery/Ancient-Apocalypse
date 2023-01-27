using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : WeaponBase
{
    public HeroKnight hero;
    [SerializeField] CharacterStats character;
    [SerializeField] Collider2D[] colliders;
    public GameObject shuriken;
    [SerializeField] Vector2 shurikenSize = new Vector2(2f, 2f);
    [SerializeField] Transform rotationCenter;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        rotationCenter = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamage();
        for (int i = 0; i < colliders.Length; i++)
        {
            // Debug.Log(colliders[i].gameObject.name);
            Damageable e = colliders[i].GetComponent<Damageable>();
            if (e != null)
            {
                    PostDamage(weaponStats.damage, colliders[i].transform.position);
                    e.TakeDamage(damage);
            }
        }
    }

    [SerializeField] float rotationRadius = 1f, angularSpeed = 4f;
    float posX, posY, angle = 0f;
    private void FixedUpdate()
    {
        if (weaponStats.timeToAttack == 1.5)
            angularSpeed = 2;
        else if (weaponStats.timeToAttack == 2.5)
            angularSpeed = 3;

        gameObject.transform.Rotate(0f, 0f, -10f * Time.deltaTime * 10f);
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = (rotationCenter.position.y + 0.8f) + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX, posY);
        angle += Time.deltaTime * angularSpeed;

        if (angle >= 360) angle = 0f;
    }


    public override void Attack()
    {
        colliders = Physics2D.OverlapBoxAll(shuriken.transform.position, shurikenSize, 0f);
        ApplyDamage(colliders);
    }
}
