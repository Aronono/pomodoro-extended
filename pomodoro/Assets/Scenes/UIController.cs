using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private UIModeResolver _modeResolver;

    private void Awake()
    {
        _modeResolver = FindObjectOfType<UIModeResolver>();
    }
    public void ToggleUIMode()
    {
        _modeResolver.ResolveMode();
    }
}
