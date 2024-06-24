// dnSpy decompiler from Assembly-CSharp.dll class: tk2dBaseSprite
using System;
using System.Collections.Generic;
using System.Diagnostics;
using tk2dRuntime;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Backend/tk2dBaseSprite")]
public abstract class tk2dBaseSprite : MonoBehaviour, ISpriteCollectionForceBuild
{
	public tk2dSpriteCollectionData Collection
	{
		get
		{
			return this.collection;
		}
		set
		{
			this.collection = value;
			this.collectionInst = this.collection.inst;
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dBaseSprite> SpriteChanged;

	private void InitInstance()
	{
		if (this.collectionInst == null && this.collection != null)
		{
			this.collectionInst = this.collection.inst;
		}
	}

	public Color color
	{
		get
		{
			return this._color;
		}
		set
		{
			if (value != this._color)
			{
				this._color = value;
				this.InitInstance();
				this.UpdateColors();
			}
		}
	}

	public Vector3 scale
	{
		get
		{
			return this._scale;
		}
		set
		{
			if (value != this._scale)
			{
				this._scale = value;
				this.InitInstance();
				this.UpdateVertices();
				this.UpdateCollider();
				if (this.SpriteChanged != null)
				{
					this.SpriteChanged(this);
				}
			}
		}
	}

	private Renderer CachedRenderer
	{
		get
		{
			if (this._cachedRenderer == null)
			{
				this._cachedRenderer = base.GetComponent<Renderer>();
			}
			return this._cachedRenderer;
		}
	}

	public int SortingOrder
	{
		get
		{
			return this.CachedRenderer.sortingOrder;
		}
		set
		{
			if (this.CachedRenderer.sortingOrder != value)
			{
				this.renderLayer = value;
				this.CachedRenderer.sortingOrder = value;
			}
		}
	}

	public bool FlipX
	{
		get
		{
			return this._scale.x < 0f;
		}
		set
		{
			this.scale = new Vector3(Mathf.Abs(this._scale.x) * (float)((!value) ? 1 : -1), this._scale.y, this._scale.z);
		}
	}

	public bool FlipY
	{
		get
		{
			return this._scale.y < 0f;
		}
		set
		{
			this.scale = new Vector3(this._scale.x, Mathf.Abs(this._scale.y) * (float)((!value) ? 1 : -1), this._scale.z);
		}
	}

	public int spriteId
	{
		get
		{
			return this._spriteId;
		}
		set
		{
			if (value != this._spriteId)
			{
				this.InitInstance();
				value = Mathf.Clamp(value, 0, this.collectionInst.spriteDefinitions.Length - 1);
				if (this._spriteId < 0 || this._spriteId >= this.collectionInst.spriteDefinitions.Length || this.GetCurrentVertexCount() != this.collectionInst.spriteDefinitions[value].positions.Length || this.collectionInst.spriteDefinitions[this._spriteId].complexGeometry != this.collectionInst.spriteDefinitions[value].complexGeometry)
				{
					this._spriteId = value;
					this.UpdateGeometry();
				}
				else
				{
					this._spriteId = value;
					this.UpdateVertices();
				}
				this.UpdateMaterial();
				this.UpdateCollider();
				if (this.SpriteChanged != null)
				{
					this.SpriteChanged(this);
				}
			}
		}
	}

	public void SetSprite(int newSpriteId)
	{
		this.spriteId = newSpriteId;
	}

	public bool SetSprite(string spriteName)
	{
		int spriteIdByName = this.collection.GetSpriteIdByName(spriteName, -1);
		if (spriteIdByName != -1)
		{
			this.SetSprite(spriteIdByName);
		}
		else
		{
			UnityEngine.Debug.LogError("SetSprite - Sprite not found in collection: " + spriteName);
		}
		return spriteIdByName != -1;
	}

	public void SetSprite(tk2dSpriteCollectionData newCollection, int newSpriteId)
	{
		bool flag = false;
		if (this.Collection != newCollection)
		{
			this.collection = newCollection;
			this.collectionInst = this.collection.inst;
			this._spriteId = -1;
			flag = true;
		}
		this.spriteId = newSpriteId;
		if (flag)
		{
			this.UpdateMaterial();
		}
	}

	public bool SetSprite(tk2dSpriteCollectionData newCollection, string spriteName)
	{
		int spriteIdByName = newCollection.GetSpriteIdByName(spriteName, -1);
		if (spriteIdByName != -1)
		{
			this.SetSprite(newCollection, spriteIdByName);
		}
		else
		{
			UnityEngine.Debug.LogError("SetSprite - Sprite not found in collection: " + spriteName);
		}
		return spriteIdByName != -1;
	}

	public void MakePixelPerfect()
	{
		float num = 1f;
		tk2dCamera tk2dCamera = tk2dCamera.CameraForLayer(base.gameObject.layer);
		if (tk2dCamera != null)
		{
			if (this.Collection.version < 2)
			{
				UnityEngine.Debug.LogError("Need to rebuild sprite collection.");
			}
			float distance = base.transform.position.z - tk2dCamera.transform.position.z;
			float num2 = this.Collection.invOrthoSize * this.Collection.halfTargetHeight;
			num = tk2dCamera.GetSizeAtDistance(distance) * num2;
		}
		else if (Camera.main)
		{
			if (Camera.main.orthographic)
			{
				num = Camera.main.orthographicSize;
			}
			else
			{
				float zdist = base.transform.position.z - Camera.main.transform.position.z;
				num = tk2dPixelPerfectHelper.CalculateScaleForPerspectiveCamera(Camera.main.fieldOfView, zdist);
			}
			num *= this.Collection.invOrthoSize;
		}
		else
		{
			UnityEngine.Debug.LogError("Main camera not found.");
		}
		this.scale = new Vector3(Mathf.Sign(this.scale.x) * num, Mathf.Sign(this.scale.y) * num, Mathf.Sign(this.scale.z) * num);
	}

	protected abstract void UpdateMaterial();

	protected abstract void UpdateColors();

	protected abstract void UpdateVertices();

	protected abstract void UpdateGeometry();

	protected abstract int GetCurrentVertexCount();

	public abstract void Build();

	public int GetSpriteIdByName(string name)
	{
		this.InitInstance();
		return this.collectionInst.GetSpriteIdByName(name);
	}

	public static T AddComponent<T>(GameObject go, tk2dSpriteCollectionData spriteCollection, int spriteId) where T : tk2dBaseSprite
	{
		T t = go.AddComponent<T>();
		t._spriteId = -1;
		t.SetSprite(spriteCollection, spriteId);
		t.Build();
		return t;
	}

	public static T AddComponent<T>(GameObject go, tk2dSpriteCollectionData spriteCollection, string spriteName) where T : tk2dBaseSprite
	{
		int spriteIdByName = spriteCollection.GetSpriteIdByName(spriteName, -1);
		if (spriteIdByName == -1)
		{
			UnityEngine.Debug.LogError(string.Format("Unable to find sprite named {0} in sprite collection {1}", spriteName, spriteCollection.spriteCollectionName));
			return (T)((object)null);
		}
		return tk2dBaseSprite.AddComponent<T>(go, spriteCollection, spriteIdByName);
	}

	protected int GetNumVertices()
	{
		this.InitInstance();
		return this.collectionInst.spriteDefinitions[this.spriteId].positions.Length;
	}

	protected int GetNumIndices()
	{
		this.InitInstance();
		return this.collectionInst.spriteDefinitions[this.spriteId].indices.Length;
	}

	protected void SetPositions(Vector3[] positions, Vector3[] normals, Vector4[] tangents)
	{
		tk2dSpriteDefinition tk2dSpriteDefinition = this.collectionInst.spriteDefinitions[this.spriteId];
		int numVertices = this.GetNumVertices();
		for (int i = 0; i < numVertices; i++)
		{
			positions[i].x = tk2dSpriteDefinition.positions[i].x * this._scale.x;
			positions[i].y = tk2dSpriteDefinition.positions[i].y * this._scale.y;
			positions[i].z = tk2dSpriteDefinition.positions[i].z * this._scale.z;
		}
		int num = tk2dSpriteDefinition.normals.Length;
		if (normals.Length == num)
		{
			for (int j = 0; j < num; j++)
			{
				normals[j] = tk2dSpriteDefinition.normals[j];
			}
		}
		int num2 = tk2dSpriteDefinition.tangents.Length;
		if (tangents.Length == num2)
		{
			for (int k = 0; k < num2; k++)
			{
				tangents[k] = tk2dSpriteDefinition.tangents[k];
			}
		}
	}

	protected void SetColors(Color32[] dest)
	{
		Color color = this._color;
		if (this.collectionInst.premultipliedAlpha)
		{
			color.r *= color.a;
			color.g *= color.a;
			color.b *= color.a;
		}
		Color32 color2 = color;
		int numVertices = this.GetNumVertices();
		for (int i = 0; i < numVertices; i++)
		{
			dest[i] = color2;
		}
	}

	public Bounds GetBounds()
	{
		this.InitInstance();
		tk2dSpriteDefinition tk2dSpriteDefinition = this.collectionInst.spriteDefinitions[this._spriteId];
		return new Bounds(new Vector3(tk2dSpriteDefinition.boundsData[0].x * this._scale.x, tk2dSpriteDefinition.boundsData[0].y * this._scale.y, tk2dSpriteDefinition.boundsData[0].z * this._scale.z), new Vector3(tk2dSpriteDefinition.boundsData[1].x * Mathf.Abs(this._scale.x), tk2dSpriteDefinition.boundsData[1].y * Mathf.Abs(this._scale.y), tk2dSpriteDefinition.boundsData[1].z * Mathf.Abs(this._scale.z)));
	}

	public Bounds GetUntrimmedBounds()
	{
		this.InitInstance();
		tk2dSpriteDefinition tk2dSpriteDefinition = this.collectionInst.spriteDefinitions[this._spriteId];
		return new Bounds(new Vector3(tk2dSpriteDefinition.untrimmedBoundsData[0].x * this._scale.x, tk2dSpriteDefinition.untrimmedBoundsData[0].y * this._scale.y, tk2dSpriteDefinition.untrimmedBoundsData[0].z * this._scale.z), new Vector3(tk2dSpriteDefinition.untrimmedBoundsData[1].x * Mathf.Abs(this._scale.x), tk2dSpriteDefinition.untrimmedBoundsData[1].y * Mathf.Abs(this._scale.y), tk2dSpriteDefinition.untrimmedBoundsData[1].z * Mathf.Abs(this._scale.z)));
	}

	public static Bounds AdjustedMeshBounds(Bounds bounds, int renderLayer)
	{
		Vector3 center = bounds.center;
		center.z = (float)(-(float)renderLayer) * 0.01f;
		bounds.center = center;
		return bounds;
	}

	public tk2dSpriteDefinition GetCurrentSpriteDef()
	{
		this.InitInstance();
		return (!(this.collectionInst == null)) ? this.collectionInst.spriteDefinitions[this._spriteId] : null;
	}

	public tk2dSpriteDefinition CurrentSprite
	{
		get
		{
			this.InitInstance();
			return (!(this.collectionInst == null)) ? this.collectionInst.spriteDefinitions[this._spriteId] : null;
		}
	}

	public virtual void ReshapeBounds(Vector3 dMin, Vector3 dMax)
	{
	}

	protected virtual bool NeedBoxCollider()
	{
		return false;
	}

	protected virtual void UpdateCollider()
	{
		tk2dSpriteDefinition tk2dSpriteDefinition = this.collectionInst.spriteDefinitions[this._spriteId];
		if (tk2dSpriteDefinition.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics3D)
		{
			if (tk2dSpriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Box && this.boxCollider == null)
			{
				this.boxCollider = base.gameObject.GetComponent<BoxCollider>();
				if (this.boxCollider == null)
				{
					this.boxCollider = base.gameObject.AddComponent<BoxCollider>();
				}
			}
			if (this.boxCollider != null)
			{
				if (tk2dSpriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Box)
				{
					this.boxCollider.center = new Vector3(tk2dSpriteDefinition.colliderVertices[0].x * this._scale.x, tk2dSpriteDefinition.colliderVertices[0].y * this._scale.y, tk2dSpriteDefinition.colliderVertices[0].z * this._scale.z);
					this.boxCollider.size = new Vector3(Mathf.Abs(2f * tk2dSpriteDefinition.colliderVertices[1].x * this._scale.x), Mathf.Abs(2f * tk2dSpriteDefinition.colliderVertices[1].y * this._scale.y), Mathf.Abs(2f * tk2dSpriteDefinition.colliderVertices[1].z * this._scale.z));
				}
				else if (tk2dSpriteDefinition.colliderType != tk2dSpriteDefinition.ColliderType.Unset)
				{
					if (this.boxCollider != null)
					{
						this.boxCollider.center = new Vector3(0f, 0f, -100000f);
						this.boxCollider.size = Vector3.zero;
					}
				}
			}
		}
		else if (tk2dSpriteDefinition.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics2D)
		{
			if (tk2dSpriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Box)
			{
				if (this.boxCollider2D == null)
				{
					this.boxCollider2D = base.gameObject.GetComponent<BoxCollider2D>();
					if (this.boxCollider2D == null)
					{
						this.boxCollider2D = base.gameObject.AddComponent<BoxCollider2D>();
					}
				}
				if (this.polygonCollider2D.Count > 0)
				{
					foreach (PolygonCollider2D polygonCollider2D in this.polygonCollider2D)
					{
						if (polygonCollider2D != null && polygonCollider2D.enabled)
						{
							polygonCollider2D.enabled = false;
						}
					}
				}
				if (this.edgeCollider2D.Count > 0)
				{
					foreach (EdgeCollider2D edgeCollider2D in this.edgeCollider2D)
					{
						if (edgeCollider2D != null && edgeCollider2D.enabled)
						{
							edgeCollider2D.enabled = false;
						}
					}
				}
				if (!this.boxCollider2D.enabled)
				{
					this.boxCollider2D.enabled = true;
				}
				this.boxCollider2D.offset = new Vector2(tk2dSpriteDefinition.colliderVertices[0].x * this._scale.x, tk2dSpriteDefinition.colliderVertices[0].y * this._scale.y);
				this.boxCollider2D.size = new Vector2(Mathf.Abs(2f * tk2dSpriteDefinition.colliderVertices[1].x * this._scale.x), Mathf.Abs(2f * tk2dSpriteDefinition.colliderVertices[1].y * this._scale.y));
			}
			else if (tk2dSpriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Mesh)
			{
				if (this.boxCollider2D != null && this.boxCollider2D.enabled)
				{
					this.boxCollider2D.enabled = false;
				}
				int num = tk2dSpriteDefinition.polygonCollider2D.Length;
				for (int i = 0; i < this.polygonCollider2D.Count; i++)
				{
					if (this.polygonCollider2D[i] == null)
					{
						this.polygonCollider2D[i] = base.gameObject.AddComponent<PolygonCollider2D>();
					}
				}
				while (this.polygonCollider2D.Count < num)
				{
					this.polygonCollider2D.Add(base.gameObject.AddComponent<PolygonCollider2D>());
				}
				for (int j = 0; j < num; j++)
				{
					if (!this.polygonCollider2D[j].enabled)
					{
						this.polygonCollider2D[j].enabled = true;
					}
					if (this._scale.x != 1f || this._scale.y != 1f)
					{
						Vector2[] points = tk2dSpriteDefinition.polygonCollider2D[j].points;
						Vector2[] array = new Vector2[points.Length];
						for (int k = 0; k < points.Length; k++)
						{
							array[k] = Vector2.Scale(points[k], this._scale);
						}
						this.polygonCollider2D[j].points = array;
					}
					else
					{
						this.polygonCollider2D[j].points = tk2dSpriteDefinition.polygonCollider2D[j].points;
					}
				}
				for (int l = num; l < this.polygonCollider2D.Count; l++)
				{
					if (this.polygonCollider2D[l].enabled)
					{
						this.polygonCollider2D[l].enabled = false;
					}
				}
				int num2 = tk2dSpriteDefinition.edgeCollider2D.Length;
				for (int m = 0; m < this.edgeCollider2D.Count; m++)
				{
					if (this.edgeCollider2D[m] == null)
					{
						this.edgeCollider2D[m] = base.gameObject.AddComponent<EdgeCollider2D>();
					}
				}
				while (this.edgeCollider2D.Count < num2)
				{
					this.edgeCollider2D.Add(base.gameObject.AddComponent<EdgeCollider2D>());
				}
				for (int n = 0; n < num2; n++)
				{
					if (!this.edgeCollider2D[n].enabled)
					{
						this.edgeCollider2D[n].enabled = true;
					}
					if (this._scale.x != 1f || this._scale.y != 1f)
					{
						Vector2[] points2 = tk2dSpriteDefinition.edgeCollider2D[n].points;
						Vector2[] array2 = new Vector2[points2.Length];
						for (int num3 = 0; num3 < points2.Length; num3++)
						{
							array2[num3] = Vector2.Scale(points2[num3], this._scale);
						}
						this.edgeCollider2D[n].points = array2;
					}
					else
					{
						this.edgeCollider2D[n].points = tk2dSpriteDefinition.edgeCollider2D[n].points;
					}
				}
				for (int num4 = num2; num4 < this.edgeCollider2D.Count; num4++)
				{
					if (this.edgeCollider2D[num4].enabled)
					{
						this.edgeCollider2D[num4].enabled = false;
					}
				}
			}
			else if (tk2dSpriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.None)
			{
				if (this.boxCollider2D != null && this.boxCollider2D.enabled)
				{
					this.boxCollider2D.enabled = false;
				}
				if (this.polygonCollider2D.Count > 0)
				{
					foreach (PolygonCollider2D polygonCollider2D2 in this.polygonCollider2D)
					{
						if (polygonCollider2D2 != null && polygonCollider2D2.enabled)
						{
							polygonCollider2D2.enabled = false;
						}
					}
				}
				if (this.edgeCollider2D.Count > 0)
				{
					foreach (EdgeCollider2D edgeCollider2D2 in this.edgeCollider2D)
					{
						if (edgeCollider2D2 != null && edgeCollider2D2.enabled)
						{
							edgeCollider2D2.enabled = false;
						}
					}
				}
			}
		}
	}

	protected virtual void CreateCollider()
	{
		tk2dSpriteDefinition tk2dSpriteDefinition = this.collectionInst.spriteDefinitions[this._spriteId];
		if (tk2dSpriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Unset)
		{
			return;
		}
		if (tk2dSpriteDefinition.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics3D)
		{
			if (base.GetComponent<Collider>() != null)
			{
				this.boxCollider = base.GetComponent<BoxCollider>();
				this.meshCollider = base.GetComponent<MeshCollider>();
			}
			if ((this.NeedBoxCollider() || tk2dSpriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Box) && this.meshCollider == null)
			{
				if (this.boxCollider == null)
				{
					this.boxCollider = base.gameObject.AddComponent<BoxCollider>();
				}
			}
			else if (tk2dSpriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Mesh && this.boxCollider == null)
			{
				if (this.meshCollider == null)
				{
					this.meshCollider = base.gameObject.AddComponent<MeshCollider>();
				}
				if (this.meshColliderMesh == null)
				{
					this.meshColliderMesh = new Mesh();
				}
				this.meshColliderMesh.Clear();
				this.meshColliderPositions = new Vector3[tk2dSpriteDefinition.colliderVertices.Length];
				for (int i = 0; i < this.meshColliderPositions.Length; i++)
				{
					this.meshColliderPositions[i] = new Vector3(tk2dSpriteDefinition.colliderVertices[i].x * this._scale.x, tk2dSpriteDefinition.colliderVertices[i].y * this._scale.y, tk2dSpriteDefinition.colliderVertices[i].z * this._scale.z);
				}
				this.meshColliderMesh.vertices = this.meshColliderPositions;
				float num = this._scale.x * this._scale.y * this._scale.z;
				this.meshColliderMesh.triangles = ((num < 0f) ? tk2dSpriteDefinition.colliderIndicesBack : tk2dSpriteDefinition.colliderIndicesFwd);
				this.meshCollider.sharedMesh = this.meshColliderMesh;
				this.meshCollider.convex = tk2dSpriteDefinition.colliderConvex;
				if (base.GetComponent<Rigidbody>())
				{
					base.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
				}
			}
			else if (tk2dSpriteDefinition.colliderType != tk2dSpriteDefinition.ColliderType.None && Application.isPlaying)
			{
				UnityEngine.Debug.LogError("Invalid mesh collider on sprite '" + base.name + "', please remove and try again.");
			}
			this.UpdateCollider();
		}
		else if (tk2dSpriteDefinition.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics2D)
		{
			this.UpdateCollider();
		}
	}

	protected void Awake()
	{
		if (this.collection != null)
		{
			this.collectionInst = this.collection.inst;
		}
		this.CachedRenderer.sortingOrder = this.renderLayer;
	}

	public void CreateSimpleBoxCollider()
	{
		if (this.CurrentSprite == null)
		{
			return;
		}
		if (this.CurrentSprite.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics3D)
		{
			this.boxCollider2D = base.GetComponent<BoxCollider2D>();
			if (this.boxCollider2D != null)
			{
				UnityEngine.Object.DestroyImmediate(this.boxCollider2D, true);
			}
			this.boxCollider = base.GetComponent<BoxCollider>();
			if (this.boxCollider == null)
			{
				this.boxCollider = base.gameObject.AddComponent<BoxCollider>();
			}
		}
		else if (this.CurrentSprite.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics2D)
		{
			this.boxCollider = base.GetComponent<BoxCollider>();
			if (this.boxCollider != null)
			{
				UnityEngine.Object.DestroyImmediate(this.boxCollider, true);
			}
			this.boxCollider2D = base.GetComponent<BoxCollider2D>();
			if (this.boxCollider2D == null)
			{
				this.boxCollider2D = base.gameObject.AddComponent<BoxCollider2D>();
			}
		}
	}

	public bool UsesSpriteCollection(tk2dSpriteCollectionData spriteCollection)
	{
		return this.Collection == spriteCollection;
	}

	public virtual void ForceBuild()
	{
		if (this.collection == null)
		{
			return;
		}
		this.collectionInst = this.collection.inst;
		if (this.spriteId < 0 || this.spriteId >= this.collectionInst.spriteDefinitions.Length)
		{
			this.spriteId = 0;
		}
		this.Build();
		if (this.SpriteChanged != null)
		{
			this.SpriteChanged(this);
		}
	}

	public static GameObject CreateFromTexture<T>(Texture texture, tk2dSpriteCollectionSize size, Rect region, Vector2 anchor) where T : tk2dBaseSprite
	{
		tk2dSpriteCollectionData tk2dSpriteCollectionData = SpriteCollectionGenerator.CreateFromTexture(texture, size, region, anchor);
		if (tk2dSpriteCollectionData == null)
		{
			return null;
		}
		GameObject gameObject = new GameObject();
		tk2dBaseSprite.AddComponent<T>(gameObject, tk2dSpriteCollectionData, 0);
		return gameObject;
	}

	[SerializeField]
	private tk2dSpriteCollectionData collection;

	protected tk2dSpriteCollectionData collectionInst;

	[SerializeField]
	protected Color _color = Color.white;

	[SerializeField]
	protected Vector3 _scale = new Vector3(1f, 1f, 1f);

	[SerializeField]
	protected int _spriteId;

	public BoxCollider2D boxCollider2D;

	public List<PolygonCollider2D> polygonCollider2D = new List<PolygonCollider2D>(1);

	public List<EdgeCollider2D> edgeCollider2D = new List<EdgeCollider2D>(1);

	public BoxCollider boxCollider;

	public MeshCollider meshCollider;

	public Vector3[] meshColliderPositions;

	public Mesh meshColliderMesh;

	private Renderer _cachedRenderer;

	[SerializeField]
	protected int renderLayer;

	public enum Anchor
	{
		LowerLeft,
		LowerCenter,
		LowerRight,
		MiddleLeft,
		MiddleCenter,
		MiddleRight,
		UpperLeft,
		UpperCenter,
		UpperRight
	}
}
