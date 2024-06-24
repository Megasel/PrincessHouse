using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SoundSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то запускаем Ваш метод
            GetData();

            // Если плагин еще не прогрузился, то метод не запуститься в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

    // Ваш метод, который будет запускаться в старте
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
