// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteCollectionDefinition
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class tk2dSpriteCollectionDefinition
{
	public void CopyFrom(tk2dSpriteCollectionDefinition src)
	{
		this.name = src.name;
		this.disableTrimming = src.disableTrimming;
		this.additive = src.additive;
		this.scale = src.scale;
		this.texture = src.texture;
		this.materialId = src.materialId;
		this.anchor = src.anchor;
		this.anchorX = src.anchorX;
		this.anchorY = src.anchorY;
		this.overrideMesh = src.overrideMesh;
		this.doubleSidedSprite = src.doubleSidedSprite;
		this.customSpriteGeometry = src.customSpriteGeometry;
		this.geometryIslands = src.geometryIslands;
		this.dice = src.dice;
		this.diceUnitX = src.diceUnitX;
		this.diceUnitY = src.diceUnitY;
		this.diceFilter = src.diceFilter;
		this.pad = src.pad;
		this.source = src.source;
		this.fromSpriteSheet = src.fromSpriteSheet;
		this.hasSpriteSheetId = src.hasSpriteSheetId;
		this.spriteSheetX = src.spriteSheetX;
		this.spriteSheetY = src.spriteSheetY;
		this.spriteSheetId = src.spriteSheetId;
		this.extractRegion = src.extractRegion;
		this.regionX = src.regionX;
		this.regionY = src.regionY;
		this.regionW = src.regionW;
		this.regionH = src.regionH;
		this.regionId = src.regionId;
		this.colliderType = src.colliderType;
		this.boxColliderMin = src.boxColliderMin;
		this.boxColliderMax = src.boxColliderMax;
		this.polyColliderCap = src.polyColliderCap;
		this.colliderColor = src.colliderColor;
		this.colliderConvex = src.colliderConvex;
		this.colliderSmoothSphereCollisions = src.colliderSmoothSphereCollisions;
		this.extraPadding = src.extraPadding;
		this.colliderData = new List<tk2dSpriteCollectionDefinition.ColliderData>(src.colliderData.Count);
		foreach (tk2dSpriteCollectionDefinition.ColliderData src2 in src.colliderData)
		{
			tk2dSpriteCollectionDefinition.ColliderData colliderData = new tk2dSpriteCollectionDefinition.ColliderData();
			colliderData.CopyFrom(src2);
			this.colliderData.Add(colliderData);
		}
		if (src.polyColliderIslands != null)
		{
			this.polyColliderIslands = new tk2dSpriteColliderIsland[src.polyColliderIslands.Length];
			for (int i = 0; i < this.polyColliderIslands.Length; i++)
			{
				this.polyColliderIslands[i] = new tk2dSpriteColliderIsland();
				this.polyColliderIslands[i].CopyFrom(src.polyColliderIslands[i]);
			}
		}
		else
		{
			this.polyColliderIslands = new tk2dSpriteColliderIsland[0];
		}
		if (src.geometryIslands != null)
		{
			this.geometryIslands = new tk2dSpriteColliderIsland[src.geometryIslands.Length];
			for (int j = 0; j < this.geometryIslands.Length; j++)
			{
				this.geometryIslands[j] = new tk2dSpriteColliderIsland();
				this.geometryIslands[j].CopyFrom(src.geometryIslands[j]);
			}
		}
		else
		{
			this.geometryIslands = new tk2dSpriteColliderIsland[0];
		}
		this.attachPoints = new List<tk2dSpriteDefinition.AttachPoint>(src.attachPoints.Count);
		foreach (tk2dSpriteDefinition.AttachPoint src3 in src.attachPoints)
		{
			tk2dSpriteDefinition.AttachPoint attachPoint = new tk2dSpriteDefinition.AttachPoint();
			attachPoint.CopyFrom(src3);
			this.attachPoints.Add(attachPoint);
		}
	}

	public void Clear()
	{
		tk2dSpriteCollectionDefinition src = new tk2dSpriteCollectionDefinition();
		this.CopyFrom(src);
	}

	public bool CompareTo(tk2dSpriteCollectionDefinition src)
	{
		if (this.name != src.name)
		{
			return false;
		}
		if (this.additive != src.additive)
		{
			return false;
		}
		if (this.scale != src.scale)
		{
			return false;
		}
		if (this.texture != src.texture)
		{
			return false;
		}
		if (this.materialId != src.materialId)
		{
			return false;
		}
		if (this.anchor != src.anchor)
		{
			return false;
		}
		if (this.anchorX != src.anchorX)
		{
			return false;
		}
		if (this.anchorY != src.anchorY)
		{
			return false;
		}
		if (this.overrideMesh != src.overrideMesh)
		{
			return false;
		}
		if (this.dice != src.dice)
		{
			return false;
		}
		if (this.diceUnitX != src.diceUnitX)
		{
			return false;
		}
		if (this.diceUnitY != src.diceUnitY)
		{
			return false;
		}
		if (this.diceFilter != src.diceFilter)
		{
			return false;
		}
		if (this.pad != src.pad)
		{
			return false;
		}
		if (this.extraPadding != src.extraPadding)
		{
			return false;
		}
		if (this.doubleSidedSprite != src.doubleSidedSprite)
		{
			return false;
		}
		if (this.customSpriteGeometry != src.customSpriteGeometry)
		{
			return false;
		}
		if (this.geometryIslands != src.geometryIslands)
		{
			return false;
		}
		if (this.geometryIslands != null && src.geometryIslands != null)
		{
			if (this.geometryIslands.Length != src.geometryIslands.Length)
			{
				return false;
			}
			for (int i = 0; i < this.geometryIslands.Length; i++)
			{
				if (!this.geometryIslands[i].CompareTo(src.geometryIslands[i]))
				{
					return false;
				}
			}
		}
		if (this.source != src.source)
		{
			return false;
		}
		if (this.fromSpriteSheet != src.fromSpriteSheet)
		{
			return false;
		}
		if (this.hasSpriteSheetId != src.hasSpriteSheetId)
		{
			return false;
		}
		if (this.spriteSheetId != src.spriteSheetId)
		{
			return false;
		}
		if (this.spriteSheetX != src.spriteSheetX)
		{
			return false;
		}
		if (this.spriteSheetY != src.spriteSheetY)
		{
			return false;
		}
		if (this.extractRegion != src.extractRegion)
		{
			return false;
		}
		if (this.regionX != src.regionX)
		{
			return false;
		}
		if (this.regionY != src.regionY)
		{
			return false;
		}
		if (this.regionW != src.regionW)
		{
			return false;
		}
		if (this.regionH != src.regionH)
		{
			return false;
		}
		if (this.regionId != src.regionId)
		{
			return false;
		}
		if (this.colliderType != src.colliderType)
		{
			return false;
		}
		if (this.boxColliderMin != src.boxColliderMin)
		{
			return false;
		}
		if (this.boxColliderMax != src.boxColliderMax)
		{
			return false;
		}
		if (this.polyColliderIslands != src.polyColliderIslands)
		{
			return false;
		}
		if (this.polyColliderIslands != null && src.polyColliderIslands != null)
		{
			if (this.polyColliderIslands.Length != src.polyColliderIslands.Length)
			{
				return false;
			}
			for (int j = 0; j < this.polyColliderIslands.Length; j++)
			{
				if (!this.polyColliderIslands[j].CompareTo(src.polyColliderIslands[j]))
				{
					return false;
				}
			}
		}
		if (this.colliderData.Count != src.colliderData.Count)
		{
			return false;
		}
		for (int k = 0; k < this.colliderData.Count; k++)
		{
			if (!this.colliderData[k].CompareTo(src.colliderData[k]))
			{
				return false;
			}
		}
		if (this.polyColliderCap != src.polyColliderCap)
		{
			return false;
		}
		if (this.colliderColor != src.colliderColor)
		{
			return false;
		}
		if (this.colliderSmoothSphereCollisions != src.colliderSmoothSphereCollisions)
		{
			return false;
		}
		if (this.colliderConvex != src.colliderConvex)
		{
			return false;
		}
		if (this.attachPoints.Count != src.attachPoints.Count)
		{
			return false;
		}
		for (int l = 0; l < this.attachPoints.Count; l++)
		{
			if (!this.attachPoints[l].CompareTo(src.attachPoints[l]))
			{
				return false;
			}
		}
		return true;
	}

	public string name = string.Empty;

	public bool disableTrimming;

	public bool additive;

	public Vector3 scale = new Vector3(1f, 1f, 1f);

	public Texture2D texture;

	[NonSerialized]
	public Texture2D thumbnailTexture;

	public int materialId;

	public tk2dSpriteCollectionDefinition.Anchor anchor = tk2dSpriteCollectionDefinition.Anchor.MiddleCenter;

	public float anchorX;

	public float anchorY;

	public UnityEngine.Object overrideMesh;

	public bool doubleSidedSprite;

	public bool customSpriteGeometry;

	public tk2dSpriteColliderIsland[] geometryIslands = new tk2dSpriteColliderIsland[0];

	public bool dice;

	public int diceUnitX = 64;

	public int diceUnitY = 64;

	public tk2dSpriteCollectionDefinition.DiceFilter diceFilter;

	public tk2dSpriteCollectionDefinition.Pad pad;

	public int extraPadding;

	public tk2dSpriteCollectionDefinition.Source source;

	public bool fromSpriteSheet;

	public bool hasSpriteSheetId;

	public int spriteSheetId;

	public int spriteSheetX;

	public int spriteSheetY;

	public bool extractRegion;

	public int regionX;

	public int regionY;

	public int regionW;

	public int regionH;

	public int regionId;

	public tk2dSpriteCollectionDefinition.ColliderType colliderType;

	public List<tk2dSpriteCollectionDefinition.ColliderData> colliderData = new List<tk2dSpriteCollectionDefinition.ColliderData>();

	public Vector2 boxColliderMin;

	public Vector2 boxColliderMax;

	public tk2dSpriteColliderIsland[] polyColliderIslands;

	public tk2dSpriteCollectionDefinition.PolygonColliderCap polyColliderCap = tk2dSpriteCollectionDefinition.PolygonColliderCap.FrontAndBack;

	public bool colliderConvex;

	public bool colliderSmoothSphereCollisions;

	public tk2dSpriteCollectionDefinition.ColliderColor colliderColor;

	public List<tk2dSpriteDefinition.AttachPoint> attachPoints = new List<tk2dSpriteDefinition.AttachPoint>();

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
		LowerRight,
		Custom
	}

	public enum Pad
	{
		Default,
		BlackZeroAlpha,
		Extend,
		TileXY,
		TileX,
		TileY
	}

	public enum ColliderType
	{
		UserDefined,
		ForceNone,
		BoxTrimmed,
		BoxCustom,
		Polygon,
		Advanced
	}

	public enum PolygonColliderCap
	{
		None,
		FrontAndBack,
		Front,
		Back
	}

	public enum ColliderColor
	{
		Default,
		Red,
		White,
		Black
	}

	public enum Source
	{
		Sprite,
		SpriteSheet,
		Font
	}

	public enum DiceFilter
	{
		Complete,
		SolidOnly,
		TransparentOnly
	}

	[Serializable]
	public class ColliderData
	{
		public void CopyFrom(tk2dSpriteCollectionDefinition.ColliderData src)
		{
			this.name = src.name;
			this.type = src.type;
			this.origin = src.origin;
			this.size = src.size;
			this.angle = src.angle;
		}

		public bool CompareTo(tk2dSpriteCollectionDefinition.ColliderData src)
		{
			return this.name == src.name && this.type == src.type && this.origin == src.origin && this.size == src.size && this.angle == src.angle;
		}

		public string name = string.Empty;

		public tk2dSpriteCollectionDefinition.ColliderData.Type type;

		public Vector2 origin = Vector3.zero;

		public Vector2 size = Vector3.zero;

		public float angle;

		public enum Type
		{
			Box,
			Circle
		}
	}
}
