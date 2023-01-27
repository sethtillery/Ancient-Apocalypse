using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyList;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float eyeSpawnTimer;
    [SerializeField] float mushroomSpawnTimer;
    [SerializeField] float goblinSpawnTimer;
    
    GameObject player;
    float flyingEyeSpawnTime = 2f;

    float mushroomSpawnTime = 5f;
    [SerializeField] int mushroomLevelToSpawn = 3;

    float goblinTimeToSpawn = 10;
    [SerializeField] int goblinLevelToSpawn = 7;

    [SerializeField] bool eye = true;
    [SerializeField] bool mushroom = false;
    [SerializeField] bool goblin = false;
    Level level;

    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
        level = GameObject.FindGameObjectWithTag("Player").GetComponent<Level>();
    }

    public void spawnEnemy(GameObject enemyToSpawn)
    {
        Vector3 position = GenerateRandomPosition();

        player = GameObject.FindGameObjectWithTag("Player");
        position += player.transform.position;


        GameObject newEnemy = Instantiate(enemyToSpawn);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().setTarget(player);
        newEnemy.transform.parent = transform;

        if (enemyToSpawn.tag == "eye")
            eyeSpawnTimer = flyingEyeSpawnTime;
        else if (enemyToSpawn.tag == "Mushroom")
            mushroomSpawnTimer = mushroomSpawnTime;
        else if (enemyToSpawn.tag == "Goblin")
            goblinSpawnTimer = goblinTimeToSpawn;
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
        if (level.currentLevel == mushroomLevelToSpawn)
            mushroom = true;
        else if (level.currentLevel == goblinLevelToSpawn)
            goblin = true;

        eyeSpawnTimer -= Time.deltaTime;
        mushroomSpawnTimer -= Time.deltaTime;
        goblinSpawnTimer -= Time.deltaTime;

        if(eye)
            if (eyeSpawnTimer <= 0) spawnEnemy(enemyList[0]);
        else if (mushroom)
            if (mushroomSpawnTimer <= 0) spawnEnemy(enemyList[1]);
        else if(goblin)
            if (goblinSpawnTimer <= 0) spawnEnemy(enemyList[2]);

    }
}
