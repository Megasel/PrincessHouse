// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIBaseItemControl
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIBaseItemControl")]
public abstract class tk2dUIBaseItemControl : MonoBehaviour
{
	public GameObject SendMessageTarget
	{
		get
		{
			if (this.uiItem != null)
			{
				return this.uiItem.sendMessageTarget;
			}
			return null;
		}
		set
		{
			if (this.uiItem != null)
			{
				this.uiItem.sendMessageTarget = value;
			}
		}
	}

	public static void ChangeGameObjectActiveState(GameObject go, bool isActive)
	{
		go.SetActive(isActive);
	}

	public static void ChangeGameObjectActiveStateWithNullCheck(GameObject go, bool isActive)
	{
		if (go != null)
		{
			tk2dUIBaseItemControl.ChangeGameObjectActiveState(go, isActive);
		}
	}

	protected void DoSendMessage(string methodName, object parameter)
	{
		if (this.SendMessageTarget != null && methodName.Length > 0)
		{
			this.SendMessageTarget.SendMessage(methodName, parameter, SendMessageOptions.RequireReceiver);
		}
	}

	public tk2dUIItem uiItem;
}
