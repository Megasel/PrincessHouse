// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Android.RewardBasedVideoAdClient
using System;
using System.Diagnostics;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class RewardBasedVideoAdClient : AndroidJavaProxy, IRewardBasedVideoAdClient
	{
		public RewardBasedVideoAdClient() : base("com.google.unity.ads.UnityRewardBasedVideoAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.androidRewardBasedVideo = new AndroidJavaObject("com.google.unity.ads.RewardBasedVideo", new object[]
			{
				@static,
				this
			});
		}

		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdLoaded;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdOpening;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdStarted;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdClosed;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<Reward> OnAdRewarded;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdLeavingApplication;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> OnAdCompleted;



		public void CreateRewardBasedVideoAd()
		{
			this.androidRewardBasedVideo.Call("create", new object[0]);
		}

		public void LoadAd(AdRequest request, string adUnitId)
		{
			this.androidRewardBasedVideo.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request),
				adUnitId
			});
		}

		public bool IsLoaded()
		{
			return this.androidRewardBasedVideo.Call<bool>("isLoaded", new object[0]);
		}

		public void ShowRewardBasedVideoAd()
		{
			this.androidRewardBasedVideo.Call("show", new object[0]);
		}

		public void SetUserId(string userId)
		{
			this.androidRewardBasedVideo.Call("setUserId", new object[]
			{
				userId
			});
		}

		public void DestroyRewardBasedVideoAd()
		{
			this.androidRewardBasedVideo.Call("destroy", new object[0]);
		}

		public string MediationAdapterClassName()
		{
			return this.androidRewardBasedVideo.Call<string>("getMediationAdapterClassName", new object[0]);
		}

		private void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		private void onAdFailedToLoad(string errorReason)
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

		private void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		private void onAdStarted()
		{
			if (this.OnAdStarted != null)
			{
				this.OnAdStarted(this, EventArgs.Empty);
			}
		}

		private void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		private void onAdRewarded(string type, float amount)
		{
			if (this.OnAdRewarded != null)
			{
				Reward e = new Reward
				{
					Type = type,
					Amount = (double)amount
				};
				this.OnAdRewarded(this, e);
			}
		}

		private void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}

		private void onAdCompleted()
		{
			if (this.OnAdCompleted != null)
			{
				this.OnAdCompleted(this, EventArgs.Empty);
			}
		}

		private AndroidJavaObject androidRewardBasedVideo;
	}
}
