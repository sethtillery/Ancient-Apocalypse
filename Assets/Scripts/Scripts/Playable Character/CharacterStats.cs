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

    public float damageBonus;
    public float armorBonus;
    public float weaponAttackBonus;
    public float attackRadiusBonus;
    public float fasterProjectilesBonus;
    public float speedBonus;
    public float xpBonus;

    [SerializeField] HealthBar hpBar;
    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;

    [SerializeField] DataContainer dataContainer;

    HeroKnight heroKnight;

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
        ApplyPermanentUpgrades();
    }

    private void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        hpBar.setState(currentHp, maxHP);
        currentHp = maxHP;
        heroKnight = GetComponent<HeroKnight>();
    }

    private void ApplyPermanentUpgrades()
    {
        int damageUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPermanentUpgrades.Damage);

        if(damageUpgradeLevel != 0)
            damageBonus = 1f + 0.25f * damageUpgradeLevel;


        int hpUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPermanentUpgrades.Hp);

        maxHP += maxHP / 10 * hpUpgradeLevel;

        currentHp = maxHP;

        int armorUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPermanentUpgrades.Armor);

        armorBonus = armorUpgradeLevel;

        int fasterWeaponsUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPermanentUpgrades.FasterWeapons);

        if(fasterWeaponsUpgradeLevel != 0)
            weaponAttackBonus = 1f + 0.025f * fasterWeaponsUpgradeLevel;

        int biggerAttackRadiusUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPermanentUpgrades.BiggerAttackRadius);

        if(biggerAttackRadiusUpgradeLevel != 0) 
            attackRadiusBonus = 1f + 0.10f * biggerAttackRadiusUpgradeLevel;

        int fasterProjectilesUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPermanentUpgrades.FasterProjectiles);

        if(fasterProjectilesUpgradeLevel != 0)
            fasterProjectilesBonus = 1f + 0.05f * fasterProjectilesUpgradeLevel;

        int fasterMovementUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPermanentUpgrades.FasterMovement);

        if(fasterMovementUpgradeLevel != 0)
            speedBonus = 1f + 0.25f * fasterMovementUpgradeLevel;

        int moreXPUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPermanentUpgrades.moreXP);

        if(moreXPUpgradeLevel != 0)
            xpBonus = 1f + 0.5f * moreXPUpgradeLevel;
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
        }
        hpBar.setState(currentHp, maxHP);
    }

    private void applyArmor(ref int damage)
    {
        armor += (int)armorBonus;
        damage -= armor;
        if (damage < 0)
            damage = 0;
    }

    private void Update()
    {
        if (pauseGame)
            timer += Time.deltaTime;
        
        if (pauseGame && timer > 1)
        {
            GetComponent<CharacterGameOver>().GameOver();
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
