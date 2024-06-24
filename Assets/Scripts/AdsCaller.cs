// dnSpy decompiler from Assembly-CSharp.dll class: AdsCaller
using System;
using ToastPlugin;
using UnityEngine;

public class AdsCaller : MonoBehaviour
{
	private void OnEnable()
	{
		try
		{
			if (RemoteSettings.GetBool("showAds", false))
			{
				if (Application.internetReachability != NetworkReachability.NotReachable)
				{
					//CheckInternetConnectivity._instance.CheckInternetConnection(new Action<bool, Ping>(this.InternetResponce));
				}
				else
				{
					ToastHelper.ShowToast("Sorry,No Internet Connection", true);
				}
			}
		}
		catch (Exception ex)
		{
			GeneralScript._instance.SendExceptionEmail(ex.Message.ToString(), "function not set");
		}
	}

	private void InternetResponce(bool resp)
	{
		if (resp)
		{
			this.CallingToAds();
		}
	}

	public void CallingToAds()
	{
		
	}

	public void CallAds()
	{
		if (this.AdsType == Adspref.Menu)
		{
		}
		else if (this.AdsType == Adspref.Selection)
		{
		}
		else if (this.AdsType == Adspref.GamePause)
		{
		}
		else if (this.AdsType == Adspref.GameEnd)
		{
		}
	}

	public void CallAdsForMainMenu(int sequenceIndex)
	{
		switch (sequenceIndex)
		{
		case 0:
			this.AdmobAd();
			break;
		case 1:
			this.UnityAd();
			break;
		case 2:
			this.LovinAd();
			break;
		case 3:
			this.IronSourceAd();
			break;
		}
	}

	public void CallAdsForSelection(int sequenceIndex)
	{
		switch (sequenceIndex)
		{
		case 0:
			this.AdmobAd();
			break;
		case 1:
			this.UnityAd();
			break;
		case 2:
			this.LovinAd();
			break;
		case 3:
			this.IronSourceAd();
			break;
		}
	}

	public void CallAdsForGamePause(int sequenceIndex)
	{
		switch (sequenceIndex)
		{
		case 0:
			this.AdmobAd();
			break;
		case 1:
			this.UnityAd();
			break;
		case 2:
			this.LovinAd();
			break;
		case 3:
			this.IronSourceAd();
			break;
		}
	}

	public void CallAdsForGameEnd(int sequenceIndex)
	{
		switch (sequenceIndex)
		{
		case 0:
			this.AdmobAd();
			break;
		case 1:
			this.UnityAd();
			break;
		case 2:
			this.LovinAd();
			break;
		case 3:
			this.IronSourceAd();
			break;
		}
	}

	private void AdmobAd()
	{
		
	}

	private void UnityAd()
	{
		
	}

	private void IronSourceAd()
	{
		
	}

	private void LovinAd()
	{
		
	}

	public Adspref AdsType;
}
