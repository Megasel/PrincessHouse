// dnSpy decompiler from Assembly-CSharp.dll class: tk2dTileMapData
using System;
using System.Collections.Generic;
using tk2dRuntime.TileMap;
using UnityEngine;

public class tk2dTileMapData : ScriptableObject
{
	public int NumLayers
	{
		get
		{
			if (this.tileMapLayers == null || this.tileMapLayers.Count == 0)
			{
				this.InitLayers();
			}
			return this.tileMapLayers.Count;
		}
	}

	public LayerInfo[] Layers
	{
		get
		{
			if (this.tileMapLayers == null || this.tileMapLayers.Count == 0)
			{
				this.InitLayers();
			}
			return this.tileMapLayers.ToArray();
		}
	}

	public TileInfo GetTileInfoForSprite(int tileId)
	{
		if (this.tileInfo == null || tileId < 0 || tileId >= this.tileInfo.Length)
		{
			return null;
		}
		return this.tileInfo[tileId];
	}

	public TileInfo[] GetOrCreateTileInfo(int numTiles)
	{
		return this.tileInfo;
	}

	public void GetTileOffset(out float x, out float y)
	{
		tk2dTileMapData.TileType tileType = this.tileType;
		if (tileType != tk2dTileMapData.TileType.Isometric)
		{
			if (tileType != tk2dTileMapData.TileType.Rectangular)
			{
			}
			x = 0f;
			y = 0f;
		}
		else
		{
			x = 0.5f;
			y = 0f;
		}
	}

	private void InitLayers()
	{
		this.tileMapLayers = new List<LayerInfo>();
		LayerInfo layerInfo = new LayerInfo();
		layerInfo = new LayerInfo();
		layerInfo.name = "Layer 0";
		layerInfo.hash = 1892887448;
		layerInfo.z = 0f;
		this.tileMapLayers.Add(layerInfo);
	}

	public Vector3 tileSize;

	public Vector3 tileOrigin;

	public tk2dTileMapData.TileType tileType;

	public tk2dTileMapData.ColorMode colorMode;

	public tk2dTileMapData.SortMethod sortMethod;

	public bool generateUv2;

	public bool layersFixedZ;

	public bool useSortingLayers;

	public bool usePolygonColliders;

	public GameObject[] tilePrefabs = new GameObject[0];

	[SerializeField]
	private TileInfo[] tileInfo = new TileInfo[0];

	[SerializeField]
	public List<LayerInfo> tileMapLayers = new List<LayerInfo>();

	public enum SortMethod
	{
		BottomLeft,
		TopLeft,
		BottomRight,
		TopRight
	}

	public enum TileType
	{
		Rectangular,
		Isometric
	}

	public enum ColorMode
	{
		Interpolate,
		Solid
	}
}
