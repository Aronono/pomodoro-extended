using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������ ��� ���������� �������
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
            isOn = PlayerPrefs.GetInt("soundsetting");          // ���������� PP 
        }
    }

    // ��� �������� �� ����� �������� ��������� �����������
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

    // ��������� ����� ��� ������������ �����
    public void PlayAudio(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    public void OnOffAudio()    // ������������ �� ��� � ����
    {
        if (isOn == 1)
        {
            toggle.isOn = false;
            audioState.GetComponent<Image>().sprite = audioOff;
            AudioListener.volume = 0;
            isOn = 0;
        }

        else if (isOn == 0)     // ������������ �� ���� �� ���
        {
            toggle.isOn = true;
            audioState.GetComponent<Image>().sprite = audioOn;
            AudioListener.volume = 1;
            isOn = 1;
        }

        PlayerPrefs.SetInt("soundsetting", isOn);       // ���������� ����� �������� � PP
    }
}
