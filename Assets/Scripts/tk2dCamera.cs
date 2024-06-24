// dnSpy decompiler from Assembly-CSharp.dll class: tk2dCamera
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Camera/tk2dCamera")]
[ExecuteInEditMode]
public class tk2dCamera : MonoBehaviour
{
	public tk2dCameraSettings CameraSettings
	{
		get
		{
			return this.cameraSettings;
		}
	}

	public tk2dCameraResolutionOverride CurrentResolutionOverride
	{
		get
		{
			tk2dCamera settingsRoot = this.SettingsRoot;
			Camera screenCamera = this.ScreenCamera;
			float num = (float)screenCamera.pixelWidth;
			float num2 = (float)screenCamera.pixelHeight;
			tk2dCameraResolutionOverride tk2dCameraResolutionOverride = null;
			if (tk2dCameraResolutionOverride == null || (tk2dCameraResolutionOverride != null && ((float)tk2dCameraResolutionOverride.width != num || (float)tk2dCameraResolutionOverride.height != num2)))
			{
				tk2dCameraResolutionOverride = null;
				if (settingsRoot.resolutionOverride != null)
				{
					foreach (tk2dCameraResolutionOverride tk2dCameraResolutionOverride2 in settingsRoot.resolutionOverride)
					{
						if (tk2dCameraResolutionOverride2.Match((int)num, (int)num2))
						{
							tk2dCameraResolutionOverride = tk2dCameraResolutionOverride2;
							break;
						}
					}
				}
			}
			return tk2dCameraResolutionOverride;
		}
	}

	public tk2dCamera InheritConfig
	{
		get
		{
			return this.inheritSettings;
		}
		set
		{
			if (this.inheritSettings != value)
			{
				this.inheritSettings = value;
				this._settingsRoot = null;
			}
		}
	}

	private Camera UnityCamera
	{
		get
		{
			if (this._unityCamera == null)
			{
				this._unityCamera = base.GetComponent<Camera>();
				if (this._unityCamera == null)
				{
					UnityEngine.Debug.LogError("A unity camera must be attached to the tk2dCamera script");
				}
			}
			return this._unityCamera;
		}
	}

	public static tk2dCamera Instance
	{
		get
		{
			return tk2dCamera.inst;
		}
	}

	public static tk2dCamera CameraForLayer(int layer)
	{
		int num = 1 << layer;
		int count = tk2dCamera.allCameras.Count;
		for (int i = 0; i < count; i++)
		{
			tk2dCamera tk2dCamera = tk2dCamera.allCameras[i];
			if ((tk2dCamera.UnityCamera.cullingMask & num) == num)
			{
				return tk2dCamera;
			}
		}
		return null;
	}

	public Rect ScreenExtents
	{
		get
		{
			return this._screenExtents;
		}
	}

	public Rect NativeScreenExtents
	{
		get
		{
			return this._nativeScreenExtents;
		}
	}

	public Vector2 TargetResolution
	{
		get
		{
			return this._targetResolution;
		}
	}

	public Vector2 NativeResolution
	{
		get
		{
			return new Vector2((float)this.nativeResolutionWidth, (float)this.nativeResolutionHeight);
		}
	}

	[Obsolete]
	public Vector2 ScreenOffset
	{
		get
		{
			return new Vector2(this.ScreenExtents.xMin - this.NativeScreenExtents.xMin, this.ScreenExtents.yMin - this.NativeScreenExtents.yMin);
		}
	}

	[Obsolete]
	public Vector2 resolution
	{
		get
		{
			return new Vector2(this.ScreenExtents.xMax, this.ScreenExtents.yMax);
		}
	}

	[Obsolete]
	public Vector2 ScreenResolution
	{
		get
		{
			return new Vector2(this.ScreenExtents.xMax, this.ScreenExtents.yMax);
		}
	}

	[Obsolete]
	public Vector2 ScaledResolution
	{
		get
		{
			return new Vector2(this.ScreenExtents.width, this.ScreenExtents.height);
		}
	}

	public float ZoomFactor
	{
		get
		{
			return this.zoomFactor;
		}
		set
		{
			this.zoomFactor = Mathf.Max(0.01f, value);
		}
	}

	[Obsolete]
	public float zoomScale
	{
		get
		{
			return 1f / Mathf.Max(0.001f, this.zoomFactor);
		}
		set
		{
			this.ZoomFactor = 1f / Mathf.Max(0.001f, value);
		}
	}

	public Camera ScreenCamera
	{
		get
		{
			bool flag = this.viewportClippingEnabled && this.inheritSettings != null && this.inheritSettings.UnityCamera.rect == this.unitRect;
			return (!flag) ? this.UnityCamera : this.inheritSettings.UnityCamera;
		}
	}

	private void Awake()
	{
		this.Upgrade();
		if (tk2dCamera.allCameras.IndexOf(this) == -1)
		{
			tk2dCamera.allCameras.Add(this);
		}
		tk2dCamera settingsRoot = this.SettingsRoot;
		tk2dCameraSettings tk2dCameraSettings = settingsRoot.CameraSettings;
		if (tk2dCameraSettings.projection == tk2dCameraSettings.ProjectionType.Perspective)
		{
			this.UnityCamera.transparencySortMode = tk2dCameraSettings.transparencySortMode;
		}
	}

	private void OnEnable()
	{
		if (this.UnityCamera != null)
		{
			this.UpdateCameraMatrix();
		}
		else
		{
			base.GetComponent<Camera>().enabled = false;
		}
		if (!this.viewportClippingEnabled)
		{
			tk2dCamera.inst = this;
		}
		if (tk2dCamera.allCameras.IndexOf(this) == -1)
		{
			tk2dCamera.allCameras.Add(this);
		}
	}

	private void OnDestroy()
	{
		int num = tk2dCamera.allCameras.IndexOf(this);
		if (num != -1)
		{
			tk2dCamera.allCameras.RemoveAt(num);
		}
	}

	private void OnPreCull()
	{
		tk2dUpdateManager.FlushQueues();
		this.UpdateCameraMatrix();
	}

	public float GetSizeAtDistance(float distance)
	{
		tk2dCameraSettings tk2dCameraSettings = this.SettingsRoot.CameraSettings;
		tk2dCameraSettings.ProjectionType projection = tk2dCameraSettings.projection;
		if (projection != tk2dCameraSettings.ProjectionType.Orthographic)
		{
			if (projection != tk2dCameraSettings.ProjectionType.Perspective)
			{
				return 1f;
			}
			return Mathf.Tan(this.CameraSettings.fieldOfView * 0.0174532924f * 0.5f) * distance * 2f / (float)this.SettingsRoot.nativeResolutionHeight;
		}
		else
		{
			if (tk2dCameraSettings.orthographicType == tk2dCameraSettings.OrthographicType.PixelsPerMeter)
			{
				return 1f / tk2dCameraSettings.orthographicPixelsPerMeter;
			}
			return 2f * tk2dCameraSettings.orthographicSize / (float)this.SettingsRoot.nativeResolutionHeight;
		}
	}

	public tk2dCamera SettingsRoot
	{
		get
		{
			if (this._settingsRoot == null)
			{
				this._settingsRoot = ((!(this.inheritSettings == null) && !(this.inheritSettings == this)) ? this.inheritSettings.SettingsRoot : this);
			}
			return this._settingsRoot;
		}
	}

	public Matrix4x4 OrthoOffCenter(Vector2 scale, float left, float right, float bottom, float top, float near, float far)
	{
		float value = 2f / (right - left) * scale.x;
		float value2 = 2f / (top - bottom) * scale.y;
		float value3 = -2f / (far - near);
		float value4 = -(right + left) / (right - left);
		float value5 = -(bottom + top) / (top - bottom);
		float value6 = -(far + near) / (far - near);
		Matrix4x4 result = default(Matrix4x4);
		result[0, 0] = value;
		result[0, 1] = 0f;
		result[0, 2] = 0f;
		result[0, 3] = value4;
		result[1, 0] = 0f;
		result[1, 1] = value2;
		result[1, 2] = 0f;
		result[1, 3] = value5;
		result[2, 0] = 0f;
		result[2, 1] = 0f;
		result[2, 2] = value3;
		result[2, 3] = value6;
		result[3, 0] = 0f;
		result[3, 1] = 0f;
		result[3, 2] = 0f;
		result[3, 3] = 1f;
		return result;
	}

	private Vector2 GetScaleForOverride(tk2dCamera settings, tk2dCameraResolutionOverride currentOverride, float width, float height)
	{
		Vector2 one = Vector2.one;
		if (currentOverride == null)
		{
			return one;
		}
		float num;
		switch (currentOverride.autoScaleMode)
		{
		case tk2dCameraResolutionOverride.AutoScaleMode.FitWidth:
			num = width / (float)settings.nativeResolutionWidth;
			one.Set(num, num);
			return one;
		case tk2dCameraResolutionOverride.AutoScaleMode.FitHeight:
			num = height / (float)settings.nativeResolutionHeight;
			one.Set(num, num);
			return one;
		case tk2dCameraResolutionOverride.AutoScaleMode.FitVisible:
		case tk2dCameraResolutionOverride.AutoScaleMode.ClosestMultipleOfTwo:
		{
			float num2 = (float)settings.nativeResolutionWidth / (float)settings.nativeResolutionHeight;
			float num3 = width / height;
			if (num3 < num2)
			{
				num = width / (float)settings.nativeResolutionWidth;
			}
			else
			{
				num = height / (float)settings.nativeResolutionHeight;
			}
			if (currentOverride.autoScaleMode == tk2dCameraResolutionOverride.AutoScaleMode.ClosestMultipleOfTwo)
			{
				if (num > 1f)
				{
					num = Mathf.Floor(num);
				}
				else
				{
					num = Mathf.Pow(2f, Mathf.Floor(Mathf.Log(num, 2f)));
				}
			}
			one.Set(num, num);
			return one;
		}
		case tk2dCameraResolutionOverride.AutoScaleMode.StretchToFit:
			one.Set(width / (float)settings.nativeResolutionWidth, height / (float)settings.nativeResolutionHeight);
			return one;
		case tk2dCameraResolutionOverride.AutoScaleMode.PixelPerfect:
			num = 1f;
			one.Set(num, num);
			return one;
		case tk2dCameraResolutionOverride.AutoScaleMode.Fill:
			num = Mathf.Max(width / (float)settings.nativeResolutionWidth, height / (float)settings.nativeResolutionHeight);
			one.Set(num, num);
			return one;
		}
		num = currentOverride.scale;
		one.Set(num, num);
		return one;
	}

	private Vector2 GetOffsetForOverride(tk2dCamera settings, tk2dCameraResolutionOverride currentOverride, Vector2 scale, float width, float height)
	{
		Vector2 result = Vector2.zero;
		if (currentOverride == null)
		{
			return result;
		}
		tk2dCameraResolutionOverride.FitMode fitMode = currentOverride.fitMode;
		if (fitMode != tk2dCameraResolutionOverride.FitMode.Center)
		{
			if (fitMode != tk2dCameraResolutionOverride.FitMode.Constant)
			{
			}
			result = -currentOverride.offsetPixels;
		}
		else if (settings.cameraSettings.orthographicOrigin == tk2dCameraSettings.OrthographicOrigin.BottomLeft)
		{
			result = new Vector2(Mathf.Round(((float)settings.nativeResolutionWidth * scale.x - width) / 2f), Mathf.Round(((float)settings.nativeResolutionHeight * scale.y - height) / 2f));
		}
		return result;
	}

	private Matrix4x4 GetProjectionMatrixForOverride(tk2dCamera settings, tk2dCameraResolutionOverride currentOverride, float pixelWidth, float pixelHeight, bool halfTexelOffset, out Rect screenExtents, out Rect unscaledScreenExtents)
	{
		Vector2 scaleForOverride = this.GetScaleForOverride(settings, currentOverride, pixelWidth, pixelHeight);
		Vector2 offsetForOverride = this.GetOffsetForOverride(settings, currentOverride, scaleForOverride, pixelWidth, pixelHeight);
		float num = offsetForOverride.x;
		float num2 = offsetForOverride.y;
		float num3 = pixelWidth + offsetForOverride.x;
		float num4 = pixelHeight + offsetForOverride.y;
		Vector2 zero = Vector2.zero;
		bool flag = false;
		if (this.viewportClippingEnabled && this.InheritConfig != null)
		{
			float num5 = (num3 - num) / scaleForOverride.x;
			float num6 = (num4 - num2) / scaleForOverride.y;
			Vector4 vector = new Vector4((float)((int)this.viewportRegion.x), (float)((int)this.viewportRegion.y), (float)((int)this.viewportRegion.z), (float)((int)this.viewportRegion.w));
			flag = true;
			float num7 = -offsetForOverride.x / pixelWidth + vector.x / num5;
			float num8 = -offsetForOverride.y / pixelHeight + vector.y / num6;
			float num9 = vector.z / num5;
			float num10 = vector.w / num6;
			if (settings.cameraSettings.orthographicOrigin == tk2dCameraSettings.OrthographicOrigin.Center)
			{
				num7 += (pixelWidth - (float)settings.nativeResolutionWidth * scaleForOverride.x) / pixelWidth / 2f;
				num8 += (pixelHeight - (float)settings.nativeResolutionHeight * scaleForOverride.y) / pixelHeight / 2f;
			}
			Rect rect = new Rect(num7, num8, num9, num10);
			if (this.UnityCamera.rect.x != num7 || this.UnityCamera.rect.y != num8 || this.UnityCamera.rect.width != num9 || this.UnityCamera.rect.height != num10)
			{
				this.UnityCamera.rect = rect;
			}
			float num11 = Mathf.Min(1f - rect.x, rect.width);
			float num12 = Mathf.Min(1f - rect.y, rect.height);
			float num13 = vector.x * scaleForOverride.x - offsetForOverride.x;
			float num14 = vector.y * scaleForOverride.y - offsetForOverride.y;
			if (settings.cameraSettings.orthographicOrigin == tk2dCameraSettings.OrthographicOrigin.Center)
			{
				num13 -= (float)settings.nativeResolutionWidth * 0.5f * scaleForOverride.x;
				num14 -= (float)settings.nativeResolutionHeight * 0.5f * scaleForOverride.y;
			}
			if (rect.x < 0f)
			{
				num13 += -rect.x * pixelWidth;
				num11 = rect.x + rect.width;
			}
			if (rect.y < 0f)
			{
				num14 += -rect.y * pixelHeight;
				num12 = rect.y + rect.height;
			}
			num += num13;
			num2 += num14;
			num3 = pixelWidth * num11 + offsetForOverride.x + num13;
			num4 = pixelHeight * num12 + offsetForOverride.y + num14;
		}
		else
		{
			if (this.UnityCamera.rect != this.CameraSettings.rect)
			{
				this.UnityCamera.rect = this.CameraSettings.rect;
			}
			if (settings.cameraSettings.orthographicOrigin == tk2dCameraSettings.OrthographicOrigin.Center)
			{
				float num15 = (num3 - num) * 0.5f;
				num -= num15;
				num3 -= num15;
				float num16 = (num4 - num2) * 0.5f;
				num4 -= num16;
				num2 -= num16;
				zero.Set((float)(-(float)this.nativeResolutionWidth) / 2f, (float)(-(float)this.nativeResolutionHeight) / 2f);
			}
		}
		float num17 = 1f / this.ZoomFactor;
		bool flag2 = false;
		bool flag3 = Application.platform == RuntimePlatform.WindowsPlayer || flag2 || Application.platform == RuntimePlatform.WindowsEditor;
		float num18 = (!halfTexelOffset || !flag3 || SystemInfo.graphicsShaderLevel >= 40) ? 0f : 0.5f;
		float num19 = settings.cameraSettings.orthographicSize;
		tk2dCameraSettings.OrthographicType orthographicType = settings.cameraSettings.orthographicType;
		if (orthographicType != tk2dCameraSettings.OrthographicType.OrthographicSize)
		{
			if (orthographicType == tk2dCameraSettings.OrthographicType.PixelsPerMeter)
			{
				num19 = 1f / settings.cameraSettings.orthographicPixelsPerMeter;
			}
		}
		else
		{
			num19 = 2f * settings.cameraSettings.orthographicSize / (float)settings.nativeResolutionHeight;
		}
		if (!flag)
		{
			float num20 = Mathf.Min(this.UnityCamera.rect.width, 1f - this.UnityCamera.rect.x);
			float num21 = Mathf.Min(this.UnityCamera.rect.height, 1f - this.UnityCamera.rect.y);
			if (num20 > 0f && num21 > 0f)
			{
				scaleForOverride.x /= num20;
				scaleForOverride.y /= num21;
			}
		}
		float num22 = num19 * num17;
		screenExtents = new Rect(num * num22 / scaleForOverride.x, num2 * num22 / scaleForOverride.y, (num3 - num) * num22 / scaleForOverride.x, (num4 - num2) * num22 / scaleForOverride.y);
		unscaledScreenExtents = new Rect(zero.x * num22, zero.y * num22, (float)this.nativeResolutionWidth * num22, (float)this.nativeResolutionHeight * num22);
		return this.OrthoOffCenter(scaleForOverride, num19 * (num + num18) * num17, num19 * (num3 + num18) * num17, num19 * (num2 - num18) * num17, num19 * (num4 - num18) * num17, this.UnityCamera.nearClipPlane, this.UnityCamera.farClipPlane);
	}

	private Vector2 GetScreenPixelDimensions(tk2dCamera settings)
	{
		Vector2 result = new Vector2((float)this.ScreenCamera.pixelWidth, (float)this.ScreenCamera.pixelHeight);
		return result;
	}

	private void Upgrade()
	{
		if (this.version != tk2dCamera.CURRENT_VERSION)
		{
			if (this.version == 0)
			{
				this.cameraSettings.orthographicPixelsPerMeter = 1f;
				this.cameraSettings.orthographicType = tk2dCameraSettings.OrthographicType.PixelsPerMeter;
				this.cameraSettings.orthographicOrigin = tk2dCameraSettings.OrthographicOrigin.BottomLeft;
				this.cameraSettings.projection = tk2dCameraSettings.ProjectionType.Orthographic;
				foreach (tk2dCameraResolutionOverride tk2dCameraResolutionOverride in this.resolutionOverride)
				{
					tk2dCameraResolutionOverride.Upgrade(this.version);
				}
				Camera component = base.GetComponent<Camera>();
				if (component != null)
				{
					this.cameraSettings.rect = component.rect;
					if (!component.orthographic)
					{
						this.cameraSettings.projection = tk2dCameraSettings.ProjectionType.Perspective;
						this.cameraSettings.fieldOfView = component.fieldOfView * this.ZoomFactor;
					}
					component.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector);
				}
			}
			UnityEngine.Debug.Log("tk2dCamera '" + base.name + "' - Upgraded from version " + this.version.ToString());
			this.version = tk2dCamera.CURRENT_VERSION;
		}
	}

	public void UpdateCameraMatrix()
	{
		this.Upgrade();
		if (!this.viewportClippingEnabled)
		{
			tk2dCamera.inst = this;
		}
		Camera unityCamera = this.UnityCamera;
		tk2dCamera settingsRoot = this.SettingsRoot;
		tk2dCameraSettings tk2dCameraSettings = settingsRoot.CameraSettings;
		if (unityCamera.rect != this.cameraSettings.rect)
		{
			unityCamera.rect = this.cameraSettings.rect;
		}
		this._targetResolution = this.GetScreenPixelDimensions(settingsRoot);
		if (tk2dCameraSettings.projection == tk2dCameraSettings.ProjectionType.Perspective)
		{
			if (unityCamera.orthographic)
			{
				unityCamera.orthographic = false;
			}
			float num = Mathf.Min(179.9f, tk2dCameraSettings.fieldOfView / Mathf.Max(0.001f, this.ZoomFactor));
			if (unityCamera.fieldOfView != num)
			{
				unityCamera.fieldOfView = num;
			}
			this._screenExtents.Set(-unityCamera.aspect, -1f, unityCamera.aspect * 2f, 2f);
			this._nativeScreenExtents = this._screenExtents;
			unityCamera.ResetProjectionMatrix();
		}
		else
		{
			if (!unityCamera.orthographic)
			{
				unityCamera.orthographic = true;
			}
			Matrix4x4 matrix4x = this.GetProjectionMatrixForOverride(settingsRoot, settingsRoot.CurrentResolutionOverride, this._targetResolution.x, this._targetResolution.y, true, out this._screenExtents, out this._nativeScreenExtents);
			if ((Application.platform == RuntimePlatform.MetroPlayerARM || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerX86) && SystemInfo.deviceType == DeviceType.Handheld && (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight))
			{
				float z = (Screen.orientation != ScreenOrientation.LandscapeRight) ? -90f : 90f;
				Matrix4x4 lhs = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, z), Vector3.one);
				matrix4x = lhs * matrix4x;
			}
			if (unityCamera.projectionMatrix != matrix4x)
			{
				unityCamera.projectionMatrix = matrix4x;
			}
		}
	}

	private static int CURRENT_VERSION = 1;

	public int version;

	[SerializeField]
	private tk2dCameraSettings cameraSettings = new tk2dCameraSettings();

	public tk2dCameraResolutionOverride[] resolutionOverride = new tk2dCameraResolutionOverride[]
	{
		tk2dCameraResolutionOverride.DefaultOverride
	};

	[SerializeField]
	private tk2dCamera inheritSettings;

	public int nativeResolutionWidth = 960;

	public int nativeResolutionHeight = 640;

	[SerializeField]
	private Camera _unityCamera;

	private static tk2dCamera inst;

	private static List<tk2dCamera> allCameras = new List<tk2dCamera>();

	public bool viewportClippingEnabled;

	public Vector4 viewportRegion = new Vector4(0f, 0f, 100f, 100f);

	private Vector2 _targetResolution = Vector2.zero;

	[SerializeField]
	private float zoomFactor = 1f;

	[HideInInspector]
	public bool forceResolutionInEditor;

	[HideInInspector]
	public Vector2 forceResolution = new Vector2(960f, 640f);

	private Rect _screenExtents;

	private Rect _nativeScreenExtents;

	private Rect unitRect = new Rect(0f, 0f, 1f, 1f);

	private tk2dCamera _settingsRoot;
}
