using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ��� ������ ��� ������������ ����� �������
// ��� sceneid ����� ����� � File -> Build Settings
// ��� ������������� ����� �������� ���� ������ �� Canvas �����,
// ����� ������� Canvas � �������� ������� � ������� (On Click() � ������, ��������)
// � ��� ��� (� ���� �������� ������) ������� ������� ������� �������� ����
// ������� ��� ���� ������� � � ����� ��� �� ���

public class SceneTransition : MonoBehaviour
{
    
    public void changeScene(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }
}
