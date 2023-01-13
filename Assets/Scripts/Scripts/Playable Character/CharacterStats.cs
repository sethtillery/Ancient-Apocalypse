using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public int maxHP = 300;
    public int currentHp = 300;
    Animator playerAnim;
    public bool isAlive = true;
    bool pauseGame = false;
    float timer = 0f;
    public int armor = 0;

    [SerializeField] HealthBar hpBar;
    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    private void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        hpBar.setState(currentHp, maxHP);
        currentHp = maxHP;
    }
    public void TakeDamage(int damage)
    {
        if (!isAlive)
            return;

        applyArmor(ref damage);
        currentHp -= damage;


        if(currentHp <= 0)
        {
            isAlive = false;
            playerAnim.Play("Death");
            pauseGame = true;
            GetComponent<CharacterGameOver>().GameOver();
        }
        hpBar.setState(currentHp, maxHP);
    }

    private void applyArmor(ref int damage)
    {
        damage -= armor;
        if (damage < 0)
            damage = 0;
    }

    private void Update()
    {
        if (pauseGame)
            timer += Time.deltaTime;
        
        if (pauseGame && timer > 2)
        {
            hpBar.gameObject.SetActive(false);
            Time.timeScale = 0;
            pauseGame = false;
        }
    }

    public void Heal(int amount)
    {
        if(currentHp <= 0)
            return;
        
        currentHp += amount;

        if (currentHp > maxHP)
            currentHp = maxHP;

        hpBar.setState(currentHp, maxHP);
    }
    
}
