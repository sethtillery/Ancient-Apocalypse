using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    GameObject player;
    float flyingEyeSpawnTime = 2f;
    float mushroomSpawnTime = 5f;
    public int mushroomLeveltoSpawn;
    public bool mushroom = false;
    public bool eye = false;
    Level level;

    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
        level = GameObject.FindGameObjectWithTag("Player").GetComponent<Level>();
    }

    public void spawnEnemy()
    {
        Vector3 position = GenerateRandomPosition();

        player = GameObject.FindGameObjectWithTag("Player");
        position += player.transform.position;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().setTarget(player);
        newEnemy.transform.parent = transform;

        if (enemy.tag == "eye")
            spawnTimer = 2f;
        else if (enemy.tag == "Mushroom")
            spawnTimer = 5f;
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();
        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;

        if(UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }

        position.z = 0;
        
        return position;
    }

    private void Update()
    {
        if (level.currentLevel == 3)
            mushroom = true;

        spawnTimer -= Time.deltaTime;
        if(level.currentLevel >= mushroomLeveltoSpawn || mushroom || eye)
        {
            if(spawnTimer <= 0)
            {
                spawnEnemy();
            }
        }

    }
}
