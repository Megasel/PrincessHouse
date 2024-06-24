// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSprite
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Sprite/tk2dSprite")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class tk2dSprite : tk2dBaseSprite
{
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
		if (this.meshColliderMesh)
		{
			UnityEngine.Object.Destroy(this.meshColliderMesh);
		}
	}

	public override void Build()
	{
		tk2dSpriteDefinition tk2dSpriteDefinition = this.collectionInst.spriteDefinitions[base.spriteId];
		this.meshVertices = new Vector3[tk2dSpriteDefinition.positions.Length];
		this.meshColors = new Color32[tk2dSpriteDefinition.positions.Length];
		this.meshNormals = new Vector3[0];
		this.meshTangents = new Vector4[0];
		if (tk2dSpriteDefinition.normals != null && tk2dSpriteDefinition.normals.Length > 0)
		{
			this.meshNormals = new Vector3[tk2dSpriteDefinition.normals.Length];
		}
		if (tk2dSpriteDefinition.tangents != null && tk2dSpriteDefinition.tangents.Length > 0)
		{
			this.meshTangents = new Vector4[tk2dSpriteDefinition.tangents.Length];
		}
		base.SetPositions(this.meshVertices, this.meshNormals, this.meshTangents);
		base.SetColors(this.meshColors);
		if (this.mesh == null)
		{
			this.mesh = new Mesh();
			this.mesh.MarkDynamic();
			this.mesh.hideFlags = HideFlags.DontSave;
			base.GetComponent<MeshFilter>().mesh = this.mesh;
		}
		this.mesh.Clear();
		this.mesh.vertices = this.meshVertices;
		this.mesh.normals = this.meshNormals;
		this.mesh.tangents = this.meshTangents;
		this.mesh.colors32 = this.meshColors;
		this.mesh.uv = tk2dSpriteDefinition.uvs;
		this.mesh.triangles = tk2dSpriteDefinition.indices;
		this.mesh.bounds = tk2dBaseSprite.AdjustedMeshBounds(base.GetBounds(), this.renderLayer);
		this.UpdateMaterial();
		this.CreateCollider();
	}

	public static tk2dSprite AddComponent(GameObject go, tk2dSpriteCollectionData spriteCollection, int spriteId)
	{
		return tk2dBaseSprite.AddComponent<tk2dSprite>(go, spriteCollection, spriteId);
	}

	public static tk2dSprite AddComponent(GameObject go, tk2dSpriteCollectionData spriteCollection, string spriteName)
	{
		return tk2dBaseSprite.AddComponent<tk2dSprite>(go, spriteCollection, spriteName);
	}

	public static GameObject CreateFromTexture(Texture texture, tk2dSpriteCollectionSize size, Rect region, Vector2 anchor)
	{
		return tk2dBaseSprite.CreateFromTexture<tk2dSprite>(texture, size, region, anchor);
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
		this.UpdateVerticesImpl();
	}

	protected void UpdateColorsImpl()
	{
		if (this.mesh == null || this.meshColors == null || this.meshColors.Length == 0)
		{
			return;
		}
		base.SetColors(this.meshColors);
		this.mesh.colors32 = this.meshColors;
	}

	protected void UpdateVerticesImpl()
	{
		tk2dSpriteDefinition tk2dSpriteDefinition = this.collectionInst.spriteDefinitions[base.spriteId];
		if (this.mesh == null || this.meshVertices == null || this.meshVertices.Length == 0)
		{
			return;
		}
		if (tk2dSpriteDefinition.normals.Length != this.meshNormals.Length)
		{
			this.meshNormals = ((tk2dSpriteDefinition.normals == null || tk2dSpriteDefinition.normals.Length <= 0) ? new Vector3[0] : new Vector3[tk2dSpriteDefinition.normals.Length]);
		}
		if (tk2dSpriteDefinition.tangents.Length != this.meshTangents.Length)
		{
			this.meshTangents = ((tk2dSpriteDefinition.tangents == null || tk2dSpriteDefinition.tangents.Length <= 0) ? new Vector4[0] : new Vector4[tk2dSpriteDefinition.tangents.Length]);
		}
		base.SetPositions(this.meshVertices, this.meshNormals, this.meshTangents);
		this.mesh.vertices = this.meshVertices;
		this.mesh.normals = this.meshNormals;
		this.mesh.tangents = this.meshTangents;
		this.mesh.uv = tk2dSpriteDefinition.uvs;
		this.mesh.bounds = tk2dBaseSprite.AdjustedMeshBounds(base.GetBounds(), this.renderLayer);
	}

	protected void UpdateGeometryImpl()
	{
		if (this.mesh == null)
		{
			return;
		}
		tk2dSpriteDefinition tk2dSpriteDefinition = this.collectionInst.spriteDefinitions[base.spriteId];
		if (this.meshVertices == null || this.meshVertices.Length != tk2dSpriteDefinition.positions.Length)
		{
			this.meshVertices = new Vector3[tk2dSpriteDefinition.positions.Length];
			this.meshColors = new Color32[tk2dSpriteDefinition.positions.Length];
		}
		if (this.meshNormals == null || (tk2dSpriteDefinition.normals != null && this.meshNormals.Length != tk2dSpriteDefinition.normals.Length))
		{
			this.meshNormals = new Vector3[tk2dSpriteDefinition.normals.Length];
		}
		else if (tk2dSpriteDefinition.normals == null)
		{
			this.meshNormals = new Vector3[0];
		}
		if (this.meshTangents == null || (tk2dSpriteDefinition.tangents != null && this.meshTangents.Length != tk2dSpriteDefinition.tangents.Length))
		{
			this.meshTangents = new Vector4[tk2dSpriteDefinition.tangents.Length];
		}
		else if (tk2dSpriteDefinition.tangents == null)
		{
			this.meshTangents = new Vector4[0];
		}
		base.SetPositions(this.meshVertices, this.meshNormals, this.meshTangents);
		base.SetColors(this.meshColors);
		this.mesh.Clear();
		this.mesh.vertices = this.meshVertices;
		this.mesh.normals = this.meshNormals;
		this.mesh.tangents = this.meshTangents;
		this.mesh.colors32 = this.meshColors;
		this.mesh.uv = tk2dSpriteDefinition.uvs;
		this.mesh.bounds = tk2dBaseSprite.AdjustedMeshBounds(base.GetBounds(), this.renderLayer);
		this.mesh.triangles = tk2dSpriteDefinition.indices;
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
		if (this.meshVertices == null)
		{
			return 0;
		}
		return this.meshVertices.Length;
	}

	public override void ForceBuild()
	{
		base.ForceBuild();
		base.GetComponent<MeshFilter>().mesh = this.mesh;
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

	private Vector3[] meshVertices;

	private Vector3[] meshNormals;

	private Vector4[] meshTangents;

	private Color32[] meshColors;
}
