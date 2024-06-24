// dnSpy decompiler from Assembly-CSharp.dll class: tk2dCameraResolutionOverride
using System;
using UnityEngine;

[Serializable]
public class tk2dCameraResolutionOverride
{
	public bool Match(int pixelWidth, int pixelHeight)
	{
		tk2dCameraResolutionOverride.MatchByType matchByType = this.matchBy;
		if (matchByType == tk2dCameraResolutionOverride.MatchByType.Wildcard)
		{
			return true;
		}
		if (matchByType == tk2dCameraResolutionOverride.MatchByType.Resolution)
		{
			return pixelWidth == this.width && pixelHeight == this.height;
		}
		if (matchByType != tk2dCameraResolutionOverride.MatchByType.AspectRatio)
		{
			return false;
		}
		float num = (float)pixelHeight / (float)pixelWidth;
		float num2 = num * this.aspectRatioNumerator;
		float num3 = Mathf.Abs(num2 - this.aspectRatioDenominator);
		return num3 < 0.05f;
	}

	public void Upgrade(int version)
	{
		if (version == 0)
		{
			this.matchBy = (((this.width != -1 || this.height != -1) && (this.width != 0 || this.height != 0)) ? tk2dCameraResolutionOverride.MatchByType.Resolution : tk2dCameraResolutionOverride.MatchByType.Wildcard);
		}
	}

	public static tk2dCameraResolutionOverride DefaultOverride
	{
		get
		{
			return new tk2dCameraResolutionOverride
			{
				name = "Override",
				matchBy = tk2dCameraResolutionOverride.MatchByType.Wildcard,
				autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible,
				fitMode = tk2dCameraResolutionOverride.FitMode.Center
			};
		}
	}

	public string name;

	public tk2dCameraResolutionOverride.MatchByType matchBy;

	public int width;

	public int height;

	public float aspectRatioNumerator = 4f;

	public float aspectRatioDenominator = 3f;

	public float scale = 1f;

	public Vector2 offsetPixels = new Vector2(0f, 0f);

	public tk2dCameraResolutionOverride.AutoScaleMode autoScaleMode;

	public tk2dCameraResolutionOverride.FitMode fitMode;

	public enum MatchByType
	{
		Resolution,
		AspectRatio,
		Wildcard
	}

	public enum AutoScaleMode
	{
		None,
		FitWidth,
		FitHeight,
		FitVisible,
		StretchToFit,
		ClosestMultipleOfTwo,
		PixelPerfect,
		Fill
	}

	public enum FitMode
	{
		Constant,
		Center
	}
}
