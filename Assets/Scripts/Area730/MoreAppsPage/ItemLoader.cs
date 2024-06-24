// dnSpy decompiler from Assembly-CSharp.dll class: Area730.MoreAppsPage.ItemLoader
using System;
using UnityEngine;

namespace Area730.MoreAppsPage
{
	[AddComponentMenu("More Apps/Item Loader")]
	public class ItemLoader : MonoBehaviour
	{
		public ItemLoader.ItemElement[] AndroidApps;

		public ItemLoader.ItemElement[] IosApps;

		[Serializable]
		public class ItemElement
		{
			public Sprite AppIcon;

			public string AppName;

			public string AppId;
		}
	}
}
