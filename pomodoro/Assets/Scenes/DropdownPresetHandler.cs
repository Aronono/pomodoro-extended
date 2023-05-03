using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownPresetHandler : MonoBehaviour
{
    public StartTimer timer;
    public TMP_Dropdown presetSelector;

    public void ChangePreset()
    {
        timer.SetPreset(presetSelector.value);
    }

    public void InitSelector(List<string> optionList)
    {
        presetSelector.ClearOptions();
        presetSelector.AddOptions(optionList);
    }
}
