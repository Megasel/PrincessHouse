using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ResetSaves : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
            
        
    }
}
