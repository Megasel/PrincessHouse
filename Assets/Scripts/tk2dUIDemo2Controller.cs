// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIDemo2Controller
using System;
using System.Collections;
using UnityEngine;

public class tk2dUIDemo2Controller : tk2dUIBaseDemoController
{
	private void Start()
	{
		this.rectMin[0] = this.windowLayout.GetMinBounds();
		this.rectMax[0] = this.windowLayout.GetMaxBounds();
	}

	private IEnumerator NextButtonPressed()
	{
		if (!this.allowButtonPress)
		{
			yield break;
		}
		this.allowButtonPress = false;
		this.currRect = (this.currRect + 1) % this.rectMin.Length;
		Vector3 min = this.rectMin[this.currRect];
		Vector3 max = this.rectMax[this.currRect];
		yield return base.StartCoroutine(base.coResizeLayout(this.windowLayout, min, max, 0.15f));
		this.allowButtonPress = true;
		yield break;
	}

	private void LateUpdate()
	{
		int num = this.rectMin.Length - 1;
		this.rectMin[num].Set(tk2dCamera.Instance.ScreenExtents.xMin, tk2dCamera.Instance.ScreenExtents.yMin, 0f);
		this.rectMax[num].Set(tk2dCamera.Instance.ScreenExtents.xMax, tk2dCamera.Instance.ScreenExtents.yMax, 0f);
	}

	public tk2dUILayout windowLayout;

	private Vector3[] rectMin = new Vector3[]
	{
		Vector3.zero,
		new Vector3(-0.8f, -0.7f, 0f),
		new Vector3(-0.9f, -0.9f, 0f),
		new Vector3(-1f, -0.9f, 0f),
		new Vector3(-1f, -1f, 0f),
		Vector3.zero
	};

	private Vector3[] rectMax = new Vector3[]
	{
		Vector3.one,
		new Vector3(0.8f, 0.7f, 0f),
		new Vector3(0.9f, 0.9f, 0f),
		new Vector3(0.6f, 0.7f, 0f),
		new Vector3(1f, 1f, 0f),
		Vector3.one
	};

	private int currRect;

	private bool allowButtonPress = true;
}
