// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSlicedSprite
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Sprite/tk2dSlicedSprite")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class tk2dSlicedSprite : tk2dBaseSprite
{
	public bool BorderOnly
	{
		get
		{
			return this._borderOnly;
		}
		set
		{
			if (value != this._borderOnly)
			{
				this._borderOnly = value;
				this.UpdateIndices();
			}
		}
	}

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

	public void SetBorder(float left, float bottom, float right, float top)
	{
		if (this.borderLeft != left || this.borderBottom != bottom || this.borderRight != right || this.borderTop != top)
		{
			this.borderLeft = left;
			this.borderBottom = bottom;
			this.borderRight = right;
			this.borderTop = top;
			this.UpdateVertices();
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
		if (this.boxCollider == null)
		{
			this.boxCollider = base.GetComponent<BoxCollider>();
		}
		if (this.boxCollider2D == null)
		{
			this.boxCollider2D = base.GetComponent<BoxCollider2D>();
		}
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
		tk2dSpriteGeomGen.SetSpriteColors(dest, 0, 16, this._color, this.collectionInst.premultipliedAlpha);
	}

	protected void SetGeometry(Vector3[] vertices, Vector2[] uvs)
	{
		tk2dSpriteDefinition currentSprite = base.CurrentSprite;
		float colliderOffsetZ = (!(this.boxCollider != null)) ? 0f : this.boxCollider.center.z;
		float colliderExtentZ = (!(this.boxCollider != null)) ? 0.5f : (this.boxCollider.size.z * 0.5f);
		tk2dSpriteGeomGen.SetSlicedSpriteGeom(this.meshVertices, this.meshUvs, 0, out this.boundsCenter, out this.boundsExtents, currentSprite, this._scale, this.dimensions, new Vector2(this.borderLeft, this.borderBottom), new Vector2(this.borderRight, this.borderTop), this.anchor, colliderOffsetZ, colliderExtentZ);
		if (this.meshNormals.Length > 0 || this.meshTangents.Length > 0)
		{
			tk2dSpriteGeomGen.SetSpriteVertexNormals(this.meshVertices, this.meshVertices[0], this.meshVertices[15], currentSprite.normals, currentSprite.tangents, this.meshNormals, this.meshTangents);
		}
		if (currentSprite.positions.Length != 4 || currentSprite.complexGeometry)
		{
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = Vector3.zero;
			}
		}
	}

	private void SetIndices()
	{
		int num = (!this._borderOnly) ? 54 : 48;
		this.meshIndices = new int[num];
		tk2dSpriteGeomGen.SetSlicedSpriteIndices(this.meshIndices, 0, 0, base.CurrentSprite, this._borderOnly);
	}

	private bool NearEnough(float value, float compValue, float scale)
	{
		float num = Mathf.Abs(value - compValue);
		return Mathf.Abs(num / scale) < 0.01f;
	}

	private void PermanentUpgradeLegacyMode()
	{
		tk2dSpriteDefinition currentSprite = base.CurrentSprite;
		float x = currentSprite.untrimmedBoundsData[0].x;
		float y = currentSprite.untrimmedBoundsData[0].y;
		float x2 = currentSprite.untrimmedBoundsData[1].x;
		float y2 = currentSprite.untrimmedBoundsData[1].y;
		if (this.NearEnough(x, 0f, x2) && this.NearEnough(y, -y2 / 2f, y2))
		{
			this._anchor = tk2dBaseSprite.Anchor.UpperCenter;
		}
		else if (this.NearEnough(x, 0f, x2) && this.NearEnough(y, 0f, y2))
		{
			this._anchor = tk2dBaseSprite.Anchor.MiddleCenter;
		}
		else if (this.NearEnough(x, 0f, x2) && this.NearEnough(y, y2 / 2f, y2))
		{
			this._anchor = tk2dBaseSprite.Anchor.LowerCenter;
		}
		else if (this.NearEnough(x, -x2 / 2f, x2) && this.NearEnough(y, -y2 / 2f, y2))
		{
			this._anchor = tk2dBaseSprite.Anchor.UpperRight;
		}
		else if (this.NearEnough(x, -x2 / 2f, x2) && this.NearEnough(y, 0f, y2))
		{
			this._anchor = tk2dBaseSprite.Anchor.MiddleRight;
		}
		else if (this.NearEnough(x, -x2 / 2f, x2) && this.NearEnough(y, y2 / 2f, y2))
		{
			this._anchor = tk2dBaseSprite.Anchor.LowerRight;
		}
		else if (this.NearEnough(x, x2 / 2f, x2) && this.NearEnough(y, -y2 / 2f, y2))
		{
			this._anchor = tk2dBaseSprite.Anchor.UpperLeft;
		}
		else if (this.NearEnough(x, x2 / 2f, x2) && this.NearEnough(y, 0f, y2))
		{
			this._anchor = tk2dBaseSprite.Anchor.MiddleLeft;
		}
		else if (this.NearEnough(x, x2 / 2f, x2) && this.NearEnough(y, y2 / 2f, y2))
		{
			this._anchor = tk2dBaseSprite.Anchor.LowerLeft;
		}
		else
		{
			UnityEngine.Debug.LogError("tk2dSlicedSprite (" + base.name + ") error - Unable to determine anchor upgrading from legacy mode. Please fix this manually.");
			this._anchor = tk2dBaseSprite.Anchor.MiddleCenter;
		}
		float num = x2 / currentSprite.texelSize.x;
		float num2 = y2 / currentSprite.texelSize.y;
		this._dimensions.x = this._scale.x * num;
		this._dimensions.y = this._scale.y * num2;
		this._scale.Set(1f, 1f, 1f);
		this.legacyMode = false;
	}

	public override void Build()
	{
		if (this.legacyMode)
		{
			this.PermanentUpgradeLegacyMode();
		}
		tk2dSpriteDefinition currentSprite = base.CurrentSprite;
		this.meshUvs = new Vector2[16];
		this.meshVertices = new Vector3[16];
		this.meshColors = new Color32[16];
		this.meshNormals = new Vector3[0];
		this.meshTangents = new Vector4[0];
		if (currentSprite.normals != null && currentSprite.normals.Length > 0)
		{
			this.meshNormals = new Vector3[16];
		}
		if (currentSprite.tangents != null && currentSprite.tangents.Length > 0)
		{
			this.meshTangents = new Vector4[16];
		}
		this.SetIndices();
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

	private void UpdateIndices()
	{
		if (this.mesh != null)
		{
			this.SetIndices();
			this.mesh.triangles = this.meshIndices;
		}
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
			this.UpdateCollider();
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
	private bool _borderOnly;

	[SerializeField]
	private bool legacyMode;

	public float borderTop = 0.2f;

	public float borderBottom = 0.2f;

	public float borderLeft = 0.2f;

	public float borderRight = 0.2f;

	[SerializeField]
	protected bool _createBoxCollider;

	private Vector3 boundsCenter = Vector3.zero;

	private Vector3 boundsExtents = Vector3.zero;
}
