using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SoundSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    // ������������ �� ������� GetDataEvent � OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        // ��������� ���������� �� ������
        if (YandexGame.SDKEnabled == true)
        {
            // ���� ����������, �� ��������� ��� �����
            GetData();

            // ���� ������ ��� �� �����������, �� ����� �� ����������� � ������ Start,
            // �� �� ���������� ��� ������ ������� GetDataEvent, ����� ��������� �������
        }
    }

    // ��� �����, ������� ����� ����������� � ������
    public void GetData()
    {
        if (YandexGame.savesData.isSoundOn == 1)
        {
            AudioSource[] components = AudioSource.FindObjectsOfType<AudioSource>();
            foreach (AudioSource aud in components)
                aud.enabled = true;
        }
        else
        {
            AudioSource[] components = AudioSource.FindObjectsOfType<AudioSource>();
            foreach (AudioSource aud in components)
                aud.enabled = false;
        }
    }

}
