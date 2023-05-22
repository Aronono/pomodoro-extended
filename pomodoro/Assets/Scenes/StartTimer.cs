using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class StartTimer : MonoBehaviour
{
    public PresetManager presetManager;
    public TMP_Text textTimer;
    public float timeStart;
    public int work_periods;

    public AudioClip cycleEndSnd;  // ���� ��� ��������� �����
    public AudioSource audiosrc;   // ��������� �����

    bool timer_running = false;
    bool break_time = false;
    bool big_break_time = false;

    void Awake()
    {

    }

    public void UpdateVals()
    {
        if(presetManager.activePreset == null)
        {
            timeStart = 0;
            work_periods = 0;
            textTimer.text = timeStart.ToString();
        } else
        {
            timeStart = presetManager.activePreset.WorkTime;
            work_periods = presetManager.activePreset.WorkCycles;
            textTimer.text = presetManager.activePreset.WorkTime.ToString();
        }
    }

    void Start()
    {
        UpdateVals();
    }

    void Update()
    {
        textTimer.text = Mathf.Round(timeStart).ToString();
        if (timer_running)
        {
            timeStart -= Time.deltaTime;
        }

        // ������ ��������
        if (timeStart <= 0 && break_time == false)
        {
            timer_running = false;;
            break_time = true;
            work_periods--;
            timeStart = presetManager.activePreset.BreakTime;
            audiosrc.PlayOneShot(cycleEndSnd);  // ���� ������ 
        }

        // ������������� �������� �����
        if (timeStart <= 0 && break_time == true)
        {
            timer_running = false;;
            break_time = false;
            timeStart = presetManager.activePreset.WorkTime;
        }

        // ���������� ����, ������� �������
        if (work_periods == 0)
        {
            timer_running = false;;
            work_periods = 3;
            big_break_time = true;
            timeStart = presetManager.activePreset.BigBreakTime;
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
            timeStart = presetManager.activePreset.WorkTime;
        }
        else if (break_time == true)
        {
            timeStart = presetManager.activePreset.BreakTime;
        }
        else if (big_break_time == true)
        {
            timeStart = presetManager.activePreset.BigBreakTime;
        }
        textTimer.text = Mathf.Round(timeStart).ToString();
    }

    public void TimerSkip()
    {
        break_time = !break_time;
        if (break_time == true)
        {
            timer_running = false;;
            timeStart = presetManager.activePreset.BreakTime;
            work_periods--;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        else if (break_time == false || big_break_time == false)
        {
            timer_running = false;;
            timeStart = presetManager.activePreset.WorkTime;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        else if (big_break_time == true) 
        {
            timer_running = false;;
            timeStart = presetManager.activePreset.BigBreakTime;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
    }
}

