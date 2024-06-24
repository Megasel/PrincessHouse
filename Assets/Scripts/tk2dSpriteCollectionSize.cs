// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteCollectionSize
using System;

[Serializable]
public class tk2dSpriteCollectionSize
{
	public static tk2dSpriteCollectionSize Explicit(float orthoSize, float targetHeight)
	{
		return tk2dSpriteCollectionSize.ForResolution(orthoSize, targetHeight, targetHeight);
	}

	public static tk2dSpriteCollectionSize PixelsPerMeter(float pixelsPerMeter)
	{
		return new tk2dSpriteCollectionSize
		{
			type = tk2dSpriteCollectionSize.Type.PixelsPerMeter,
			pixelsPerMeter = pixelsPerMeter
		};
	}

	public static tk2dSpriteCollectionSize ForResolution(float orthoSize, float width, float height)
	{
		return new tk2dSpriteCollectionSize
		{
			type = tk2dSpriteCollectionSize.Type.Explicit,
			orthoSize = orthoSize,
			width = width,
			height = height
		};
	}

	public static tk2dSpriteCollectionSize ForTk2dCamera()
	{
		return new tk2dSpriteCollectionSize
		{
			type = tk2dSpriteCollectionSize.Type.PixelsPerMeter,
			pixelsPerMeter = 1f
		};
	}

	public static tk2dSpriteCollectionSize ForTk2dCamera(tk2dCamera camera)
	{
		tk2dSpriteCollectionSize tk2dSpriteCollectionSize = new tk2dSpriteCollectionSize();
		tk2dCameraSettings cameraSettings = camera.SettingsRoot.CameraSettings;
		if (cameraSettings.projection == tk2dCameraSettings.ProjectionType.Orthographic)
		{
			tk2dCameraSettings.OrthographicType orthographicType = cameraSettings.orthographicType;
			if (orthographicType != tk2dCameraSettings.OrthographicType.PixelsPerMeter)
			{
				if (orthographicType == tk2dCameraSettings.OrthographicType.OrthographicSize)
				{
					tk2dSpriteCollectionSize.type = tk2dSpriteCollectionSize.Type.Explicit;
					tk2dSpriteCollectionSize.height = (float)camera.nativeResolutionHeight;
					tk2dSpriteCollectionSize.orthoSize = cameraSettings.orthographicSize;
				}
			}
			else
			{
				tk2dSpriteCollectionSize.type = tk2dSpriteCollectionSize.Type.PixelsPerMeter;
				tk2dSpriteCollectionSize.pixelsPerMeter = cameraSettings.orthographicPixelsPerMeter;
			}
		}
		else if (cameraSettings.projection == tk2dCameraSettings.ProjectionType.Perspective)
		{
			tk2dSpriteCollectionSize.type = tk2dSpriteCollectionSize.Type.PixelsPerMeter;
			tk2dSpriteCollectionSize.pixelsPerMeter = 100f;
		}
		return tk2dSpriteCollectionSize;
	}

	public static tk2dSpriteCollectionSize Default()
	{
		return tk2dSpriteCollectionSize.PixelsPerMeter(100f);
	}

	public void CopyFromLegacy(bool useTk2dCamera, float orthoSize, float targetHeight)
	{
		if (useTk2dCamera)
		{
			this.type = tk2dSpriteCollectionSize.Type.PixelsPerMeter;
			this.pixelsPerMeter = 1f;
		}
		else
		{
			this.type = tk2dSpriteCollectionSize.Type.Explicit;
			this.height = targetHeight;
			this.orthoSize = orthoSize;
		}
	}

	public void CopyFrom(tk2dSpriteCollectionSize source)
	{
		this.type = source.type;
		this.width = source.width;
		this.height = source.height;
		this.orthoSize = source.orthoSize;
		this.pixelsPerMeter = source.pixelsPerMeter;
	}

	public float OrthoSize
	{
		get
		{
			tk2dSpriteCollectionSize.Type type = this.type;
			if (type == tk2dSpriteCollectionSize.Type.Explicit)
			{
				return this.orthoSize;
			}
			if (type != tk2dSpriteCollectionSize.Type.PixelsPerMeter)
			{
				return this.orthoSize;
			}
			return 0.5f;
		}
	}

	public float TargetHeight
	{
		get
		{
			tk2dSpriteCollectionSize.Type type = this.type;
			if (type == tk2dSpriteCollectionSize.Type.Explicit)
			{
				return this.height;
			}
			if (type != tk2dSpriteCollectionSize.Type.PixelsPerMeter)
			{
				return this.height;
			}
			return this.pixelsPerMeter;
		}
	}

	public tk2dSpriteCollectionSize.Type type = tk2dSpriteCollectionSize.Type.PixelsPerMeter;

	public float orthoSize = 10f;

	public float pixelsPerMeter = 100f;

	public float width = 960f;

	public float height = 640f;

	public enum Type
	{
		Explicit,
		PixelsPerMeter
	}
}
