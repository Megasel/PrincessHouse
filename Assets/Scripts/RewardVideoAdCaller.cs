// dnSpy decompiler from Assembly-CSharp.dll class: RewardVideoAdCaller
using System;
using ToastPlugin;
using UnityEngine;

public class RewardVideoAdCaller : MonoBehaviour
{
	private void OnValidate()
	{
		if (this.showPopup && this.confirmationPopup == null)
		{
			this.confirmationPopup = UnityEngine.Object.FindObjectOfType<RewardedVideoConfirmationPopup>().gameObject;
			if (this.confirmationPopup == null)
			{
				UnityEngine.Debug.LogError("Please Drag prefab ==> -Watch Video Confirmation popup- from Prefab folder in AdsScript.");
			}
		}
	}

	private void Start()
	{
	}

	public void CallRewardedVideo()
	{
		try
		{
			if (Application.internetReachability != NetworkReachability.NotReachable)
			{
				WatchRewardedVideoAd.callBackObject = base.gameObject;
				if (this.showPopup)
				{
					this.confirmationPopup.SetActive(true);
				}
				else
				{
					WatchRewardedVideoAd.instance.CallRewardedAd();
				}
			}
			else
			{
				ToastHelper.ShowToast("No, Network Available", true);
			}
		}
		catch (Exception ex)
		{
			GeneralScript._instance.SendExceptionEmail(ex.Message.ToString(), "function not set");
		}
	}

	private void VideoWatches()
	{
		try
		{
			int rewardInteger = this.RewardInteger;
			if (rewardInteger != 0)
			{
				if (rewardInteger != 1)
				{
				}
			}
		}
		catch (Exception ex)
		{
			GeneralScript._instance.SendExceptionEmail(ex.Message.ToString(), "function not set");
		}
	}

	public void RewardedVideoFailed()
	{
	}

	public bool showPopup;

	[DrawIf("showPopup", true, DrawIfAttribute.DisablingType.DontDraw)]
	public GameObject confirmationPopup;

	[Tooltip("To Recognize Reward")]
	public int RewardInteger;
}
