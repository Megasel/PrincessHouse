// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIMask
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/Core/tk2dUIMask")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class tk2dUIMask : MonoBehaviour
{
	private MeshFilter ThisMeshFilter
	{
		get
		{
			if (this._thisMeshFilter == null)
			{
				this._thisMeshFilter = base.GetComponent<MeshFilter>();
			}
			return this._thisMeshFilter;
		}
	}

	private BoxCollider ThisBoxCollider
	{
		get
		{
			if (this._thisBoxCollider == null)
			{
				this._thisBoxCollider = base.GetComponent<BoxCollider>();
			}
			return this._thisBoxCollider;
		}
	}

	private void Awake()
	{
		this.Build();
	}

	private void OnDestroy()
	{
		if (this.ThisMeshFilter.sharedMesh != null)
		{
			UnityEngine.Object.Destroy(this.ThisMeshFilter.sharedMesh);
		}
	}

	private Mesh FillMesh(Mesh mesh)
	{
		Vector3 zero = Vector3.zero;
		switch (this.anchor)
		{
		case tk2dBaseSprite.Anchor.LowerLeft:
			zero = new Vector3(0f, 0f, 0f);
			break;
		case tk2dBaseSprite.Anchor.LowerCenter:
			zero = new Vector3(-this.size.x / 2f, 0f, 0f);
			break;
		case tk2dBaseSprite.Anchor.LowerRight:
			zero = new Vector3(-this.size.x, 0f, 0f);
			break;
		case tk2dBaseSprite.Anchor.MiddleLeft:
			zero = new Vector3(0f, -this.size.y / 2f, 0f);
			break;
		case tk2dBaseSprite.Anchor.MiddleCenter:
			zero = new Vector3(-this.size.x / 2f, -this.size.y / 2f, 0f);
			break;
		case tk2dBaseSprite.Anchor.MiddleRight:
			zero = new Vector3(-this.size.x, -this.size.y / 2f, 0f);
			break;
		case tk2dBaseSprite.Anchor.UpperLeft:
			zero = new Vector3(0f, -this.size.y, 0f);
			break;
		case tk2dBaseSprite.Anchor.UpperCenter:
			zero = new Vector3(-this.size.x / 2f, -this.size.y, 0f);
			break;
		case tk2dBaseSprite.Anchor.UpperRight:
			zero = new Vector3(-this.size.x, -this.size.y, 0f);
			break;
		}
		Vector3[] vertices = new Vector3[]
		{
			zero + new Vector3(0f, 0f, -this.depth),
			zero + new Vector3(this.size.x, 0f, -this.depth),
			zero + new Vector3(0f, this.size.y, -this.depth),
			zero + new Vector3(this.size.x, this.size.y, -this.depth)
		};
		mesh.vertices = vertices;
		mesh.uv = tk2dUIMask.uv;
		mesh.triangles = tk2dUIMask.indices;
		Bounds bounds = default(Bounds);
		bounds.SetMinMax(zero, zero + new Vector3(this.size.x, this.size.y, 0f));
		mesh.bounds = bounds;
		return mesh;
	}

	private void OnDrawGizmosSelected()
	{
		Mesh sharedMesh = this.ThisMeshFilter.sharedMesh;
		if (sharedMesh != null)
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Bounds bounds = sharedMesh.bounds;
			Gizmos.color = new Color32(56, 146, 227, 96);
			float num = -this.depth * 1.001f;
			Vector3 center = new Vector3(bounds.center.x, bounds.center.y, num * 0.5f);
			Vector3 vector = new Vector3(bounds.extents.x * 2f, bounds.extents.y * 2f, Mathf.Abs(num));
			Gizmos.DrawCube(center, vector);
			Gizmos.color = new Color32(22, 145, byte.MaxValue, byte.MaxValue);
			Gizmos.DrawWireCube(center, vector);
		}
	}

	public void Build()
	{
		if (this.ThisMeshFilter.sharedMesh == null)
		{
			Mesh mesh = new Mesh();
			mesh.MarkDynamic();
			mesh.hideFlags = HideFlags.DontSave;
			this.ThisMeshFilter.mesh = this.FillMesh(mesh);
		}
		else
		{
			this.FillMesh(this.ThisMeshFilter.sharedMesh);
		}
		if (this.createBoxCollider)
		{
			if (this.ThisBoxCollider == null)
			{
				this._thisBoxCollider = base.gameObject.AddComponent<BoxCollider>();
			}
			Bounds bounds = this.ThisMeshFilter.sharedMesh.bounds;
			this.ThisBoxCollider.center = new Vector3(bounds.center.x, bounds.center.y, -this.depth);
			this.ThisBoxCollider.size = new Vector3(bounds.size.x, bounds.size.y, 0.0002f);
		}
		else if (this.ThisBoxCollider != null)
		{
			UnityEngine.Object.Destroy(this.ThisBoxCollider);
		}
	}

	public void ReshapeBounds(Vector3 dMin, Vector3 dMax)
	{
		Vector3 vector = new Vector3(this.size.x, this.size.y);
		Vector3 a = Vector3.zero;
		switch (this.anchor)
		{
		case tk2dBaseSprite.Anchor.LowerLeft:
			a.Set(0f, 0f, 0f);
			break;
		case tk2dBaseSprite.Anchor.LowerCenter:
			a.Set(0.5f, 0f, 0f);
			break;
		case tk2dBaseSprite.Anchor.LowerRight:
			a.Set(1f, 0f, 0f);
			break;
		case tk2dBaseSprite.Anchor.MiddleLeft:
			a.Set(0f, 0.5f, 0f);
			break;
		case tk2dBaseSprite.Anchor.MiddleCenter:
			a.Set(0.5f, 0.5f, 0f);
			break;
		case tk2dBaseSprite.Anchor.MiddleRight:
			a.Set(1f, 0.5f, 0f);
			break;
		case tk2dBaseSprite.Anchor.UpperLeft:
			a.Set(0f, 1f, 0f);
			break;
		case tk2dBaseSprite.Anchor.UpperCenter:
			a.Set(0.5f, 1f, 0f);
			break;
		case tk2dBaseSprite.Anchor.UpperRight:
			a.Set(1f, 1f, 0f);
			break;
		}
		a = Vector3.Scale(a, vector) * -1f;
		Vector3 vector2 = vector + dMax - dMin;
		Vector3 b = new Vector3((!Mathf.Approximately(vector.x, 0f)) ? (a.x * vector2.x / vector.x) : 0f, (!Mathf.Approximately(vector.y, 0f)) ? (a.y * vector2.y / vector.y) : 0f);
		Vector3 position = a + dMin - b;
		position.z = 0f;
		base.transform.position = base.transform.TransformPoint(position);
		this.size = new Vector2(vector2.x, vector2.y);
		this.Build();
	}

	public tk2dBaseSprite.Anchor anchor = tk2dBaseSprite.Anchor.MiddleCenter;

	public Vector2 size = new Vector2(1f, 1f);

	public float depth = 1f;

	public bool createBoxCollider = true;

	private MeshFilter _thisMeshFilter;

	private BoxCollider _thisBoxCollider;

	private static readonly Vector2[] uv = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f)
	};

	private static readonly int[] indices = new int[]
	{
		0,
		3,
		1,
		2,
		3,
		0
	};
}
