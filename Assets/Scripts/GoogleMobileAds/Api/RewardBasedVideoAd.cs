// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Api.RewardBasedVideoAd
using System;
using System.Diagnostics;
using System.Reflection;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	public class RewardBasedVideoAd
	{
		private RewardBasedVideoAd()
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildRewardBasedVideoAdClient", BindingFlags.Static | BindingFlags.Public);
			this.client = (IRewardBasedVideoAdClient)method.Invoke(null, null);
			this.client.CreateRewardBasedVideoAd();
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
			this.client.OnAdStarted += delegate(object sender, EventArgs args)
			{
				if (this.OnAdStarted != null)
				{
					this.OnAdStarted(this, args);
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
			this.client.OnAdRewarded += delegate(object sender, Reward args)
			{
				if (this.OnAdRewarded != null)
				{
					this.OnAdRewarded(this, args);
				}
			};
			this.client.OnAdCompleted += delegate(object sender, EventArgs args)
			{
				if (this.OnAdCompleted != null)
				{
					this.OnAdCompleted(this, args);
				}
			};
		}

		public static RewardBasedVideoAd Instance
		{
			get
			{
				return RewardBasedVideoAd.instance;
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdLoaded;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdOpening;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdStarted;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdClosed;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<Reward> OnAdRewarded;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdCompleted;

		public void LoadAd(AdRequest request, string adUnitId)
		{
			this.client.LoadAd(request, adUnitId);
		}

		public bool IsLoaded()
		{
			return this.client.IsLoaded();
		}

		public void Show()
		{
			this.client.ShowRewardBasedVideoAd();
		}

		public void SetUserId(string userId)
		{
			this.client.SetUserId(userId);
		}

		public string MediationAdapterClassName()
		{
			return this.client.MediationAdapterClassName();
		}

		private IRewardBasedVideoAdClient client;

		private static readonly RewardBasedVideoAd instance = new RewardBasedVideoAd();
	}
}
