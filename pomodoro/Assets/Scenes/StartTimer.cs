using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PresetManager;

public class StartTimer : MonoBehaviour
{
    private List<TimerPreset> timerPresets = new();
    public TimerPreset preset;
    public TMP_Text textTimer;
    public float timeStart;
    public int work_periods = 3;

    public AudioClip cycleEndSnd;  // Звук при окончании цикла
    public AudioSource audiosrc;   // Аудиосорс нужен

    bool timer_running = false;
    bool break_time = false;
    bool big_break_time = false;

    void Awake()
    {
        InitPresets();
        preset = timerPresets[0];
    }

    void Start()
    {
        timeStart = preset.WorkTime;
        textTimer.text = preset.WorkTime.ToString();
    }

    void Update()
    {
        textTimer.text = Mathf.Round(timeStart).ToString();
        if (timer_running)
        {
            timeStart -= Time.deltaTime;
        }

        // Начало перерыва
        if (timeStart <= 0 && break_time == false)
        {
            timer_running = false;;
            break_time = true;
            work_periods--;
            timeStart = preset.BreakTime;
            audiosrc.PlayOneShot(cycleEndSnd);  // Звук победы 
        }

        // Возобновление рабочего цикла
        if (timeStart <= 0 && break_time == true)
        {
            timer_running = false;;
            break_time = false;
            timeStart = preset.WorkTime;
        }

        // Закончился цикл, большой перерыв
        if (work_periods == 0)
        {
            timer_running = false;;
            work_periods = 3;
            big_break_time = true;
            timeStart = preset.BigBreakTime;
        }
    }



    public void TimerStart()
    {
        timer_running = !timer_running;
    }

    public void TimerStop()
    {
        timer_running = false;
        if (break_time == false)
        {
            timeStart = preset.WorkTime;
        }
        else if (break_time == true)
        {
            timeStart = preset.BreakTime;
        }
        else if (big_break_time == true)
        {
            timeStart = preset.BigBreakTime;
        }
        textTimer.text = Mathf.Round(timeStart).ToString();
    }

    public void TimerSkip()
    {
        break_time = !break_time;
        if (break_time == true)
        {
            timer_running = false;;
            timeStart = preset.BreakTime;
            work_periods--;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        else if (break_time == false || big_break_time == false)
        {
            timer_running = false;;
            timeStart = preset.WorkTime;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        else if (big_break_time == true) 
        {
            timer_running = false;;
            timeStart = preset.BigBreakTime;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
    }

    public void SetPreset(int id)
    {
        preset = timerPresets[id];
        TimerStop();
        textTimer.text = preset.WorkTime.ToString();
    }

    private void InitPresets()
    {
        timerPresets.Add(new TimerPreset());
        timerPresets.Add(new TimerPreset(20, 15, 18, 3));
        timerPresets.Add(new TimerPreset(180, 30, 60, 4));
        timerPresets.Add(new TimerPreset(5, 2, 3, 4));

        List<string> optList = new();
        foreach (TimerPreset tp in timerPresets)
        {
            optList.Add(tp.Optionify());
        }
    }
}

