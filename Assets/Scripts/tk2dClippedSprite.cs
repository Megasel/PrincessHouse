// dnSpy decompiler from Assembly-CSharp.dll class: tk2dClippedSprite
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Sprite/tk2dClippedSprite")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class tk2dClippedSprite : tk2dBaseSprite
{
	public Rect ClipRect
	{
		get
		{
			this._clipRect.Set(this._clipBottomLeft.x, this._clipBottomLeft.y, this._clipTopRight.x - this._clipBottomLeft.x, this._clipTopRight.y - this._clipBottomLeft.y);
			return this._clipRect;
		}
		set
		{
			Vector2 vector = new Vector2(value.x, value.y);
			this.clipBottomLeft = vector;
			vector.x += value.width;
			vector.y += value.height;
			this.clipTopRight = vector;
		}
	}

	public Vector2 clipBottomLeft
	{
		get
		{
			return this._clipBottomLeft;
		}
		set
		{
			if (value != this._clipBottomLeft)
			{
				this._clipBottomLeft = new Vector2(value.x, value.y);
				this.Build();
				this.UpdateCollider();
			}
		}
	}

	public Vector2 clipTopRight
	{
		get
		{
			return this._clipTopRight;
		}
		set
		{
			if (value != this._clipTopRight)
			{
				this._clipTopRight = new Vector2(value.x, value.y);
				this.Build();
				this.UpdateCollider();
			}
		}
	}

	public bool CreateBoxCollider
	{
		get
		{
			return this._createBoxCollider;
		}
		set
		{
			if (this._createBoxCollider != value)
			{
				this._createBoxCollider = value;
				this.UpdateCollider();
			}
		}
	}

	private new void Awake()
	{
		base.Awake();
		this.mesh = new Mesh();
		this.mesh.MarkDynamic();
		this.mesh.hideFlags = HideFlags.DontSave;
		base.GetComponent<MeshFilter>().mesh = this.mesh;
		if (base.Collection)
		{
			if (this._spriteId < 0 || this._spriteId >= base.Collection.Count)
			{
				this._spriteId = 0;
			}
			this.Build();
		}
	}

	protected void OnDestroy()
	{
		if (this.mesh)
		{
			UnityEngine.Object.Destroy(this.mesh);
		}
	}

	protected new void SetColors(Color32[] dest)
	{
		if (base.CurrentSprite.positions.Length == 4)
		{
			tk2dSpriteGeomGen.SetSpriteColors(dest, 0, 4, this._color, this.collectionInst.premultipliedAlpha);
		}
	}

	protected void SetGeometry(Vector3[] vertices, Vector2[] uvs)
	{
		tk2dSpriteDefinition currentSprite = base.CurrentSprite;
		float colliderOffsetZ = (!(this.boxCollider != null)) ? 0f : this.boxCollider.center.z;
		float colliderExtentZ = (!(this.boxCollider != null)) ? 0.5f : (this.boxCollider.size.z * 0.5f);
		tk2dSpriteGeomGen.SetClippedSpriteGeom(this.meshVertices, this.meshUvs, 0, out this.boundsCenter, out this.boundsExtents, currentSprite, this._scale, this._clipBottomLeft, this._clipTopRight, colliderOffsetZ, colliderExtentZ);
		if (this.meshNormals.Length > 0 || this.meshTangents.Length > 0)
		{
			tk2dSpriteGeomGen.SetSpriteVertexNormals(this.meshVertices, this.meshVertices[0], this.meshVertices[3], currentSprite.normals, currentSprite.tangents, this.meshNormals, this.meshTangents);
		}
		if (currentSprite.positions.Length != 4 || currentSprite.complexGeometry)
		{
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = Vector3.zero;
			}
		}
	}

	public override void Build()
	{
		tk2dSpriteDefinition currentSprite = base.CurrentSprite;
		this.meshUvs = new Vector2[4];
		this.meshVertices = new Vector3[4];
		this.meshColors = new Color32[4];
		this.meshNormals = new Vector3[0];
		this.meshTangents = new Vector4[0];
		if (currentSprite.normals != null && currentSprite.normals.Length > 0)
		{
			this.meshNormals = new Vector3[4];
		}
		if (currentSprite.tangents != null && currentSprite.tangents.Length > 0)
		{
			this.meshTangents = new Vector4[4];
		}
		this.SetGeometry(this.meshVertices, this.meshUvs);
		this.SetColors(this.meshColors);
		if (this.mesh == null)
		{
			this.mesh = new Mesh();
			this.mesh.MarkDynamic();
			this.mesh.hideFlags = HideFlags.DontSave;
		}
		else
		{
			this.mesh.Clear();
		}
		this.mesh.vertices = this.meshVertices;
		this.mesh.colors32 = this.meshColors;
		this.mesh.uv = this.meshUvs;
		this.mesh.normals = this.meshNormals;
		this.mesh.tangents = this.meshTangents;
		int[] array = new int[6];
		tk2dSpriteGeomGen.SetClippedSpriteIndices(array, 0, 0, base.CurrentSprite);
		this.mesh.triangles = array;
		this.mesh.RecalculateBounds();
		this.mesh.bounds = tk2dBaseSprite.AdjustedMeshBounds(this.mesh.bounds, this.renderLayer);
		base.GetComponent<MeshFilter>().mesh = this.mesh;
		this.UpdateCollider();
		this.UpdateMaterial();
	}

	protected override void UpdateGeometry()
	{
		this.UpdateGeometryImpl();
	}

	protected override void UpdateColors()
	{
		this.UpdateColorsImpl();
	}

	protected override void UpdateVertices()
	{
		this.UpdateGeometryImpl();
	}

	protected void UpdateColorsImpl()
	{
		if (this.meshColors == null || this.meshColors.Length == 0)
		{
			this.Build();
		}
		else
		{
			this.SetColors(this.meshColors);
			this.mesh.colors32 = this.meshColors;
		}
	}

	protected void UpdateGeometryImpl()
	{
		if (this.meshVertices == null || this.meshVertices.Length == 0)
		{
			this.Build();
		}
		else
		{
			this.SetGeometry(this.meshVertices, this.meshUvs);
			this.mesh.vertices = this.meshVertices;
			this.mesh.uv = this.meshUvs;
			this.mesh.normals = this.meshNormals;
			this.mesh.tangents = this.meshTangents;
			this.mesh.RecalculateBounds();
			this.mesh.bounds = tk2dBaseSprite.AdjustedMeshBounds(this.mesh.bounds, this.renderLayer);
		}
	}

	protected override void UpdateCollider()
	{
		if (this.CreateBoxCollider)
		{
			if (base.CurrentSprite.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics3D)
			{
				if (this.boxCollider != null)
				{
					this.boxCollider.size = 2f * this.boundsExtents;
					this.boxCollider.center = this.boundsCenter;
				}
			}
			else if (base.CurrentSprite.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics2D && this.boxCollider2D != null)
			{
				this.boxCollider2D.size = 2f * this.boundsExtents;
				this.boxCollider2D.offset = this.boundsCenter;
			}
		}
	}

	protected override void CreateCollider()
	{
		this.UpdateCollider();
	}

	protected override void UpdateMaterial()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (component.sharedMaterial != this.collectionInst.spriteDefinitions[base.spriteId].materialInst)
		{
			component.material = this.collectionInst.spriteDefinitions[base.spriteId].materialInst;
		}
	}

	protected override int GetCurrentVertexCount()
	{
		return 4;
	}

	public override void ReshapeBounds(Vector3 dMin, Vector3 dMax)
	{
		float num = 0.1f;
		tk2dSpriteDefinition currentSprite = base.CurrentSprite;
		Vector3 b = new Vector3(Mathf.Abs(this._scale.x), Mathf.Abs(this._scale.y), Mathf.Abs(this._scale.z));
		Vector3 b2 = Vector3.Scale(currentSprite.untrimmedBoundsData[0], this._scale) - 0.5f * Vector3.Scale(currentSprite.untrimmedBoundsData[1], b);
		Vector3 a = Vector3.Scale(currentSprite.untrimmedBoundsData[1], b);
		Vector3 vector = a + dMax - dMin;
		vector.x /= currentSprite.untrimmedBoundsData[1].x;
		vector.y /= currentSprite.untrimmedBoundsData[1].y;
		if (currentSprite.untrimmedBoundsData[1].x * vector.x < currentSprite.texelSize.x * num && vector.x < b.x)
		{
			dMin.x = 0f;
			vector.x = b.x;
		}
		if (currentSprite.untrimmedBoundsData[1].y * vector.y < currentSprite.texelSize.y * num && vector.y < b.y)
		{
			dMin.y = 0f;
			vector.y = b.y;
		}
		Vector2 vector2 = new Vector3((!Mathf.Approximately(b.x, 0f)) ? (vector.x / b.x) : 0f, (!Mathf.Approximately(b.y, 0f)) ? (vector.y / b.y) : 0f);
		Vector3 b3 = new Vector3(b2.x * vector2.x, b2.y * vector2.y);
		Vector3 position = dMin + b2 - b3;
		position.z = 0f;
		base.transform.position = base.transform.TransformPoint(position);
		base.scale = new Vector3(this._scale.x * vector2.x, this._scale.y * vector2.y, this._scale.z);
	}

	private Mesh mesh;

	private Vector2[] meshUvs;

	private Vector3[] meshVertices;

	private Color32[] meshColors;

	private Vector3[] meshNormals;

	private Vector4[] meshTangents;

	private int[] meshIndices;

	public Vector2 _clipBottomLeft = new Vector2(0f, 0f);

	public Vector2 _clipTopRight = new Vector2(1f, 1f);

	private Rect _clipRect = new Rect(0f, 0f, 0f, 0f);

	[SerializeField]
	protected bool _createBoxCollider;

	private Vector3 boundsCenter = Vector3.zero;

	private Vector3 boundsExtents = Vector3.zero;
}
