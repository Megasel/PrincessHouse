// dnSpy decompiler from Assembly-CSharp.dll class: tk2dCameraAnchor
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Camera/tk2dCameraAnchor")]
[ExecuteInEditMode]
public class tk2dCameraAnchor : MonoBehaviour
{
	public tk2dBaseSprite.Anchor AnchorPoint
	{
		get
		{
			if (this.anchor != -1)
			{
				if (this.anchor >= 0 && this.anchor <= 2)
				{
					this._anchorPoint = this.anchor + tk2dBaseSprite.Anchor.UpperLeft;
				}
				else if (this.anchor >= 6 && this.anchor <= 8)
				{
					this._anchorPoint = (tk2dBaseSprite.Anchor)(this.anchor - 6);
				}
				else
				{
					this._anchorPoint = (tk2dBaseSprite.Anchor)this.anchor;
				}
				this.anchor = -1;
			}
			return this._anchorPoint;
		}
		set
		{
			this._anchorPoint = value;
		}
	}

	public Vector2 AnchorOffsetPixels
	{
		get
		{
			return this.offset;
		}
		set
		{
			this.offset = value;
		}
	}

	public bool AnchorToNativeBounds
	{
		get
		{
			return this.anchorToNativeBounds;
		}
		set
		{
			this.anchorToNativeBounds = value;
		}
	}

	public Camera AnchorCamera
	{
		get
		{
			if (this.tk2dCamera != null)
			{
				this._anchorCamera = this.tk2dCamera.GetComponent<Camera>();
				this.tk2dCamera = null;
			}
			return this._anchorCamera;
		}
		set
		{
			this._anchorCamera = value;
			this._anchorCameraCached = null;
		}
	}

	private tk2dCamera AnchorTk2dCamera
	{
		get
		{
			if (this._anchorCameraCached != this._anchorCamera)
			{
				this._anchorTk2dCamera = this._anchorCamera.GetComponent<tk2dCamera>();
				this._anchorCameraCached = this._anchorCamera;
			}
			return this._anchorTk2dCamera;
		}
	}

	private Transform myTransform
	{
		get
		{
			if (this._myTransform == null)
			{
				this._myTransform = base.transform;
			}
			return this._myTransform;
		}
	}

	private void Start()
	{
		this.UpdateTransform();
	}

	private void UpdateTransform()
	{
		if (this.AnchorCamera == null)
		{
			return;
		}
		float num = 1f;
		Vector3 localPosition = this.myTransform.localPosition;
		tk2dCamera tk2dCamera = (!(this.AnchorTk2dCamera != null) || this.AnchorTk2dCamera.CameraSettings.projection == tk2dCameraSettings.ProjectionType.Perspective) ? null : this.AnchorTk2dCamera;
		Rect rect = default(Rect);
		if (tk2dCamera != null)
		{
			rect = ((!this.anchorToNativeBounds) ? tk2dCamera.ScreenExtents : tk2dCamera.NativeScreenExtents);
			num = tk2dCamera.GetSizeAtDistance(1f);
		}
		else
		{
			rect.Set(0f, 0f, (float)this.AnchorCamera.pixelWidth, (float)this.AnchorCamera.pixelHeight);
		}
		float yMin = rect.yMin;
		float yMax = rect.yMax;
		float y = (yMin + yMax) * 0.5f;
		float xMin = rect.xMin;
		float xMax = rect.xMax;
		float x = (xMin + xMax) * 0.5f;
		Vector3 zero = Vector3.zero;
		switch (this.AnchorPoint)
		{
		case tk2dBaseSprite.Anchor.LowerLeft:
			zero = new Vector3(xMin, yMin, localPosition.z);
			break;
		case tk2dBaseSprite.Anchor.LowerCenter:
			zero = new Vector3(x, yMin, localPosition.z);
			break;
		case tk2dBaseSprite.Anchor.LowerRight:
			zero = new Vector3(xMax, yMin, localPosition.z);
			break;
		case tk2dBaseSprite.Anchor.MiddleLeft:
			zero = new Vector3(xMin, y, localPosition.z);
			break;
		case tk2dBaseSprite.Anchor.MiddleCenter:
			zero = new Vector3(x, y, localPosition.z);
			break;
		case tk2dBaseSprite.Anchor.MiddleRight:
			zero = new Vector3(xMax, y, localPosition.z);
			break;
		case tk2dBaseSprite.Anchor.UpperLeft:
			zero = new Vector3(xMin, yMax, localPosition.z);
			break;
		case tk2dBaseSprite.Anchor.UpperCenter:
			zero = new Vector3(x, yMax, localPosition.z);
			break;
		case tk2dBaseSprite.Anchor.UpperRight:
			zero = new Vector3(xMax, yMax, localPosition.z);
			break;
		}
		Vector3 vector = zero + new Vector3(num * this.offset.x, num * this.offset.y, 0f);
		if (tk2dCamera == null)
		{
			Vector3 vector2 = this.AnchorCamera.ScreenToWorldPoint(vector);
			if (this.myTransform.position != vector2)
			{
				this.myTransform.position = vector2;
			}
		}
		else
		{
			Vector3 localPosition2 = this.myTransform.localPosition;
			if (localPosition2 != vector)
			{
				this.myTransform.localPosition = vector;
			}
		}
	}

	public void ForceUpdateTransform()
	{
		this.UpdateTransform();
	}

	private void LateUpdate()
	{
		this.UpdateTransform();
	}

	[SerializeField]
	private int anchor = -1;

	[SerializeField]
	private tk2dBaseSprite.Anchor _anchorPoint = tk2dBaseSprite.Anchor.UpperLeft;

	[SerializeField]
	private bool anchorToNativeBounds;

	[SerializeField]
	private Vector2 offset = Vector2.zero;

	[SerializeField]
	private tk2dCamera tk2dCamera;

	[SerializeField]
	private Camera _anchorCamera;

	private Camera _anchorCameraCached;

	private tk2dCamera _anchorTk2dCamera;

	private Transform _myTransform;
}
