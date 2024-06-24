// dnSpy decompiler from Assembly-CSharp.dll class: tk2dPixelPerfectHelper
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Deprecated/Extra/tk2dPixelPerfectHelper")]
public class tk2dPixelPerfectHelper : MonoBehaviour
{
	public static tk2dPixelPerfectHelper inst
	{
		get
		{
			if (tk2dPixelPerfectHelper._inst == null)
			{
				tk2dPixelPerfectHelper._inst = (UnityEngine.Object.FindObjectOfType(typeof(tk2dPixelPerfectHelper)) as tk2dPixelPerfectHelper);
				if (tk2dPixelPerfectHelper._inst == null)
				{
					return null;
				}
				tk2dPixelPerfectHelper.inst.Setup();
			}
			return tk2dPixelPerfectHelper._inst;
		}
	}

	private void Awake()
	{
		this.Setup();
		tk2dPixelPerfectHelper._inst = this;
	}

	public virtual void Setup()
	{
		float num = (float)this.collectionTargetHeight / this.targetResolutionHeight;
		if (base.GetComponent<Camera>() != null)
		{
			this.cam = base.GetComponent<Camera>();
		}
		if (this.cam == null)
		{
			this.cam = Camera.main;
		}
		if (this.cam.orthographic)
		{
			this.scaleK = num * this.cam.orthographicSize / this.collectionOrthoSize;
			this.scaleD = 0f;
		}
		else
		{
			float num2 = num * Mathf.Tan(0.0174532924f * this.cam.fieldOfView * 0.5f) / this.collectionOrthoSize;
			this.scaleK = num2 * -this.cam.transform.position.z;
			this.scaleD = num2;
		}
	}

	public static float CalculateScaleForPerspectiveCamera(float fov, float zdist)
	{
		return Mathf.Abs(Mathf.Tan(0.0174532924f * fov * 0.5f) * zdist);
	}

	public bool CameraIsOrtho
	{
		get
		{
			return this.cam.orthographic;
		}
	}

	private static tk2dPixelPerfectHelper _inst;

	[NonSerialized]
	public Camera cam;

	public int collectionTargetHeight = 640;

	public float collectionOrthoSize = 1f;

	public float targetResolutionHeight = 640f;

	[NonSerialized]
	public float scaleD;

	[NonSerialized]
	public float scaleK;
}
