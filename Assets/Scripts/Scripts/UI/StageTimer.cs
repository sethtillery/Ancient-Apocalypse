using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float time;
    TimerUI timerUI;

    private void Awake()
    {
        timerUI = FindObjectOfType<TimerUI>();
        Time.timeScale = 1;
    }

    private void Update()
    {
        time += Time.deltaTime;
        timerUI.UpdateTime(time);
    }
}
