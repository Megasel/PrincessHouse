// dnSpy decompiler from Assembly-CSharp.dll class: RewardedVideoConfirmationPopup
using System;
using System.Reflection;
using UnityEngine;

public class RewardedVideoConfirmationPopup : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Debug.Log("Going to Deactive Object");
		base.gameObject.SetActive(false);
	}

	private void Update()
	{
	}

	public void ShowRewardedAd()
	{
		try
		{
			if (this.watchVideoScript != null)
			{
				this.watchVideoScript.CallRewardedAd();
			}
			else
			{
				this.watchVideoScript = UnityEngine.Object.FindObjectOfType<WatchRewardedVideoAd>();
				this.watchVideoScript.CallRewardedAd();
			}
		}
		catch (Exception ex)
		{
			GeneralScript._instance.SendExceptionEmail(ex.Message, base.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name + "()");
		}
	}

	private WatchRewardedVideoAd watchVideoScript;
}
