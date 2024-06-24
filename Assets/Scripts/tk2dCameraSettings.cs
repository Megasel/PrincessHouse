// dnSpy decompiler from Assembly-CSharp.dll class: tk2dCameraSettings
using System;
using UnityEngine;

[Serializable]
public class tk2dCameraSettings
{
	public tk2dCameraSettings.ProjectionType projection;

	public float orthographicSize = 10f;

	public float orthographicPixelsPerMeter = 100f;

	public tk2dCameraSettings.OrthographicOrigin orthographicOrigin = tk2dCameraSettings.OrthographicOrigin.Center;

	public tk2dCameraSettings.OrthographicType orthographicType;

	public TransparencySortMode transparencySortMode;

	public float fieldOfView = 60f;

	public Rect rect = new Rect(0f, 0f, 1f, 1f);

	public enum ProjectionType
	{
		Orthographic,
		Perspective
	}

	public enum OrthographicType
	{
		PixelsPerMeter,
		OrthographicSize
	}

	public enum OrthographicOrigin
	{
		BottomLeft,
		Center
	}
}
