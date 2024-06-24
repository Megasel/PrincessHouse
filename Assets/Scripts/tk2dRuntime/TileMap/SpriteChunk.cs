// dnSpy decompiler from Assembly-CSharp.dll class: tk2dRuntime.TileMap.SpriteChunk
using System;
using System.Collections.Generic;
using UnityEngine;

namespace tk2dRuntime.TileMap
{
	[Serializable]
	public class SpriteChunk
	{
		public SpriteChunk()
		{
			this.spriteIds = new int[0];
		}

		public bool Dirty
		{
			get
			{
				return this.dirty;
			}
			set
			{
				this.dirty = value;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.spriteIds.Length == 0;
			}
		}

		public bool HasGameData
		{
			get
			{
				return this.gameObject != null || this.mesh != null || this.meshCollider != null || this.colliderMesh != null || this.edgeColliders.Count > 0;
			}
		}

		public void DestroyGameData(tk2dTileMap tileMap)
		{
			if (this.mesh != null)
			{
				tileMap.DestroyMesh(this.mesh);
			}
			if (this.gameObject != null)
			{
				tk2dUtil.DestroyImmediate(this.gameObject);
			}
			this.gameObject = null;
			this.mesh = null;
			this.DestroyColliderData(tileMap);
		}

		public void DestroyColliderData(tk2dTileMap tileMap)
		{
			if (this.colliderMesh != null)
			{
				tileMap.DestroyMesh(this.colliderMesh);
			}
			if (this.meshCollider != null && this.meshCollider.sharedMesh != null && this.meshCollider.sharedMesh != this.colliderMesh)
			{
				tileMap.DestroyMesh(this.meshCollider.sharedMesh);
			}
			if (this.meshCollider != null)
			{
				tk2dUtil.DestroyImmediate(this.meshCollider);
			}
			this.meshCollider = null;
			this.colliderMesh = null;
			if (this.edgeColliders.Count > 0)
			{
				for (int i = 0; i < this.edgeColliders.Count; i++)
				{
					tk2dUtil.DestroyImmediate(this.edgeColliders[i]);
				}
				this.edgeColliders.Clear();
			}
		}

		private bool dirty;

		public int[] spriteIds;

		public GameObject gameObject;

		public Mesh mesh;

		public MeshCollider meshCollider;

		public Mesh colliderMesh;

		public List<EdgeCollider2D> edgeColliders = new List<EdgeCollider2D>();
	}
}
