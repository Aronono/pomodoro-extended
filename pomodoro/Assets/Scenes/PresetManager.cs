using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using System;

public class PresetManager : MonoBehaviour
{
    public StartTimer timerScript;

    public List<TimerPreset> availablePresets = new();
    public TimerPreset activePreset = new();
    public TMP_Text PresetAnchor;

    public TMP_InputField InputWorkTime;
    public TMP_InputField InputBreakTime;
    public TMP_InputField InputBigBreakTime;
    public TMP_InputField InputWorkCycles;

    private List<TMP_InputField> inputFields = new();

    private void Awake()
    {
        inputFields.Add(InputWorkTime);
        inputFields.Add(InputBreakTime);
        inputFields.Add(InputBigBreakTime);
        inputFields.Add(InputWorkCycles);

        availablePresets.Add(activePreset);
        PresetSet();
    }

    private void UpdateOptions()
    {
    }

    public void PresetAdd()
    {

    }

    public void PresetEdit()
    {
        timerScript.TimerStop();

        activePreset.WorkTime = (float)Convert.ToDouble(InputWorkTime.text);
        activePreset.BreakTime = (float)Convert.ToDouble(InputBreakTime.text);
        activePreset.BigBreakTime = (float)Convert.ToDouble(InputBigBreakTime.text);
        activePreset.WorkCycles = Convert.ToInt32(InputWorkCycles.text);

    }

    public void PresetDelete()
    {
        timerScript.TimerStop();
        
    }

    public void PresetSet()
    {
        
    }

    public class TimerPreset
    {
        public float WorkTime { get; set; }
        private float Def_WorkTime { get; set; } = 60;

        public float BreakTime { get; set; }
        private float Def_BreakTime { get; set; } = 15;

        public float BigBreakTime { get; set; }
        private float Def_BigBreakTime { get; } = 45;

        public int WorkCycles { get; set; }
        private int Def_WorkCycles { get; } = 4;

        public TimerPreset()
        {
            WorkTime = Def_WorkTime;
            BreakTime = Def_BreakTime;
            BigBreakTime = Def_BigBreakTime;
            WorkCycles = Def_WorkCycles;
        }

        public TimerPreset(float InitialTime, float BreakTime, float BigBreakTime, int WorkCycles)
        {
            this.WorkTime = InitialTime;
            this.BreakTime = BreakTime;
            this.BigBreakTime = BigBreakTime;
            this.WorkCycles = WorkCycles;
        }

        public override string ToString()
        {
            return $"{(int)WorkTime}/{(int)BreakTime}/{(int)BigBreakTime}/{(int)WorkCycles}";
        }

        public void ToDefault()
        {
            WorkTime = Def_WorkTime;
            BreakTime = Def_BreakTime;
            BigBreakTime = Def_BigBreakTime;
            WorkCycles = Def_WorkCycles;
        }
    }
}