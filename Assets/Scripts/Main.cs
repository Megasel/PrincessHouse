// dnSpy decompiler from Assembly-CSharp.dll class: Main
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using YG;

public class Main : MonoBehaviour
{
	[SerializeField] AudioListener audioListener;
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
        Debug.Log(YandexGame.savesData.isSoundOn);
        if (YandexGame.savesData.isSoundOn == 1)
        {
            this.Sound_on.SetActive(true);
            this.Sound_off.SetActive(false);

        }


        else
        {
            this.Sound_on.SetActive(false);
            this.Sound_off.SetActive(true);
        }
        base.StartCoroutine(this.Start_Action());
        Time.timeScale = 1;
    }


	private IEnumerator Start_Action()
	{
		yield return new WaitForSeconds(2.1f);
		yield break;
	}


	public void Play_Btn()
	{
		UnityEngine.Debug.Log("Play");
		this.Main_Object_Pop_up.SetActive(false);

		iTween.MoveTo(this._Camera, iTween.Hash(new object[]
		{
			"x",
			9.73f,
			"y",
			0f,
			"time",
			1.5,
			"eastype",
			iTween.EaseType.linear,
			"islocal",
			true
		}));
		base.Invoke("Selection_All_Btn_Active_f", 0.5f);
	}

	private void Selection_All_Btn_Active_f()
	{
		this.Selection_All_Btn.SetActive(true);
		for (int i = 0; i < this.leaves.Length; i++)
		{
			iTween.ScaleTo(this.leaves[i], iTween.Hash(new object[]
			{
				"x",
				1f,
				"y",
				1f,
				"time",
				7.5,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
		}
	}

	public void Rate_Us_Btn()
	{
		UnityEngine.Debug.Log("Rate_Us_Btn");
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.hmg.housecleaning.princesscastle.cleaninggames");
	}

	public void More_Btn()
	{
		UnityEngine.Debug.Log("More_Btn");
		Application.OpenURL("https://play.google.com/store/apps/developer?id=Happy+Melon+Games");
	}

	public void Privacy_Btn()
	{
		UnityEngine.Debug.Log("More_Btn");
		Application.OpenURL("https://happymelongamesprivacypolicy.blogspot.com/2019/01/our-policy-is-designed-to-help-you.html");
	}

	public void No_Ads_Show_Pop_Up()
	{
		UnityEngine.Debug.Log("No_Ads_Btn");
		this.Exit_Panel_Pop_Up.SetActive(false);
		this.All_Btn.SetActive(false);
	}

	public void No_Ads_No_Btn()
	{
		UnityEngine.Debug.Log("No");
		this.Exit_Panel_Pop_Up.SetActive(true);
		this.All_Btn.SetActive(true);
	}

	public void rate_us_later_Btn()
	{
		UnityEngine.Debug.Log("No");
		this.Exit_Panel_Pop_Up.SetActive(true);
		this.All_Btn.SetActive(true);
	}

	public void cancel_btn_unlock_all()
	{
		UnityEngine.Debug.Log("No");
		this.unlock_all_pop_up.SetActive(false);
		this.Exit_Panel_Pop_Up.SetActive(true);
		this.All_Btn.SetActive(true);
	}

	public void remove_2_btn_unlock_all()
	{
		UnityEngine.Debug.Log("No");
	}
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            Debug.Log(YandexGame.savesData.isSoundOn);
    }
    public void Sound_On_Btn()
	{
		YandexGame.savesData.isSoundOn = 0;
		YandexGame.SaveProgress();
        Debug.Log(YandexGame.savesData.isSoundOn);
        UnityEngine.Debug.Log("Sound_On_Btn");
		this.Sound_on.SetActive(false);
		this.Sound_off.SetActive(true);
        AudioSource[] components = AudioSource.FindObjectsOfType<AudioSource>();
        foreach (AudioSource aud in components)
            aud.enabled = false;
    }

	public void Sound_Off_Btn()
	{
        YandexGame.savesData.isSoundOn = 1;
        YandexGame.SaveProgress();
		Debug.Log(YandexGame.savesData.isSoundOn);
        UnityEngine.Debug.Log("Sound_Off_Btn");
		this.Sound_on.SetActive(true);
		this.Sound_off.SetActive(false);
        AudioSource[] components = AudioSource.FindObjectsOfType<AudioSource>();
        foreach (AudioSource aud in components)
            aud.enabled = true;
    }



	public GameObject Exit_Panel_Pop_Up;

	public GameObject All_Btn;

	public AudioSource Bg_Sound;

	public GameObject Sound_on;

	public GameObject Sound_off;



	public GameObject unlock_all_pop_up;

	public GameObject _Camera;

	public GameObject Main_Object_Pop_up;

	public GameObject Selection_All_Btn;

	public GameObject[] leaves;

	



}
