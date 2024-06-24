// dnSpy decompiler from Assembly-CSharp.dll class: WatchRewardedVideoAd
using System;
using ToastPlugin;
using UnityEngine;
using UnityEngine.Advertisements;

public class WatchRewardedVideoAd : MonoBehaviour
{
	private void Start()
	{
		WatchRewardedVideoAd.instance = this;
	}

	public void CallRewardedAd()
	{
		try
		{
			if (RemoteSettings.GetBool("showAds", false))
			{
				if (Application.internetReachability != NetworkReachability.NotReachable)
				{
					//CheckInternetConnectivity._instance.CheckInternetConnection(new Action<bool>(this.InternetResponce));
				}
				else
				{
					ToastHelper.ShowToast("Sorry,No Internet Connection", true);
				}
			}
			else
			{
				ToastHelper.ShowToast("Sorry,No Video Ad Available.", true);
			}
		}
		catch (Exception ex)
		{
			GeneralScript._instance.SendExceptionEmail(ex.Message.ToString(), "function not set");
		}
	}

	//private void InternetResponce(bool resp)
	//{
	//	if (resp)
	//	{
	//		this.CallToRewardedAd();
	//	}
	//	else
	//	{
	//		ToastHelper.ShowToast("Sorry,No Internet Connection", true);
	//	}
	//}

	private void CallToRewardedAd()
	{

		switch (this.rewardedAdsSequence)
		{
		case 0:
			this.CallRewardedAdPerAdmobe();
			break;
		case 1:
			this.CallRewardedAdUnity();
			break;
		case 2:
			this.CallRewardedAdPerLovin();
			break;
		case 3:
			this.CallRewardedAdIronSource();
			break;
		}
	}

	public void CallRewardedAdPerAdmobe()
	{
		
	}

	public void CallRewardedAdPerLovin()
	{
		
	}

	public void CallRewardedAdUnity()
	{
		
	}

	public void CallRewardedAdIronSource()
	{
		
	}



	public static GameObject callBackObject;

	public static WatchRewardedVideoAd instance;

	private int rewardedAdsSequence;
}
