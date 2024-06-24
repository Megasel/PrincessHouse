// dnSpy decompiler from Assembly-CSharp.dll class: MainMenuButtons
using System;
using UnityEngine;
using YG;

public class MainMenuButtons : MonoBehaviour
{
	private void Start()
	{
		try
		{
				this.GDPRPanel.SetActive(true);
				this.ispanel = true;
			
		}
		catch (Exception ex)
		{
			GeneralScript._instance.SendExceptionEmail(ex.Message.ToString(), "function not set");
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
		{
			if (!this.ispanel)
			{
				this.ispanel = true;
				if (YandexGame.savesData.rateus == 0)
				{
					this.RateUs.SetActive(true);
				}
				else
				{
					this.ExistPanel.SetActive(true);
				}
			}
			else
			{
				this.ClosePanel();
			}
		}
	}

	public void ClosePanel()
	{
		try
		{
			this.RateUs.SetActive(false);
			this.ExistPanel.SetActive(false);
			this.ispanel = false;
		}
		catch (Exception ex)
		{
			GeneralScript._instance.SendExceptionEmail(ex.Message.ToString(), "function not set");
		}
	}

	public void ExitApplication()
	{
		Application.Quit();
	}

	public void CloseGDPRPanel()
	{
		try
		{
            this.GDPRPanel.SetActive(false);
		}
		catch (Exception ex)
		{
			GeneralScript._instance.SendExceptionEmail(ex.Message.ToString(), "function not set");
		}
	}

	public void MoreGames()
	{

	}

	public void RateUsLink()
	{
	}

	public void PrivacyPolicyLink()
	{
	}

	[SerializeField]
	private GameObject ExistPanel;

	[SerializeField]
	private GameObject RateUs;

	[SerializeField]
	private GameObject GDPRPanel;

	private bool ispanel;
}
