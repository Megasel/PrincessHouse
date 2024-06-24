// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUITime
using System;
using UnityEngine;

public static class tk2dUITime
{
	public static float deltaTime
	{
		get
		{
			return tk2dUITime._deltaTime;
		}
	}

	public static void Init()
	{
		tk2dUITime.lastRealTime = (double)Time.realtimeSinceStartup;
		tk2dUITime._deltaTime = Time.maximumDeltaTime;
	}

	public static void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (Time.timeScale < 0.001f)
		{
			tk2dUITime._deltaTime = Mathf.Min(0.06666667f, (float)((double)realtimeSinceStartup - tk2dUITime.lastRealTime));
		}
		else
		{
			tk2dUITime._deltaTime = Time.deltaTime / Time.timeScale;
		}
		tk2dUITime.lastRealTime = (double)realtimeSinceStartup;
	}

	private static double lastRealTime;

	private static float _deltaTime = 0.0166666675f;
}
