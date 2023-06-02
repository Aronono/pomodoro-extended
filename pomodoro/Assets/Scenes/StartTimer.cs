using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;

public class StartTimer : MonoBehaviour
{
    public PresetManager presetManager;
    public int work_time;
    public int rest_time;
    public int long_rest_time;
    public TMP_Text textTimer;
    public float timeStart;
    public int work_periods;
    public Image timerBar;
    public float timeMax;

    public AudioClip cycleEndSnd;   // ���� ��� ��������� �����
    public AudioSource audiosrc;    // ��������� �����
    
    public int money;               // ���������� �����   
    public TMP_Text moneyText;      // ����������� �����

    public StartPauseChange changer; //"�������������" ������ ����� � ������

    bool timer_running = false;
    bool break_time = false;
    bool big_break_time = false;

    void Awake()                    // ��� �� ����������� ����� Start(),
                                    // �������������� ������� � ����� ������ ������ �� ������ ��������
    {
        timerBar = GameObject.Find("TimerBar").GetComponent<Image>();
        timerBar.fillAmount = 0;
    }

    void Start()
    {
        timeStart = presetManager.activePreset.WorkTime;
        timeMax = presetManager.activePreset.WorkTime;
        textTimer.text = presetManager.activePreset.WorkTime.ToString();
        work_periods = presetManager.activePreset.WorkCycles;

        if(PlayerPrefs.HasKey("money"))
        {
            money = PlayerPrefs.GetInt("money");
            moneyText.text = money.ToString();
        }
    }

    void Update()
    {
        textTimer.text = Mathf.Round(timeStart).ToString();
        if (timer_running)
        {
            timeStart -= Time.deltaTime;
            timerBar.fillAmount = 1.0f - (timeStart * 100 / timeMax) / 100;
        }

        // ������ ��������
        if (timeStart <= 0 && break_time == false)
        {
            changer.StartIcon();
            timer_running = false;;
            break_time = true;
            work_periods--;
            timeStart = presetManager.activePreset.BreakTime;
            timeMax = presetManager.activePreset.BreakTime;

            money++;                                // ���� ������
            audiosrc.PlayOneShot(cycleEndSnd);      // ���� ������
            moneyText.text = money.ToString();      // ��������� ����� ��� ��������� ������
            PlayerPrefs.SetInt("money", money);
        }

        // ������������� �������� �����
        if (timeStart <= 0 && break_time == true)
        {
            changer.StartIcon();
            timer_running = false;;
            break_time = false;
            timeStart = presetManager.activePreset.WorkTime;
            timeMax = presetManager.activePreset.WorkTime;
        }

        // ���������� ����, ������� �������
        if (work_periods == 0)
        {
            changer.StartIcon();
            timer_running = false;;
            work_periods = 3;
            big_break_time = true;
            timeStart = presetManager.activePreset.BigBreakTime;
            timeMax = presetManager.activePreset.BigBreakTime;
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
            timeMax = presetManager.activePreset.WorkTime;
        }
        else if (break_time == true)
        {
            timeStart = presetManager.activePreset.BreakTime;
            timeMax = presetManager.activePreset.BreakTime;
        }
        else if (big_break_time == true)
        {
            timeStart = presetManager.activePreset.BigBreakTime;
            timeMax = presetManager.activePreset.BigBreakTime;
        }
        timerBar.fillAmount = 0;
        textTimer.text = Mathf.Round(timeStart).ToString();
    }

    public void TimerSkip()
    {
        break_time = !break_time;
        if (break_time == true)
        {
            timer_running = false;
            timeStart = presetManager.activePreset.BreakTime;
            timeMax = presetManager.activePreset.BreakTime;
            work_periods--;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        else if (break_time == false || big_break_time == false)
        {
            timer_running = false;;
            timeStart = presetManager.activePreset.WorkTime;
            timeMax = presetManager.activePreset.WorkTime;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        else if (big_break_time == true) 
        {
            timer_running = false;;
            timeStart = presetManager.activePreset.BigBreakTime;
            timeMax = presetManager.activePreset.BigBreakTime;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        timerBar.fillAmount = 0;
    }

    public void SetPreset(int id)
    {
        TimerStop();
        textTimer.text = presetManager.activePreset.WorkTime.ToString();
    }
}