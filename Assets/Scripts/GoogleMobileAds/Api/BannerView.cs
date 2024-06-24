// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Api.BannerView
using System;
using System.Diagnostics;
using System.Reflection;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	public class BannerView
	{
		public BannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildBannerClient", BindingFlags.Static | BindingFlags.Public);
			this.client = (IBannerClient)method.Invoke(null, null);
			this.client.CreateBannerView(adUnitId, adSize, position);
			this.ConfigureBannerEvents();
		}

		public BannerView(string adUnitId, AdSize adSize, int x, int y)
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildBannerClient", BindingFlags.Static | BindingFlags.Public);
			this.client = (IBannerClient)method.Invoke(null, null);
			this.client.CreateBannerView(adUnitId, adSize, x, y);
			this.ConfigureBannerEvents();
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

		public void Hide()
		{
			this.client.HideBannerView();
		}

		public void Show()
		{
			this.client.ShowBannerView();
		}

		public void Destroy()
		{
			this.client.DestroyBannerView();
		}

		public float GetHeightInPixels()
		{
			return this.client.GetHeightInPixels();
		}

		public float GetWidthInPixels()
		{
			return this.client.GetWidthInPixels();
		}

		public void SetPosition(AdPosition adPosition)
		{
			this.client.SetPosition(adPosition);
		}

		public void SetPosition(int x, int y)
		{
			this.client.SetPosition(x, y);
		}

		private void ConfigureBannerEvents()
		{
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

		public string MediationAdapterClassName()
		{
			return this.client.MediationAdapterClassName();
		}

		private IBannerClient client;
	}
}
