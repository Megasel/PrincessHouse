// dnSpy decompiler from Assembly-CSharp.dll class: ShareScreenScript
using System;
using UnityEngine;

public class ShareScreenScript : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ShareScreenShot()
	{
		GeneralScript._instance.TakeScreenShot(null, null);
	}
}
