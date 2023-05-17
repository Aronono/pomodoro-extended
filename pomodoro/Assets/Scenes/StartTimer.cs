using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartTimer : MonoBehaviour
{
    private List<TimerPreset> timerPresets = new();
    public DropdownPresetHandler PresetDropdown;
    public TimerPreset preset;
    public Text textTimer;
    public float timeStart;
    public int work_periods = 3;

    public AudioClip cycleEndSnd;  // Звук при окончании цикла
    public AudioSource audiosrc;   // Аудиосорс нужен

    bool timer_running = false;
    bool break_time = false;

    void Awake() //Это всё выполняется перед Start(), инициализирует пресеты и сразу ставит первый из списка активным
    {
        InitPresets();
        preset = timerPresets[0];
    }

    void Start()
    {
        textTimer.text = preset.InitialTime.ToString();
    }

    void Update()
    {
        if (timer_running)
        {
            timeStart -= Time.deltaTime;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }

        // Начало перерыва
        if (timeStart <= 0 && break_time == false)
        {
            break_time = true;
            work_periods--;
            timeStart = preset.BreakTime;
        }

        // Возобновление рабочего цикла
        if (timeStart <= 0 && break_time == true)
        {
            break_time = false;
            timeStart = preset.InitialTime;
        }

        // Закончился цикл, большой перерыв
        if (work_periods == 0)                      
        {
            timer_running = false               // Остановлен таймер
            break_time = true;
            timeStart = preset.BigBreakTime;
            work_periods = 3;
            audiosrc.PlayOneShot(cycleEndSnd);  // Звук победы
        }
    }


    public void TimerStart()
    {
        timer_running = !timer_running;
        if (break_time == false)
        {
            timeStart = preset.InitialTime;
        }
    }

    public void TimerStop()
    {
        timer_running = false;
        if (break_time == false)
        {
            timeStart = preset.InitialTime;
        }
        if (break_time == true)
        {
            timeStart = preset.BreakTime;
        }
        textTimer.text = Mathf.Round(timeStart).ToString();
    }

    public void TimerSkip()
    {
        break_time = !break_time;
        if (break_time == true)
        {
            timeStart = preset.BreakTime;
            work_periods--;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        else
        {
            timeStart = preset.InitialTime;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
    }

    public void SetPreset(int id)
    {
        preset = timerPresets[id];
        TimerStop();
        textTimer.text = preset.InitialTime.ToString();
    }

    private void InitPresets()
    {
        timerPresets.Add(new TimerPreset());
        timerPresets.Add(new TimerPreset(20, 15, 20));
        timerPresets.Add(new TimerPreset(180, 30, 60));
        timerPresets.Add(new TimerPreset(5, 2, 3));

        List<string> optList = new();
        foreach (TimerPreset tp in timerPresets)
        {
            optList.Add(tp.Optionify());
        }

        PresetDropdown.InitSelector(optList);
    }
}

public class TimerPreset
{
    public float InitialTime { get; set; }
    private float Def_InitialTime { get; set; }

    public float BreakTime { get; set; }
    private float Def_BreakTime { get; set; }

    public float BigBreakTime { get; set; }
    private float Def_BigBreakTime { get; set; }

    public TimerPreset()
    {
        InitialTime = 60;
        BreakTime = 30;
        BigBreakTime = 45;

        UpdateDefaults();
    }

    public TimerPreset(float InitialTime, float BreakTime, float BigBreakTime)
    {
        this.InitialTime = InitialTime;
        this.BreakTime = BreakTime;
        this.BigBreakTime = BigBreakTime;

        UpdateDefaults();
    }

    public string Optionify() //Используется для динамического добавления имеющихся пресетов в Dropdown
    {
        return $"{(int)InitialTime}/{(int)BreakTime}/{(int)BigBreakTime}";
    }

    public void ToDefault()
    {
        InitialTime = Def_InitialTime;
        BreakTime = Def_BreakTime;
        BigBreakTime = Def_BigBreakTime;
    }

    private void UpdateDefaults()
    {
        Def_InitialTime = InitialTime;
        Def_BreakTime = BreakTime;
        Def_BigBreakTime = BigBreakTime;
    }
}