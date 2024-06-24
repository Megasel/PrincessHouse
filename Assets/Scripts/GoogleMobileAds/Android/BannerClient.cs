// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Android.BannerClient
using System;
using System.Diagnostics;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class BannerClient : AndroidJavaProxy, IBannerClient
	{
		public BannerClient() : base("com.google.unity.ads.UnityAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.bannerView = new AndroidJavaObject("com.google.unity.ads.Banner", new object[]
			{
				@static,
				this
			});
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

		public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			this.bannerView.Call("create", new object[]
			{
				adUnitId,
				Utils.GetAdSizeJavaObject(adSize),
				(int)position
			});
		}

		public void CreateBannerView(string adUnitId, AdSize adSize, int x, int y)
		{
			this.bannerView.Call("create", new object[]
			{
				adUnitId,
				Utils.GetAdSizeJavaObject(adSize),
				x,
				y
			});
		}

		public void LoadAd(AdRequest request)
		{
			this.bannerView.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request)
			});
		}

		public void ShowBannerView()
		{
			this.bannerView.Call("show", new object[0]);
		}

		public void HideBannerView()
		{
			this.bannerView.Call("hide", new object[0]);
		}

		public void DestroyBannerView()
		{
			this.bannerView.Call("destroy", new object[0]);
		}

		public float GetHeightInPixels()
		{
			return this.bannerView.Call<float>("getHeightInPixels", new object[0]);
		}

		public float GetWidthInPixels()
		{
			return this.bannerView.Call<float>("getWidthInPixels", new object[0]);
		}

		public void SetPosition(AdPosition adPosition)
		{
			this.bannerView.Call("setPosition", new object[]
			{
				(int)adPosition
			});
		}

		public void SetPosition(int x, int y)
		{
			this.bannerView.Call("setPosition", new object[]
			{
				x,
				y
			});
		}

		public string MediationAdapterClassName()
		{
			return this.bannerView.Call<string>("getMediationAdapterClassName", new object[0]);
		}

		public void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		public void onAdFailedToLoad(string errorReason)
		{
			if (this.OnAdFailedToLoad != null)
			{
				AdFailedToLoadEventArgs e = new AdFailedToLoadEventArgs
				{
					Message = errorReason
				};
				this.OnAdFailedToLoad(this, e);
			}
		}

		public void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		public void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		public void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}

		private AndroidJavaObject bannerView;
	}
}
