using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Скрипт для управления звуками
public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Публичный метод для проигрывания звука
    public void PlayAudio(AudioClip clip)
    {
        GetComponent<AudioSource> ().PlayOneShot (clip);
    }
}
