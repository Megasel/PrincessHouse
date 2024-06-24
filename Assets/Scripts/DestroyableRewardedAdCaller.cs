// dnSpy decompiler from Assembly-CSharp.dll class: DestroyableRewardedAdCaller
using System;
using ToastPlugin;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class DestroyableRewardedAdCaller : MonoBehaviour
{
	private void OnValidate()
	{
		
	}

	private void Start()
	{
		if (YandexGame.savesData.Onetime == 0)
		{
            YandexGame.savesData.Onetime =  1;
            YandexGame.savesData.health1 =  0f;
            YandexGame.savesData.health2 =  0f;
            YandexGame.SaveProgress();
        }
	}

	private void OnEnable()
	{
		
	}

	public void CallRewardedVideo()
	{
		
	}

	private void VideoWatches()
	{
		
	}

	public void RewardedVideoFailed()
	{
		
	}

	public bool showPopup;


	public RewardedVideoType rewardedVideoType;

	[DrawIf("rewardedVideoType", RewardedVideoType.VideoCountUnlock, DrawIfAttribute.DisablingType.DontDraw)]
	public int unLockOnVideoCount;

	[DrawIf("rewardedVideoType", RewardedVideoType.VideoCountUnlock, DrawIfAttribute.DisablingType.DontDraw)]
	public Text textToShowCount;

	private int count;
}
