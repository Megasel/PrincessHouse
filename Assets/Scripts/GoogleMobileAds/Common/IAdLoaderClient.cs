// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Common.IAdLoaderClient
using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	public interface IAdLoaderClient
	{
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		void LoadAd(AdRequest request);
	}
}
