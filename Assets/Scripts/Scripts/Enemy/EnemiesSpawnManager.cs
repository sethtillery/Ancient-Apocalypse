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
    [SerializeField] float skeletonSpawnTimer;
    
    GameObject player;

    public float flyingEyeSpawnTime = 2f;
    public float mushroomSpawnTime = 5f;
    public float goblinTimeToSpawn = 10f;
    public float skeletonTimeToSpawn = 6f;

    public int mushroomLevelToSpawn = 3;
    public int goblinLevelToSpawn = 7;
    public int skeletonLevelToSpawn = 10;

    public bool eye = true;
    public bool mushroom = false;
    public bool goblin = false;
    public bool skeleton = false;
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
        else if (enemyToSpawn.tag == "Skeleton")
            skeletonSpawnTimer = skeletonTimeToSpawn;
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
        eyeSpawnTimer -= Time.deltaTime;
        mushroomSpawnTimer -= Time.deltaTime;
        goblinSpawnTimer -= Time.deltaTime;
        skeletonSpawnTimer -= Time.deltaTime;

        if (eye)
            if (eyeSpawnTimer <= 0) spawnEnemy(enemyList[0]);
        else if (mushroom)
            if (mushroomSpawnTimer <= 0) spawnEnemy(enemyList[1]);
        else if (goblin)
            if (goblinSpawnTimer <= 0) spawnEnemy(enemyList[2]);
        else if (skeleton)
            if (skeletonSpawnTimer <= 0) spawnEnemy(enemyList[3]);

    }
}
