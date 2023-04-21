using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    public float timeStart = 60;
    public Text textTimer;
    public float elapsedTime = 0;

    bool timer_running = false;
    bool break_time = false;

    void Start()
    {
        textTimer.text = timeStart.ToString();
    }

    void Update()
    {
        if (timer_running)
        {
            timeStart -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        if (timeStart < 0)
        {
            timeStart = 0;
        }
    }


    public void TimerStart()
    {
        timer_running = !timer_running;
    }

    public void TimerStop()
    {
        timer_running = false;
        timeStart = timeStart + elapsedTime;
        textTimer.text = Mathf.Round(timeStart).ToString();
        elapsedTime = 0;
        
    }

    public void TimerSkip()
    {
        break_time = !break_time;
        if (break_time == true)
        {
            TimerStop();
            timeStart = 30;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        else
        {
            TimerStop();
            timeStart = 60;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        elapsedTime = 0;
    }
}
