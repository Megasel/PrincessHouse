// dnSpy decompiler from Assembly-CSharp.dll class: tk2dRuntime.TileMap.RenderMeshBuilder
using System;
using System.Collections.Generic;
using UnityEngine;

namespace tk2dRuntime.TileMap
{
	public static class RenderMeshBuilder
	{
		public static void BuildForChunk(tk2dTileMap tileMap, SpriteChunk chunk, ColorChunk colorChunk, bool useColor, bool skipPrefabs, int baseX, int baseY)
		{
			List<Vector3> list = new List<Vector3>();
			List<Color> list2 = new List<Color>();
			List<Vector2> list3 = new List<Vector2>();
			List<Vector2> list4 = new List<Vector2>();
			int[] spriteIds = chunk.spriteIds;
			Vector3 tileSize = tileMap.data.tileSize;
			int num = tileMap.SpriteCollectionInst.spriteDefinitions.Length;
			UnityEngine.Object[] tilePrefabs = tileMap.data.tilePrefabs;
			tk2dSpriteDefinition firstValidDefinition = tileMap.SpriteCollectionInst.FirstValidDefinition;
			bool flag = firstValidDefinition != null && firstValidDefinition.normals != null && firstValidDefinition.normals.Length > 0;
			bool generateUv = tileMap.data.generateUv2;
			tk2dTileMapData.ColorMode colorMode = tileMap.data.colorMode;
			Color32 c = (!useColor || tileMap.ColorChannel == null) ? Color.white : tileMap.ColorChannel.clearColor;
			if (colorChunk == null || colorChunk.colors.Length == 0)
			{
				useColor = false;
			}
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			BuilderUtil.GetLoopOrder(tileMap.data.sortMethod, tileMap.partitionSizeX, tileMap.partitionSizeY, out num2, out num3, out num4, out num5, out num6, out num7);
			float num8 = 0f;
			float num9 = 0f;
			tileMap.data.GetTileOffset(out num8, out num9);
			List<int>[] array = new List<int>[tileMap.SpriteCollectionInst.materials.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new List<int>();
			}
			int num10 = tileMap.partitionSizeX + 1;
			for (int num11 = num5; num11 != num6; num11 += num7)
			{
				float num12 = (float)(baseY + num11 & 1) * num8;
				for (int num13 = num2; num13 != num3; num13 += num4)
				{
					int rawTile = spriteIds[num11 * tileMap.partitionSizeX + num13];
					int tileFromRawTile = BuilderUtil.GetTileFromRawTile(rawTile);
					bool flag2 = BuilderUtil.IsRawTileFlagSet(rawTile, tk2dTileFlags.FlipX);
					bool flag3 = BuilderUtil.IsRawTileFlagSet(rawTile, tk2dTileFlags.FlipY);
					bool rot = BuilderUtil.IsRawTileFlagSet(rawTile, tk2dTileFlags.Rot90);
					Vector3 a = new Vector3(tileSize.x * ((float)num13 + num12), tileSize.y * (float)num11, 0f);
					if (tileFromRawTile >= 0 && tileFromRawTile < num)
					{
						if (!skipPrefabs || !tilePrefabs[tileFromRawTile])
						{
							tk2dSpriteDefinition tk2dSpriteDefinition = tileMap.SpriteCollectionInst.spriteDefinitions[tileFromRawTile];
							int count = list.Count;
							for (int j = 0; j < tk2dSpriteDefinition.positions.Length; j++)
							{
								Vector3 vector = BuilderUtil.ApplySpriteVertexTileFlags(tileMap, tk2dSpriteDefinition, tk2dSpriteDefinition.positions[j], flag2, flag3, rot);
								if (useColor && colorChunk != null)
								{
									Color color = colorChunk.colors[num11 * num10 + num13];
									Color b = colorChunk.colors[num11 * num10 + num13 + 1];
									Color a2 = colorChunk.colors[(num11 + 1) * num10 + num13];
									Color b2 = colorChunk.colors[(num11 + 1) * num10 + (num13 + 1)];
									if (colorMode != tk2dTileMapData.ColorMode.Interpolate)
									{
										if (colorMode == tk2dTileMapData.ColorMode.Solid)
										{
											list2.Add(color);
										}
									}
									else
									{
										Vector3 a3 = vector - tk2dSpriteDefinition.untrimmedBoundsData[0];
										Vector3 vector2 = a3 + tileMap.data.tileSize * 0.5f;
										float t = Mathf.Clamp01(vector2.x / tileMap.data.tileSize.x);
										float t2 = Mathf.Clamp01(vector2.y / tileMap.data.tileSize.y);
										Color item = Color.Lerp(Color.Lerp(color, b, t), Color.Lerp(a2, b2, t), t2);
										list2.Add(item);
									}
								}
								else
								{
									list2.Add(c);
								}
								if (generateUv)
								{
									if (tk2dSpriteDefinition.normalizedUvs.Length == 0)
									{
										list4.Add(Vector2.zero);
									}
									else
									{
										list4.Add(tk2dSpriteDefinition.normalizedUvs[j]);
									}
								}
								list.Add(a + vector);
								list3.Add(tk2dSpriteDefinition.uvs[j]);
							}
							bool flag4 = false;
							if (flag2)
							{
								flag4 = !flag4;
							}
							if (flag3)
							{
								flag4 = !flag4;
							}
							List<int> list5 = array[tk2dSpriteDefinition.materialId];
							for (int k = 0; k < tk2dSpriteDefinition.indices.Length; k++)
							{
								int num14 = (!flag4) ? k : (tk2dSpriteDefinition.indices.Length - 1 - k);
								list5.Add(count + tk2dSpriteDefinition.indices[num14]);
							}
						}
					}
				}
			}
			if (chunk.mesh == null)
			{
				chunk.mesh = tk2dUtil.CreateMesh();
			}
			chunk.mesh.Clear();
			chunk.mesh.vertices = list.ToArray();
			chunk.mesh.uv = list3.ToArray();
			if (generateUv)
			{
				chunk.mesh.uv2 = list4.ToArray();
			}
			chunk.mesh.colors = list2.ToArray();
			List<Material> list6 = new List<Material>();
			int num15 = 0;
			int num16 = 0;
			foreach (List<int> list7 in array)
			{
				if (list7.Count > 0)
				{
					list6.Add(tileMap.SpriteCollectionInst.materialInsts[num15]);
					num16++;
				}
				num15++;
			}
			if (num16 > 0)
			{
				chunk.mesh.subMeshCount = num16;
				chunk.gameObject.GetComponent<Renderer>().materials = list6.ToArray();
				int num17 = 0;
				foreach (List<int> list8 in array)
				{
					if (list8.Count > 0)
					{
						chunk.mesh.SetTriangles(list8.ToArray(), num17);
						num17++;
					}
				}
			}
			chunk.mesh.RecalculateBounds();
			if (flag)
			{
				chunk.mesh.RecalculateNormals();
			}
			MeshFilter component = chunk.gameObject.GetComponent<MeshFilter>();
			component.sharedMesh = chunk.mesh;
		}

		public static void Build(tk2dTileMap tileMap, bool editMode, bool forceBuild)
		{
			bool skipPrefabs = !editMode;
			bool flag = !forceBuild;
			int numLayers = tileMap.data.NumLayers;
			for (int i = 0; i < numLayers; i++)
			{
				Layer layer = tileMap.Layers[i];
				if (!layer.IsEmpty)
				{
					LayerInfo layerInfo = tileMap.data.Layers[i];
					bool useColor = !tileMap.ColorChannel.IsEmpty && tileMap.data.Layers[i].useColor;
					bool useSortingLayers = tileMap.data.useSortingLayers;
					for (int j = 0; j < layer.numRows; j++)
					{
						int baseY = j * layer.divY;
						for (int k = 0; k < layer.numColumns; k++)
						{
							int baseX = k * layer.divX;
							SpriteChunk chunk = layer.GetChunk(k, j);
							ColorChunk chunk2 = tileMap.ColorChannel.GetChunk(k, j);
							bool flag2 = chunk2 != null && chunk2.Dirty;
							if (!flag || flag2 || chunk.Dirty)
							{
								if (chunk.mesh != null)
								{
									chunk.mesh.Clear();
								}
								if (!chunk.IsEmpty)
								{
									if (editMode || (!editMode && !layerInfo.skipMeshGeneration))
									{
										RenderMeshBuilder.BuildForChunk(tileMap, chunk, chunk2, useColor, skipPrefabs, baseX, baseY);
										if (chunk.gameObject != null && useSortingLayers)
										{
											Renderer component = chunk.gameObject.GetComponent<Renderer>();
											if (component != null)
											{
												component.sortingLayerName = layerInfo.sortingLayerName;
												component.sortingOrder = layerInfo.sortingOrder;
											}
										}
									}
									if (chunk.mesh != null)
									{
										tileMap.TouchMesh(chunk.mesh);
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
