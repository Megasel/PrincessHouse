// dnSpy decompiler from Assembly-CSharp.dll class: tk2dTileMap
using System;
using System.Collections.Generic;
using tk2dRuntime;
using tk2dRuntime.TileMap;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("2D Toolkit/TileMap/TileMap")]
public class tk2dTileMap : MonoBehaviour, ISpriteCollectionForceBuild
{
	public tk2dSpriteCollectionData Editor__SpriteCollection
	{
		get
		{
			return this.spriteCollection;
		}
		set
		{
			this.spriteCollection = value;
		}
	}

	public tk2dSpriteCollectionData SpriteCollectionInst
	{
		get
		{
			if (this.spriteCollection != null)
			{
				return this.spriteCollection.inst;
			}
			return null;
		}
	}

	public bool AllowEdit
	{
		get
		{
			return this._inEditMode;
		}
	}

	private void Awake()
	{
		bool flag = true;
		if (this.SpriteCollectionInst && (this.SpriteCollectionInst.buildKey != this.spriteCollectionKey || this.SpriteCollectionInst.needMaterialInstance))
		{
			flag = false;
		}
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			if ((Application.isPlaying && this._inEditMode) || !flag)
			{
				this.EndEditMode();
			}
			else if (this.spriteCollection != null && this.data != null && this.renderData == null)
			{
				this.Build(tk2dTileMap.BuildFlags.ForceBuild);
			}
		}
		else if (this._inEditMode)
		{
			UnityEngine.Debug.LogError("Tilemap " + base.name + " is still in edit mode. Please fix.Building overhead will be significant.");
			this.EndEditMode();
		}
		else if (!flag)
		{
			this.Build(tk2dTileMap.BuildFlags.ForceBuild);
		}
		else if (this.spriteCollection != null && this.data != null && this.renderData == null)
		{
			this.Build(tk2dTileMap.BuildFlags.ForceBuild);
		}
	}

	private void OnDestroy()
	{
		if (this.layers != null)
		{
			foreach (Layer layer in this.layers)
			{
				layer.DestroyGameData(this);
			}
		}
		if (this.renderData != null)
		{
			tk2dUtil.DestroyImmediate(this.renderData);
		}
	}

	public void Build()
	{
		this.Build(tk2dTileMap.BuildFlags.Default);
	}

	public void ForceBuild()
	{
		this.Build(tk2dTileMap.BuildFlags.ForceBuild);
	}

	private void ClearSpawnedInstances()
	{
		if (this.layers == null)
		{
			return;
		}
		BuilderUtil.HideTileMapPrefabs(this);
		for (int i = 0; i < this.layers.Length; i++)
		{
			Layer layer = this.layers[i];
			for (int j = 0; j < layer.spriteChannel.chunks.Length; j++)
			{
				SpriteChunk spriteChunk = layer.spriteChannel.chunks[j];
				if (!(spriteChunk.gameObject == null))
				{
					Transform transform = spriteChunk.gameObject.transform;
					List<Transform> list = new List<Transform>();
					for (int k = 0; k < transform.childCount; k++)
					{
						list.Add(transform.GetChild(k));
					}
					for (int l = 0; l < list.Count; l++)
					{
						tk2dUtil.DestroyImmediate(list[l].gameObject);
					}
				}
			}
		}
	}

	private void SetPrefabsRootActive(bool active)
	{
		if (this.prefabsRoot != null)
		{
			tk2dUtil.SetActive(this.prefabsRoot, active);
		}
	}

	public void Build(tk2dTileMap.BuildFlags buildFlags)
	{
		if (this.data != null && this.spriteCollection != null)
		{
			if (this.data.tilePrefabs == null)
			{
				this.data.tilePrefabs = new GameObject[this.SpriteCollectionInst.Count];
			}
			else if (this.data.tilePrefabs.Length != this.SpriteCollectionInst.Count)
			{
				Array.Resize<GameObject>(ref this.data.tilePrefabs, this.SpriteCollectionInst.Count);
			}
			BuilderUtil.InitDataStore(this);
			if (this.SpriteCollectionInst)
			{
				this.SpriteCollectionInst.InitMaterialIds();
			}
			bool flag = (buildFlags & tk2dTileMap.BuildFlags.ForceBuild) != tk2dTileMap.BuildFlags.Default;
			if (this.SpriteCollectionInst && this.SpriteCollectionInst.buildKey != this.spriteCollectionKey)
			{
				flag = true;
			}
			Dictionary<Layer, bool> dictionary = new Dictionary<Layer, bool>();
			if (this.layers != null)
			{
				for (int i = 0; i < this.layers.Length; i++)
				{
					Layer layer = this.layers[i];
					if (layer != null && layer.gameObject != null)
					{
						dictionary[layer] = layer.gameObject.activeSelf;
					}
				}
			}
			if (flag)
			{
				this.ClearSpawnedInstances();
			}
			BuilderUtil.CreateRenderData(this, this._inEditMode, dictionary);
			RenderMeshBuilder.Build(this, this._inEditMode, flag);
			if (!this._inEditMode)
			{
				tk2dSpriteDefinition firstValidDefinition = this.SpriteCollectionInst.FirstValidDefinition;
				if (firstValidDefinition != null && firstValidDefinition.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics2D)
				{
					ColliderBuilder2D.Build(this, flag);
				}
				else
				{
					ColliderBuilder3D.Build(this, flag);
				}
				BuilderUtil.SpawnPrefabs(this, flag);
			}
			foreach (Layer layer2 in this.layers)
			{
				layer2.ClearDirtyFlag();
			}
			if (this.colorChannel != null)
			{
				this.colorChannel.ClearDirtyFlag();
			}
			if (this.SpriteCollectionInst)
			{
				this.spriteCollectionKey = this.SpriteCollectionInst.buildKey;
			}
			return;
		}
	}

	public bool GetTileAtPosition(Vector3 position, out int x, out int y)
	{
		float num;
		float num2;
		bool tileFracAtPosition = this.GetTileFracAtPosition(position, out num, out num2);
		x = (int)num;
		y = (int)num2;
		return tileFracAtPosition;
	}

	public bool GetTileFracAtPosition(Vector3 position, out float x, out float y)
	{
		tk2dTileMapData.TileType tileType = this.data.tileType;
		if (tileType != tk2dTileMapData.TileType.Rectangular)
		{
			if (tileType == tk2dTileMapData.TileType.Isometric)
			{
				if (this.data.tileSize.x != 0f)
				{
					float num = Mathf.Atan2(this.data.tileSize.y, this.data.tileSize.x / 2f);
					Vector3 vector = base.transform.worldToLocalMatrix.MultiplyPoint(position);
					x = (vector.x - this.data.tileOrigin.x) / this.data.tileSize.x;
					y = (vector.y - this.data.tileOrigin.y) / this.data.tileSize.y;
					float num2 = y * 0.5f;
					int num3 = (int)num2;
					float num4 = num2 - (float)num3;
					float num5 = x % 1f;
					x = (float)((int)x);
					y = (float)(num3 * 2);
					if (num5 > 0.5f)
					{
						if (num4 > 0.5f && Mathf.Atan2(1f - num4, (num5 - 0.5f) * 2f) < num)
						{
							y += 1f;
						}
						else if (num4 < 0.5f && Mathf.Atan2(num4, (num5 - 0.5f) * 2f) < num)
						{
							y -= 1f;
						}
					}
					else if (num5 < 0.5f)
					{
						if (num4 > 0.5f && Mathf.Atan2(num4 - 0.5f, num5 * 2f) > num)
						{
							y += 1f;
							x -= 1f;
						}
						if (num4 < 0.5f && Mathf.Atan2(num4, (0.5f - num5) * 2f) < num)
						{
							y -= 1f;
							x -= 1f;
						}
					}
					return x >= 0f && x < (float)this.width && y >= 0f && y < (float)this.height;
				}
			}
			x = 0f;
			y = 0f;
			return false;
		}
		Vector3 vector2 = base.transform.worldToLocalMatrix.MultiplyPoint(position);
		x = (vector2.x - this.data.tileOrigin.x) / this.data.tileSize.x;
		y = (vector2.y - this.data.tileOrigin.y) / this.data.tileSize.y;
		return x >= 0f && x < (float)this.width && y >= 0f && y < (float)this.height;
	}

	public Vector3 GetTilePosition(int x, int y)
	{
		tk2dTileMapData.TileType tileType = this.data.tileType;
		if (tileType == tk2dTileMapData.TileType.Rectangular || tileType != tk2dTileMapData.TileType.Isometric)
		{
			Vector3 point = new Vector3((float)x * this.data.tileSize.x + this.data.tileOrigin.x, (float)y * this.data.tileSize.y + this.data.tileOrigin.y, 0f);
			return base.transform.localToWorldMatrix.MultiplyPoint(point);
		}
		Vector3 point2 = new Vector3(((float)x + (((y & 1) != 0) ? 0.5f : 0f)) * this.data.tileSize.x + this.data.tileOrigin.x, (float)y * this.data.tileSize.y + this.data.tileOrigin.y, 0f);
		return base.transform.localToWorldMatrix.MultiplyPoint(point2);
	}

	public int GetTileIdAtPosition(Vector3 position, int layer)
	{
		if (layer < 0 || layer >= this.layers.Length)
		{
			return -1;
		}
		int x;
		int y;
		if (!this.GetTileAtPosition(position, out x, out y))
		{
			return -1;
		}
		return this.layers[layer].GetTile(x, y);
	}

	public TileInfo GetTileInfoForTileId(int tileId)
	{
		return this.data.GetTileInfoForSprite(tileId);
	}

	public Color GetInterpolatedColorAtPosition(Vector3 position)
	{
		Vector3 vector = base.transform.worldToLocalMatrix.MultiplyPoint(position);
		int num = (int)((vector.x - this.data.tileOrigin.x) / this.data.tileSize.x);
		int num2 = (int)((vector.y - this.data.tileOrigin.y) / this.data.tileSize.y);
		if (this.colorChannel == null || this.colorChannel.IsEmpty)
		{
			return Color.white;
		}
		if (num < 0 || num >= this.width || num2 < 0 || num2 >= this.height)
		{
			return this.colorChannel.clearColor;
		}
		int num3;
		ColorChunk colorChunk = this.colorChannel.FindChunkAndCoordinate(num, num2, out num3);
		if (colorChunk.Empty)
		{
			return this.colorChannel.clearColor;
		}
		int num4 = this.partitionSizeX + 1;
		Color a = colorChunk.colors[num3];
		Color b = colorChunk.colors[num3 + 1];
		Color a2 = colorChunk.colors[num3 + num4];
		Color b2 = colorChunk.colors[num3 + num4 + 1];
		float num5 = (float)num * this.data.tileSize.x + this.data.tileOrigin.x;
		float num6 = (float)num2 * this.data.tileSize.y + this.data.tileOrigin.y;
		float t = (vector.x - num5) / this.data.tileSize.x;
		float t2 = (vector.y - num6) / this.data.tileSize.y;
		Color a3 = Color.Lerp(a, b, t);
		Color b3 = Color.Lerp(a2, b2, t);
		return Color.Lerp(a3, b3, t2);
	}

	public bool UsesSpriteCollection(tk2dSpriteCollectionData spriteCollection)
	{
		return this.spriteCollection != null && (spriteCollection == this.spriteCollection || spriteCollection == this.spriteCollection.inst);
	}

	public void EndEditMode()
	{
		this._inEditMode = false;
		this.SetPrefabsRootActive(true);
		this.Build(tk2dTileMap.BuildFlags.ForceBuild);
		if (this.prefabsRoot != null)
		{
			tk2dUtil.DestroyImmediate(this.prefabsRoot);
			this.prefabsRoot = null;
		}
	}

	public void TouchMesh(Mesh mesh)
	{
	}

	public void DestroyMesh(Mesh mesh)
	{
		tk2dUtil.DestroyImmediate(mesh);
	}

	public int GetTilePrefabsListCount()
	{
		return this.tilePrefabsList.Count;
	}

	public List<tk2dTileMap.TilemapPrefabInstance> TilePrefabsList
	{
		get
		{
			return this.tilePrefabsList;
		}
	}

	public void GetTilePrefabsListItem(int index, out int x, out int y, out int layer, out GameObject instance)
	{
		tk2dTileMap.TilemapPrefabInstance tilemapPrefabInstance = this.tilePrefabsList[index];
		x = tilemapPrefabInstance.x;
		y = tilemapPrefabInstance.y;
		layer = tilemapPrefabInstance.layer;
		instance = tilemapPrefabInstance.instance;
	}

	public void SetTilePrefabsList(List<int> xs, List<int> ys, List<int> layers, List<GameObject> instances)
	{
		int count = instances.Count;
		this.tilePrefabsList = new List<tk2dTileMap.TilemapPrefabInstance>(count);
		for (int i = 0; i < count; i++)
		{
			tk2dTileMap.TilemapPrefabInstance tilemapPrefabInstance = new tk2dTileMap.TilemapPrefabInstance();
			tilemapPrefabInstance.x = xs[i];
			tilemapPrefabInstance.y = ys[i];
			tilemapPrefabInstance.layer = layers[i];
			tilemapPrefabInstance.instance = instances[i];
			this.tilePrefabsList.Add(tilemapPrefabInstance);
		}
	}

	public Layer[] Layers
	{
		get
		{
			return this.layers;
		}
		set
		{
			this.layers = value;
		}
	}

	public ColorChannel ColorChannel
	{
		get
		{
			return this.colorChannel;
		}
		set
		{
			this.colorChannel = value;
		}
	}

	public GameObject PrefabsRoot
	{
		get
		{
			return this.prefabsRoot;
		}
		set
		{
			this.prefabsRoot = value;
		}
	}

	public int GetTile(int x, int y, int layer)
	{
		if (layer < 0 || layer >= this.layers.Length)
		{
			return -1;
		}
		return this.layers[layer].GetTile(x, y);
	}

	public tk2dTileFlags GetTileFlags(int x, int y, int layer)
	{
		if (layer < 0 || layer >= this.layers.Length)
		{
			return tk2dTileFlags.None;
		}
		return this.layers[layer].GetTileFlags(x, y);
	}

	public void SetTile(int x, int y, int layer, int tile)
	{
		if (layer < 0 || layer >= this.layers.Length)
		{
			return;
		}
		this.layers[layer].SetTile(x, y, tile);
	}

	public void SetTileFlags(int x, int y, int layer, tk2dTileFlags flags)
	{
		if (layer < 0 || layer >= this.layers.Length)
		{
			return;
		}
		this.layers[layer].SetTileFlags(x, y, flags);
	}

	public void ClearTile(int x, int y, int layer)
	{
		if (layer < 0 || layer >= this.layers.Length)
		{
			return;
		}
		this.layers[layer].ClearTile(x, y);
	}

	public string editorDataGUID = string.Empty;

	public tk2dTileMapData data;

	public GameObject renderData;

	[SerializeField]
	private tk2dSpriteCollectionData spriteCollection;

	[SerializeField]
	private int spriteCollectionKey;

	public int width = 128;

	public int height = 128;

	public int partitionSizeX = 32;

	public int partitionSizeY = 32;

	[SerializeField]
	private Layer[] layers;

	[SerializeField]
	private ColorChannel colorChannel;

	[SerializeField]
	private GameObject prefabsRoot;

	[SerializeField]
	private List<tk2dTileMap.TilemapPrefabInstance> tilePrefabsList = new List<tk2dTileMap.TilemapPrefabInstance>();

	[SerializeField]
	private bool _inEditMode;

	public string serializedMeshPath;

	[Serializable]
	public class TilemapPrefabInstance
	{
		public int x;

		public int y;

		public int layer;

		public GameObject instance;
	}

	[Flags]
	public enum BuildFlags
	{
		Default = 0,
		EditMode = 1,
		ForceBuild = 2
	}
}
