// dnSpy decompiler from Assembly-CSharp.dll class: Area730.SelfCrossPromo.NativeAdsItemLoader
using System;
using UnityEngine;

namespace Area730.SelfCrossPromo
{
	public class NativeAdsItemLoader : MonoBehaviour
	{
		public NativeAdsItemLoader.ItemElement[] AndroidApps;

		public NativeAdsItemLoader.ItemElement[] IosApps;

		[Serializable]
		public class ItemElement
		{
			public Sprite AppIcon;

			public string AppName;

			public string AppId;
		}
	}
}
