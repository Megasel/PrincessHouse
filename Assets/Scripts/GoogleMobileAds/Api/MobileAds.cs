// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Api.MobileAds
using System;
using System.Reflection;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	public class MobileAds
	{
		public static void Initialize(string appId)
		{
			MobileAds.client.Initialize(appId);
			MobileAdsEventExecutor.Initialize();
		}

		public static void SetApplicationMuted(bool muted)
		{
			MobileAds.client.SetApplicationMuted(muted);
		}

		public static void SetApplicationVolume(float volume)
		{
			MobileAds.client.SetApplicationVolume(volume);
		}

		public static void SetiOSAppPauseOnBackground(bool pause)
		{
			MobileAds.client.SetiOSAppPauseOnBackground(pause);
		}

		private static IMobileAdsClient GetMobileAdsClient()
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("MobileAdsInstance", BindingFlags.Static | BindingFlags.Public);
			return (IMobileAdsClient)method.Invoke(null, null);
		}

		private static readonly IMobileAdsClient client = MobileAds.GetMobileAdsClient();
	}
}
