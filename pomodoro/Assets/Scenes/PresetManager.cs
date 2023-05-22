using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PresetManager : MonoBehaviour
{
    public StartTimer timerScript;

    public TMP_Dropdown presetDropdown;

    public List<TimerPreset> availablePresets = new();
    private List<String> optionList = new();
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
        optionList.Add(activePreset.ToString());
        presetDropdown.AddOptions(optionList);

        PresetSet();
    }

    private void UpdateOptions()
    {
        presetDropdown.ClearOptions();
        presetDropdown.AddOptions(optionList);
    }

    public void PresetAdd()
    {
        TimerPreset temp = new TimerPreset((float)Convert.ToDouble(InputWorkTime.text), (float)Convert.ToDouble(InputBreakTime.text),
            (float)Convert.ToDouble(InputBigBreakTime.text), Convert.ToInt32(InputWorkCycles.text));

        bool unique = true;
        foreach (TimerPreset preset in availablePresets) {
            if(preset.GetHashCode() == temp.GetHashCode())
            {
                unique = false;
            }
        }

        if(unique)
        {
            availablePresets.Add(temp);
            optionList.Add(temp.ToString());

            UpdateOptions();
        }

        activePreset = availablePresets[availablePresets.Count - 1];
        presetDropdown.value = presetDropdown.options.Count - 1;

        timerScript.UpdateVals();
    }

    public void PresetEdit()
    {
        timerScript.TimerStop();

        activePreset.WorkTime = (float)Convert.ToDouble(InputWorkTime.text);
        activePreset.BreakTime = (float)Convert.ToDouble(InputBreakTime.text);
        activePreset.BigBreakTime = (float)Convert.ToDouble(InputBigBreakTime.text);
        activePreset.WorkCycles = Convert.ToInt32(InputWorkCycles.text);

        int currentOption = presetDropdown.value;
        optionList[presetDropdown.value] = activePreset.ToString();
        UpdateOptions();
        presetDropdown.value = currentOption;

        timerScript.UpdateVals();
    }

    public void PresetDelete()
    {

    }

    public void PresetSet()
    {
        timerScript.TimerStop();

        activePreset = availablePresets[presetDropdown.value];

        InputWorkTime.text = activePreset.WorkTime.ToString();
        InputBreakTime.text = activePreset.BreakTime.ToString();
        InputBigBreakTime.text = activePreset.BigBreakTime.ToString();
        InputWorkCycles.text = activePreset.WorkCycles.ToString();

        timerScript.UpdateVals();
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
