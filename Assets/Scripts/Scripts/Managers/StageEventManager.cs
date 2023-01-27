using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemiesSpawnManager enemiesManager;

    StageTimer stageTimer;
    int eventIndex;

    private void Awake()
    {
        stageTimer = GetComponent<StageTimer>();
    }

    private void Update()
    {
        if(eventIndex >= stageData.stageEvents.Count) { return; }

        if(stageTimer.time >  stageData.stageEvents[eventIndex].time)
        {
            Debug.Log(stageData.stageEvents[eventIndex].message);

            for(int i = 0; i < stageData.stageEvents[eventIndex].count; i++)
            {
                //enemiesManager.spawnEnemy();
            }

            eventIndex += 1;
        }
    }
}
