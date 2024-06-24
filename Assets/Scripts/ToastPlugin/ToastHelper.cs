// dnSpy decompiler from Assembly-CSharp.dll class: ToastPlugin.ToastHelper
using System;
using UnityEngine;

namespace ToastPlugin
{
	public static class ToastHelper
	{
		public static void ShowToast(string toastMsg, bool isLong = false)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("missing.toastplugin.ToastHelper");
			if (androidJavaClass != null)
			{
				androidJavaClass.CallStatic("showToast", new object[]
				{
					toastMsg,
					ToastHelper.getActivity(),
					isLong
				});
			}
		}

		private static AndroidJavaObject getActivity()
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			return androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		}
	}
}
