// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Api.InterstitialAd
using System;
using System.Diagnostics;
using System.Reflection;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	public class InterstitialAd
	{
		public InterstitialAd(string adUnitId)
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildInterstitialClient", BindingFlags.Static | BindingFlags.Public);
			this.client = (IInterstitialClient)method.Invoke(null, null);
			this.client.CreateInterstitialAd(adUnitId);
			this.client.OnAdLoaded += delegate(object sender, EventArgs args)
			{
				if (this.OnAdLoaded != null)
				{
					this.OnAdLoaded(this, args);
				}
			};
			this.client.OnAdFailedToLoad += delegate(object sender, AdFailedToLoadEventArgs args)
			{
				if (this.OnAdFailedToLoad != null)
				{
					this.OnAdFailedToLoad(this, args);
				}
			};
			this.client.OnAdOpening += delegate(object sender, EventArgs args)
			{
				if (this.OnAdOpening != null)
				{
					this.OnAdOpening(this, args);
				}
			};
			this.client.OnAdClosed += delegate(object sender, EventArgs args)
			{
				if (this.OnAdClosed != null)
				{
					this.OnAdClosed(this, args);
				}
			};
			this.client.OnAdLeavingApplication += delegate(object sender, EventArgs args)
			{
				if (this.OnAdLeavingApplication != null)
				{
					this.OnAdLeavingApplication(this, args);
				}
			};
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdLoaded;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdOpening;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdClosed;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		public bool IsLoaded()
		{
			return this.client.IsLoaded();
		}

		public void Show()
		{
			this.client.ShowInterstitial();
		}

		public void Destroy()
		{
			this.client.DestroyInterstitial();
		}

		public string MediationAdapterClassName()
		{
			return this.client.MediationAdapterClassName();
		}

		private IInterstitialClient client;
	}
}
