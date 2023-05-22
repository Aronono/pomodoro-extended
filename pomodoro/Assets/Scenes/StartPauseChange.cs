using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPauseChange : MonoBehaviour
{
    public static StartPauseChange Instance;

    [SerializeField]
    private GameObject Start;
    [SerializeField]
    private GameObject Pause;

    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (Instance == null)
            Instance = this;
    }

    public void StartIcon()
    {
        Start.SetActive(true);
        Pause.SetActive(false);
    }

    public void PauseIcon()
    {
        Start.SetActive(false);
        Pause.SetActive(true);
    }
}
