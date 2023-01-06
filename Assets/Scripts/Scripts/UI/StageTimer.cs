using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float time;
    public TimerUI timerUI;

    private void Start()
    {
        timerUI = GameObject.Find("TimerNumbers").GetComponent<TimerUI>();
        Time.timeScale = 1;
    }

    private void Update()
    {
        time += Time.deltaTime;
        timerUI.UpdateTime(time);
    }
}
