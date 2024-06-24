// dnSpy decompiler from Assembly-CSharp.dll class: LoadingScene
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
	private void Start()
	{
		Camera.main.aspect = 1.5f;
		this.LoadingBar.fillAmount = 0f;
		this.LoadingTime = 0.2f;
		this.barFilled = false;
		this.interstitialLoaded = false;
	}

	private void Update()
	{
		if (this.LoadingBar.fillAmount < 1f)
		{
			this.LoadingBar.fillAmount += this.LoadingTime * Time.deltaTime;
			if (this.LoadingBar.fillAmount * 100f < 80f || !this.interstitialLoaded)
			{
			}
		}
		else if (this.LoadingBar.fillAmount >= 1f && !this.barFilled)
		{
			this.barFilled = true;
			this.goToNextScene();
		}
		this.percentageText.text = (this.LoadingBar.fillAmount * 100f).ToString("f0");
	}

	private void goToNextScene()
	{
		UnityEngine.Debug.Log(".....................Loading The Desired Scene.....................");
		LoadingScene.loadNumber++;
	}

	public Image LoadingBar;

	private float LoadingTime;

	private bool barFilled;

	public Text percentageText;

	private bool interstitialLoaded;

	public static int SceneNumber = 0;

	public static int loadNumber = 0;
}
