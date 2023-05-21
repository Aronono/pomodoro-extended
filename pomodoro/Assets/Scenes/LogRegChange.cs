using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRegChange : MonoBehaviour
{
    public static LogRegChange Instance;

    [SerializeField]
    private GameObject Login;
    [SerializeField]
    private GameObject Registration;

    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (Instance == null)
            Instance = this;
    }

    public void OpenLogin()
    {
        Login.SetActive(true);
        Registration.SetActive(false);
    }

    public void OpenRegistration()
    {
        Login.SetActive(false);
        Registration.SetActive(true);
    }
}
