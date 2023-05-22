using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetManager : MonoBehaviour
{
    public List<TimerPreset> availablePresets = new();
    public TimerPreset activePreset = new();

    private void Awake()
    {
        availablePresets.Add(activePreset);
    }

    public void PresetAdd()
    {

    }

    public void PresetEdit()
    {

    }

    public void PresetDelete()
    {

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

        public float WorkCycles { get; set; }
        private float Def_WorkCycles { get; } = 4;

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

        public string Optionify()
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
