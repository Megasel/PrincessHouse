// dnSpy decompiler from Assembly-CSharp.dll class: Exit_Panel
using System;
using UnityEngine;
using YG;

public class Exit_Panel : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
		{
			this.Exit_Panel_Pop_Up.SetActive(true);
			this.All_Btn.SetActive(false);
			this.No_ads_Panel.SetActive(false);
		}
	}

	public void Exit_Panel_Yes_Btn()
	{
		Application.Quit();
	}

	public void Exit_Panel_No_Btn()
	{
		this.Exit_Panel_Pop_Up.SetActive(false);
		
        this.All_Btn.SetActive(true);
		
	}

	public GameObject Exit_Panel_Pop_Up;

	public GameObject All_Btn;

	public GameObject No_ads_Panel;
}
