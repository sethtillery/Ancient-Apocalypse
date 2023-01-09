using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] float timeToCompleteLevel;
    [SerializeField] GameWinPanel levelCompletePanel;
    public int winLevel = 2;

    Level level;
    StageTimer stageTimer;
    

    private void Awake()
    {
        stageTimer = GetComponent<StageTimer>();
    }

    private void Start()
    {
        levelCompletePanel = FindObjectOfType<GameWinPanel>(true);
        level = GameObject.FindGameObjectWithTag("Player").GetComponent<Level>();
    }

    public void Update()
    {
        if(level.currentLevel == winLevel)
        {
            Time.timeScale = 0;
            levelCompletePanel.gameObject.SetActive(true);
        }
    }
}
