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

    // Start is called before the first frame update
    void Start()
    {
        if (AudioListener.volume == 1)
        {
            toggle.isOn = true;
            audioState.GetComponent<Image>().sprite = audioOn;
            isOn = 1;
        }
        else
        {
            toggle.isOn = false;
            audioState.GetComponent<Image>().sprite = audioOff;
            isOn = 0;
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

    public void OnOffAudio()
    {
        if (isOn == 1)
        {
            AudioListener.volume = 0;
            isOn = 0;
            audioState.GetComponent<Image>().sprite = audioOff;
        }
        else
        {
            AudioListener.volume = 1;
            isOn = 1;
            audioState.GetComponent<Image>().sprite = audioOn;
        }
    }
}
