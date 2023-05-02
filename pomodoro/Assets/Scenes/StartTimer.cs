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
    public float elapsedTime = 0;

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
        timeStart = preset.InitialTime - elapsedTime;
    }

    public void TimerStop()
    {
        timer_running = false;
        timeStart += elapsedTime;
        textTimer.text = Mathf.Round(timeStart).ToString();
        elapsedTime = 0;
    }

    public void TimerSkip()
    {
        break_time = !break_time;
        if (break_time == true)
        {
            timeStart = preset.BreakTime;
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

        List<string> optList = new();
        foreach(TimerPreset tp in timerPresets)
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