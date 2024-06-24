// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteDefinition
using System;
using UnityEngine;

[Serializable]
public class tk2dSpriteDefinition
{
	public bool Valid
	{
		get
		{
			return this.name.Length != 0;
		}
	}

	public Bounds GetBounds()
	{
		return new Bounds(new Vector3(this.boundsData[0].x, this.boundsData[0].y, this.boundsData[0].z), new Vector3(this.boundsData[1].x, this.boundsData[1].y, this.boundsData[1].z));
	}

	public Bounds GetUntrimmedBounds()
	{
		return new Bounds(new Vector3(this.untrimmedBoundsData[0].x, this.untrimmedBoundsData[0].y, this.untrimmedBoundsData[0].z), new Vector3(this.untrimmedBoundsData[1].x, this.untrimmedBoundsData[1].y, this.untrimmedBoundsData[1].z));
	}

	public string name;

	public Vector3[] boundsData;

	public Vector3[] untrimmedBoundsData;

	public Vector2 texelSize;

	public Vector3[] positions;

	public Vector3[] normals;

	public Vector4[] tangents;

	public Vector2[] uvs;

	public Vector2[] normalizedUvs = new Vector2[0];

	public int[] indices = new int[]
	{
		0,
		3,
		1,
		2,
		3,
		0
	};

	public Material material;

	[NonSerialized]
	public Material materialInst;

	public int materialId;

	public string sourceTextureGUID;

	public bool extractRegion;

	public int regionX;

	public int regionY;

	public int regionW;

	public int regionH;

	public tk2dSpriteDefinition.FlipMode flipped;

	public bool complexGeometry;

	public tk2dSpriteDefinition.PhysicsEngine physicsEngine;

	public tk2dSpriteDefinition.ColliderType colliderType;

	public tk2dSpriteColliderDefinition[] customColliders = new tk2dSpriteColliderDefinition[0];

	public Vector3[] colliderVertices;

	public int[] colliderIndicesFwd;

	public int[] colliderIndicesBack;

	public bool colliderConvex;

	public bool colliderSmoothSphereCollisions;

	public tk2dCollider2DData[] polygonCollider2D = new tk2dCollider2DData[0];

	public tk2dCollider2DData[] edgeCollider2D = new tk2dCollider2DData[0];

	public tk2dSpriteDefinition.AttachPoint[] attachPoints = new tk2dSpriteDefinition.AttachPoint[0];

	public enum ColliderType
	{
		Unset,
		None,
		Box,
		Mesh,
		Custom
	}

	public enum PhysicsEngine
	{
		Physics3D,
		Physics2D
	}

	public enum FlipMode
	{
		None,
		Tk2d,
		TPackerCW
	}

	[Serializable]
	public class AttachPoint
	{
		public void CopyFrom(tk2dSpriteDefinition.AttachPoint src)
		{
			this.name = src.name;
			this.position = src.position;
			this.angle = src.angle;
		}

		public bool CompareTo(tk2dSpriteDefinition.AttachPoint src)
		{
			return this.name == src.name && src.position == this.position && src.angle == this.angle;
		}

		public string name = string.Empty;

		public Vector3 position = Vector3.zero;

		public float angle;
	}
}
