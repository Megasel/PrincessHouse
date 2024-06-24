// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Common.MobileAdsEventExecutor
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoogleMobileAds.Common
{
	public class MobileAdsEventExecutor : MonoBehaviour
	{
		public static void Initialize()
		{
			if (MobileAdsEventExecutor.IsActive())
			{
				return;
			}
			GameObject gameObject = new GameObject("MobileAdsMainThreadExecuter");
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			MobileAdsEventExecutor.instance = gameObject.AddComponent<MobileAdsEventExecutor>();
		}

		public static bool IsActive()
		{
			return MobileAdsEventExecutor.instance != null;
		}

		public void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public static void ExecuteInUpdate(Action action)
		{
			object obj = MobileAdsEventExecutor.adEventsQueue;
			lock (obj)
			{
				MobileAdsEventExecutor.adEventsQueue.Add(action);
				MobileAdsEventExecutor.adEventsQueueEmpty = false;
			}
		}

		public void Update()
		{
			if (MobileAdsEventExecutor.adEventsQueueEmpty)
			{
				return;
			}
			List<Action> list = new List<Action>();
			object obj = MobileAdsEventExecutor.adEventsQueue;
			lock (obj)
			{
				list.AddRange(MobileAdsEventExecutor.adEventsQueue);
				MobileAdsEventExecutor.adEventsQueue.Clear();
				MobileAdsEventExecutor.adEventsQueueEmpty = true;
			}
			foreach (Action action in list)
			{
				action();
			}
		}

		public void OnDisable()
		{
			MobileAdsEventExecutor.instance = null;
		}

		private static MobileAdsEventExecutor instance = null;

		private static List<Action> adEventsQueue = new List<Action>();

		private static volatile bool adEventsQueueEmpty = true;
	}
}
