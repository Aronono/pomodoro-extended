using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Скрипт для управления звуками
public class AudioManager : MonoBehaviour
{
    public Sprite audioOn;
    public Sprite audioOff;
    public GameObject audioState;

    public Toggle toggle;
    public int isOn;

    public AudioSource audio;

    void Awake()
    {
        if (PlayerPrefs.HasKey("soundsetting"))
        {
            isOn = PlayerPrefs.GetInt("soundsetting");          // Подгружаем PP 
        }
    }

    // При переходе на экран настроек настройка подгрузится
    void Start()
    {
        if (isOn == 1)
        {
            toggle.isOn = true;
            audioState.GetComponent<Image>().sprite = audioOn;
            AudioListener.volume = 1;
        }

        else
        {
            AudioListener.volume = 0;
            toggle.isOn = false;
            audioState.GetComponent<Image>().sprite = audioOff;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Публичный метод для проигрывания звука
    public void PlayAudio(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    public void OnOffAudio()    // Переключение из вкл в выкл
    {
        if (isOn == 1)
        {
            toggle.isOn = false;
            audioState.GetComponent<Image>().sprite = audioOff;
            AudioListener.volume = 0;
            isOn = 0;
        }

        else if (isOn == 0)     // Переключение из выкл во вкл
        {
            toggle.isOn = true;
            audioState.GetComponent<Image>().sprite = audioOn;
            AudioListener.volume = 1;
            isOn = 1;
        }

        PlayerPrefs.SetInt("soundsetting", isOn);       // Записываем новое значение в PP
    }
}
