using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] float timeToCompleteLevel;
    [SerializeField] GameWinPanel levelCompletePanel;

    StageTimer stageTimer;
    

    private void Awake()
    {
        stageTimer = GetComponent<StageTimer>();
        levelCompletePanel = FindObjectOfType<GameWinPanel>(true);
    }

    public void Update()
    {
        if(stageTimer.time > timeToCompleteLevel)
        {
            Time.timeScale = 0;
            levelCompletePanel.gameObject.SetActive(true);
        }
    }
}
