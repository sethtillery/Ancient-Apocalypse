using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Damageable
{
    Transform targetDestination;
    GameObject targetGameObject;
    [SerializeField] float speed;
    CharacterStats targetCharacter;
    Animator player_anim;

    Rigidbody2D r2d;

    [SerializeField] int hp = 1;
    [SerializeField] Animator enemyAnim;
    [SerializeField] int damage = 2;
    //[SerializeField] int expReward = 1;

    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        targetCharacter = targetGameObject.GetComponent<CharacterStats>();
        player_anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    public void setTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }

    private void FixedUpdate()
    {
        if (targetCharacter.isAlive)
        {
            Vector3 direction = (targetDestination.position - transform.position).normalized;
            r2d.velocity = direction * speed;

            if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if(targetCharacter.isAlive)
        {
            targetCharacter = targetGameObject.GetComponent<CharacterStats>();
            targetCharacter.TakeDamage(damage);
            player_anim.Play("Hurt");
        }

    }

    public void TakeDamage(int damage) 
    {
        hp -= damage;

        if (hp < 1)
        {
            enemyAnim.Play("Death");
            GetComponent<DropOnDestroy>().CheckDrop();
            // targetCharacter.GetComponent<Level>().addExperience(expReward);
            Destroy(gameObject, .5f);
        }
        else
            enemyAnim.Play("TakeHit");
    }
}
