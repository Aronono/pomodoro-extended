using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Это скрипт для переключения между сценами
// Все sceneid можно найти в File -> Build Settings
// Для использования нужно добавить этот скрипт на Canvas сцены,
// затем выбрать Canvas в качестве объекта в событии (On Click() в кнопке, например)
// И уже там (в этой условной кнопке) выбрать функцию которая написана ниже
// Надеюсь это было полезно и я писал это не зря

public class SceneTransition : MonoBehaviour
{
    
    public void changeScene(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }
}
