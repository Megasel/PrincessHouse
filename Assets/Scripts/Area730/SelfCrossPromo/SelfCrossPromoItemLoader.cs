// dnSpy decompiler from Assembly-CSharp.dll class: Area730.SelfCrossPromo.SelfCrossPromoItemLoader
using System;
using UnityEngine;

namespace Area730.SelfCrossPromo
{
	public class SelfCrossPromoItemLoader : MonoBehaviour
	{
		public SelfCrossPromoItemLoader.ItemElement[] AndroidApps;

		public SelfCrossPromoItemLoader.ItemElement[] IosApps;

		[Serializable]
		public class ItemElement
		{
			public Sprite AppIcon;

			public string AppName;

			public string AppId;
		}
	}
}
