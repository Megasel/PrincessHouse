// dnSpy decompiler from Assembly-CSharp.dll class: tk2dTiledSprite
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Sprite/tk2dTiledSprite")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class tk2dTiledSprite : tk2dBaseSprite
{
	public Vector2 dimensions
	{
		get
		{
			return this._dimensions;
		}
		set
		{
			if (value != this._dimensions)
			{
				this._dimensions = value;
				this.UpdateVertices();
				this.UpdateCollider();
			}
		}
	}

	public tk2dBaseSprite.Anchor anchor
	{
		get
		{
			return this._anchor;
		}
		set
		{
			if (value != this._anchor)
			{
				this._anchor = value;
				this.UpdateVertices();
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
			if (this.boxCollider == null)
			{
				this.boxCollider = base.GetComponent<BoxCollider>();
			}
			if (this.boxCollider2D == null)
			{
				this.boxCollider2D = base.GetComponent<BoxCollider2D>();
			}
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
		int numVertices;
		int num;
		tk2dSpriteGeomGen.GetTiledSpriteGeomDesc(out numVertices, out num, base.CurrentSprite, this.dimensions);
		tk2dSpriteGeomGen.SetSpriteColors(dest, 0, numVertices, this._color, this.collectionInst.premultipliedAlpha);
	}

	public override void Build()
	{
		tk2dSpriteDefinition currentSprite = base.CurrentSprite;
		int num;
		int num2;
		tk2dSpriteGeomGen.GetTiledSpriteGeomDesc(out num, out num2, currentSprite, this.dimensions);
		if (this.meshUvs == null || this.meshUvs.Length != num)
		{
			this.meshUvs = new Vector2[num];
			this.meshVertices = new Vector3[num];
			this.meshColors = new Color32[num];
		}
		if (this.meshIndices == null || this.meshIndices.Length != num2)
		{
			this.meshIndices = new int[num2];
		}
		this.meshNormals = new Vector3[0];
		this.meshTangents = new Vector4[0];
		if (currentSprite.normals != null && currentSprite.normals.Length > 0)
		{
			this.meshNormals = new Vector3[num];
		}
		if (currentSprite.tangents != null && currentSprite.tangents.Length > 0)
		{
			this.meshTangents = new Vector4[num];
		}
		float colliderOffsetZ = (!(this.boxCollider != null)) ? 0f : this.boxCollider.center.z;
		float colliderExtentZ = (!(this.boxCollider != null)) ? 0.5f : (this.boxCollider.size.z * 0.5f);
		tk2dSpriteGeomGen.SetTiledSpriteGeom(this.meshVertices, this.meshUvs, 0, out this.boundsCenter, out this.boundsExtents, currentSprite, this._scale, this.dimensions, this.anchor, colliderOffsetZ, colliderExtentZ);
		tk2dSpriteGeomGen.SetTiledSpriteIndices(this.meshIndices, 0, 0, currentSprite, this.dimensions);
		if (this.meshNormals.Length > 0 || this.meshTangents.Length > 0)
		{
			Vector3 pMin = new Vector3(currentSprite.positions[0].x * this.dimensions.x * currentSprite.texelSize.x * base.scale.x, currentSprite.positions[0].y * this.dimensions.y * currentSprite.texelSize.y * base.scale.y);
			Vector3 pMax = new Vector3(currentSprite.positions[3].x * this.dimensions.x * currentSprite.texelSize.x * base.scale.x, currentSprite.positions[3].y * this.dimensions.y * currentSprite.texelSize.y * base.scale.y);
			tk2dSpriteGeomGen.SetSpriteVertexNormals(this.meshVertices, pMin, pMax, currentSprite.normals, currentSprite.tangents, this.meshNormals, this.meshTangents);
		}
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
		this.mesh.triangles = this.meshIndices;
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
		this.Build();
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
		return 16;
	}

	public override void ReshapeBounds(Vector3 dMin, Vector3 dMax)
	{
		float num = 0.1f;
		tk2dSpriteDefinition currentSprite = base.CurrentSprite;
		Vector2 vector = new Vector2(this._dimensions.x * currentSprite.texelSize.x, this._dimensions.y * currentSprite.texelSize.y);
		Vector3 vector2 = new Vector3(vector.x * this._scale.x, vector.y * this._scale.y);
		Vector3 vector3 = Vector3.zero;
		switch (this._anchor)
		{
		case tk2dBaseSprite.Anchor.LowerLeft:
			vector3.Set(0f, 0f, 0f);
			break;
		case tk2dBaseSprite.Anchor.LowerCenter:
			vector3.Set(0.5f, 0f, 0f);
			break;
		case tk2dBaseSprite.Anchor.LowerRight:
			vector3.Set(1f, 0f, 0f);
			break;
		case tk2dBaseSprite.Anchor.MiddleLeft:
			vector3.Set(0f, 0.5f, 0f);
			break;
		case tk2dBaseSprite.Anchor.MiddleCenter:
			vector3.Set(0.5f, 0.5f, 0f);
			break;
		case tk2dBaseSprite.Anchor.MiddleRight:
			vector3.Set(1f, 0.5f, 0f);
			break;
		case tk2dBaseSprite.Anchor.UpperLeft:
			vector3.Set(0f, 1f, 0f);
			break;
		case tk2dBaseSprite.Anchor.UpperCenter:
			vector3.Set(0.5f, 1f, 0f);
			break;
		case tk2dBaseSprite.Anchor.UpperRight:
			vector3.Set(1f, 1f, 0f);
			break;
		}
		vector3 = Vector3.Scale(vector3, vector2) * -1f;
		Vector3 vector4 = vector2 + dMax - dMin;
		vector4.x /= vector.x;
		vector4.y /= vector.y;
		if (Mathf.Abs(vector.x * vector4.x) < currentSprite.texelSize.x * num && Mathf.Abs(vector4.x) < Mathf.Abs(this._scale.x))
		{
			dMin.x = 0f;
			vector4.x = this._scale.x;
		}
		if (Mathf.Abs(vector.y * vector4.y) < currentSprite.texelSize.y * num && Mathf.Abs(vector4.y) < Mathf.Abs(this._scale.y))
		{
			dMin.y = 0f;
			vector4.y = this._scale.y;
		}
		Vector2 vector5 = new Vector3((!Mathf.Approximately(this._scale.x, 0f)) ? (vector4.x / this._scale.x) : 0f, (!Mathf.Approximately(this._scale.y, 0f)) ? (vector4.y / this._scale.y) : 0f);
		Vector3 b = new Vector3(vector3.x * vector5.x, vector3.y * vector5.y);
		Vector3 position = dMin + vector3 - b;
		position.z = 0f;
		base.transform.position = base.transform.TransformPoint(position);
		this.dimensions = new Vector2(this._dimensions.x * vector5.x, this._dimensions.y * vector5.y);
	}

	private Mesh mesh;

	private Vector2[] meshUvs;

	private Vector3[] meshVertices;

	private Color32[] meshColors;

	private Vector3[] meshNormals;

	private Vector4[] meshTangents;

	private int[] meshIndices;

	[SerializeField]
	private Vector2 _dimensions = new Vector2(50f, 50f);

	[SerializeField]
	private tk2dBaseSprite.Anchor _anchor;

	[SerializeField]
	protected bool _createBoxCollider;

	private Vector3 boundsCenter = Vector3.zero;

	private Vector3 boundsExtents = Vector3.zero;
}
