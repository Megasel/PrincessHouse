// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteSheetSource
using System;
using UnityEngine;

[Serializable]
public class tk2dSpriteSheetSource
{
	public void CopyFrom(tk2dSpriteSheetSource src)
	{
		this.texture = src.texture;
		this.tilesX = src.tilesX;
		this.tilesY = src.tilesY;
		this.numTiles = src.numTiles;
		this.anchor = src.anchor;
		this.pad = src.pad;
		this.scale = src.scale;
		this.colliderType = src.colliderType;
		this.version = src.version;
		this.active = src.active;
		this.tileWidth = src.tileWidth;
		this.tileHeight = src.tileHeight;
		this.tileSpacingX = src.tileSpacingX;
		this.tileSpacingY = src.tileSpacingY;
		this.tileMarginX = src.tileMarginX;
		this.tileMarginY = src.tileMarginY;
		this.splitMethod = src.splitMethod;
	}

	public bool CompareTo(tk2dSpriteSheetSource src)
	{
		return !(this.texture != src.texture) && this.tilesX == src.tilesX && this.tilesY == src.tilesY && this.numTiles == src.numTiles && this.anchor == src.anchor && this.pad == src.pad && !(this.scale != src.scale) && this.colliderType == src.colliderType && this.version == src.version && this.active == src.active && this.tileWidth == src.tileWidth && this.tileHeight == src.tileHeight && this.tileSpacingX == src.tileSpacingX && this.tileSpacingY == src.tileSpacingY && this.tileMarginX == src.tileMarginX && this.tileMarginY == src.tileMarginY && this.splitMethod == src.splitMethod;
	}

	public string Name
	{
		get
		{
			return (!(this.texture != null)) ? "New Sprite Sheet" : this.texture.name;
		}
	}

	public Texture2D texture;

	public int tilesX;

	public int tilesY;

	public int numTiles;

	public tk2dSpriteSheetSource.Anchor anchor = tk2dSpriteSheetSource.Anchor.MiddleCenter;

	public tk2dSpriteCollectionDefinition.Pad pad;

	public Vector3 scale = new Vector3(1f, 1f, 1f);

	public bool additive;

	public bool active;

	public int tileWidth;

	public int tileHeight;

	public int tileMarginX;

	public int tileMarginY;

	public int tileSpacingX;

	public int tileSpacingY;

	public tk2dSpriteSheetSource.SplitMethod splitMethod;

	public int version;

	public const int CURRENT_VERSION = 1;

	public tk2dSpriteCollectionDefinition.ColliderType colliderType;

	public enum Anchor
	{
		UpperLeft,
		UpperCenter,
		UpperRight,
		MiddleLeft,
		MiddleCenter,
		MiddleRight,
		LowerLeft,
		LowerCenter,
		LowerRight
	}

	public enum SplitMethod
	{
		UniformDivision
	}
}
