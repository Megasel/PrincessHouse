// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteCollectionFont
using System;
using UnityEngine;

[Serializable]
public class tk2dSpriteCollectionFont
{
	public void CopyFrom(tk2dSpriteCollectionFont src)
	{
		this.active = src.active;
		this.bmFont = src.bmFont;
		this.texture = src.texture;
		this.dupeCaps = src.dupeCaps;
		this.flipTextureY = src.flipTextureY;
		this.charPadX = src.charPadX;
		this.data = src.data;
		this.editorData = src.editorData;
		this.materialId = src.materialId;
		this.gradientCount = src.gradientCount;
		this.gradientTexture = src.gradientTexture;
		this.useGradient = src.useGradient;
	}

	public string Name
	{
		get
		{
			if (this.bmFont == null || this.texture == null)
			{
				return "Empty";
			}
			if (this.data == null)
			{
				return this.bmFont.name + " (Inactive)";
			}
			return this.bmFont.name;
		}
	}

	public bool InUse
	{
		get
		{
			return this.active && this.bmFont != null && this.texture != null && this.data != null && this.editorData != null;
		}
	}

	public bool active;

	public TextAsset bmFont;

	public Texture2D texture;

	public bool dupeCaps;

	public bool flipTextureY;

	public int charPadX;

	public tk2dFontData data;

	public tk2dFont editorData;

	public int materialId;

	public bool useGradient;

	public Texture2D gradientTexture;

	public int gradientCount = 1;
}
