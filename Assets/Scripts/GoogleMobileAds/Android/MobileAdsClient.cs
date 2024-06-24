// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Android.MobileAdsClient
using System;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class MobileAdsClient : IMobileAdsClient
	{
		private MobileAdsClient()
		{
		}

		public static MobileAdsClient Instance
		{
			get
			{
				return MobileAdsClient.instance;
			}
		}

		public void Initialize(string appId)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.google.android.gms.ads.MobileAds");
			androidJavaClass2.CallStatic("initialize", new object[]
			{
				@static,
				appId
			});
		}

		public void SetApplicationVolume(float volume)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.android.gms.ads.MobileAds");
			androidJavaClass.CallStatic("setAppVolume", new object[]
			{
				volume
			});
		}

		public void SetApplicationMuted(bool muted)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.android.gms.ads.MobileAds");
			androidJavaClass.CallStatic("setAppMuted", new object[]
			{
				muted
			});
		}

		public void SetiOSAppPauseOnBackground(bool pause)
		{
		}

		private static MobileAdsClient instance = new MobileAdsClient();
	}
}
