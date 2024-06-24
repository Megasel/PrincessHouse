// dnSpy decompiler from Assembly-CSharp.dll class: tk2dRuntime.TileMap.Layer
using System;
using UnityEngine;

namespace tk2dRuntime.TileMap
{
	[Serializable]
	public class Layer
	{
		public Layer(int hash, int width, int height, int divX, int divY)
		{
			this.spriteChannel = new SpriteChannel();
			this.Init(hash, width, height, divX, divY);
		}

		public void Init(int hash, int width, int height, int divX, int divY)
		{
			this.divX = divX;
			this.divY = divY;
			this.hash = hash;
			this.numColumns = (width + divX - 1) / divX;
			this.numRows = (height + divY - 1) / divY;
			this.width = width;
			this.height = height;
			this.spriteChannel.chunks = new SpriteChunk[this.numColumns * this.numRows];
			for (int i = 0; i < this.numColumns * this.numRows; i++)
			{
				this.spriteChannel.chunks[i] = new SpriteChunk();
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.spriteChannel.chunks.Length == 0;
			}
		}

		public void Create()
		{
			this.spriteChannel.chunks = new SpriteChunk[this.numColumns * this.numRows];
		}

		public int[] GetChunkData(int x, int y)
		{
			return this.GetChunk(x, y).spriteIds;
		}

		public SpriteChunk GetChunk(int x, int y)
		{
			return this.spriteChannel.chunks[y * this.numColumns + x];
		}

		private SpriteChunk FindChunkAndCoordinate(int x, int y, out int offset)
		{
			int num = x / this.divX;
			int num2 = y / this.divY;
			SpriteChunk result = this.spriteChannel.chunks[num2 * this.numColumns + num];
			int num3 = x - num * this.divX;
			int num4 = y - num2 * this.divY;
			offset = num4 * this.divX + num3;
			return result;
		}

		private bool GetRawTileValue(int x, int y, ref int value)
		{
			int num;
			SpriteChunk spriteChunk = this.FindChunkAndCoordinate(x, y, out num);
			if (spriteChunk.spriteIds == null || spriteChunk.spriteIds.Length == 0)
			{
				return false;
			}
			value = spriteChunk.spriteIds[num];
			return true;
		}

		private void SetRawTileValue(int x, int y, int value)
		{
			int num;
			SpriteChunk spriteChunk = this.FindChunkAndCoordinate(x, y, out num);
			if (spriteChunk != null)
			{
				this.CreateChunk(spriteChunk);
				spriteChunk.spriteIds[num] = value;
				spriteChunk.Dirty = true;
			}
		}

		public void DestroyGameData(tk2dTileMap tilemap)
		{
			foreach (SpriteChunk spriteChunk in this.spriteChannel.chunks)
			{
				if (spriteChunk.HasGameData)
				{
					spriteChunk.DestroyColliderData(tilemap);
					spriteChunk.DestroyGameData(tilemap);
				}
			}
		}

		public int GetTile(int x, int y)
		{
			int num = 0;
			if (this.GetRawTileValue(x, y, ref num) && num != -1)
			{
				return num & 16777215;
			}
			return -1;
		}

		public tk2dTileFlags GetTileFlags(int x, int y)
		{
			int num = 0;
			if (this.GetRawTileValue(x, y, ref num) && num != -1)
			{
				return (tk2dTileFlags)(num & -16777216);
			}
			return tk2dTileFlags.None;
		}

		public int GetRawTile(int x, int y)
		{
			int result = 0;
			if (this.GetRawTileValue(x, y, ref result))
			{
				return result;
			}
			return -1;
		}

		public void SetTile(int x, int y, int tile)
		{
			tk2dTileFlags tileFlags = this.GetTileFlags(x, y);
			int value = (tile != -1) ? (tile | (int)tileFlags) : -1;
			this.SetRawTileValue(x, y, value);
		}

		public void SetTileFlags(int x, int y, tk2dTileFlags flags)
		{
			int tile = this.GetTile(x, y);
			if (tile != -1)
			{
				int value = tile | (int)flags;
				this.SetRawTileValue(x, y, value);
			}
		}

		public void ClearTile(int x, int y)
		{
			this.SetTile(x, y, -1);
		}

		public void SetRawTile(int x, int y, int rawTile)
		{
			this.SetRawTileValue(x, y, rawTile);
		}

		private void CreateChunk(SpriteChunk chunk)
		{
			if (chunk.spriteIds == null || chunk.spriteIds.Length == 0)
			{
				chunk.spriteIds = new int[this.divX * this.divY];
				for (int i = 0; i < this.divX * this.divY; i++)
				{
					chunk.spriteIds[i] = -1;
				}
			}
		}

		private void Optimize(SpriteChunk chunk)
		{
			bool flag = true;
			foreach (int num in chunk.spriteIds)
			{
				if (num != -1)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				chunk.spriteIds = new int[0];
			}
		}

		public void Optimize()
		{
			foreach (SpriteChunk chunk in this.spriteChannel.chunks)
			{
				this.Optimize(chunk);
			}
		}

		public void OptimizeIncremental()
		{
			foreach (SpriteChunk spriteChunk in this.spriteChannel.chunks)
			{
				if (spriteChunk.Dirty)
				{
					this.Optimize(spriteChunk);
				}
			}
		}

		public void ClearDirtyFlag()
		{
			foreach (SpriteChunk spriteChunk in this.spriteChannel.chunks)
			{
				spriteChunk.Dirty = false;
			}
		}

		public int NumActiveChunks
		{
			get
			{
				int num = 0;
				foreach (SpriteChunk spriteChunk in this.spriteChannel.chunks)
				{
					if (!spriteChunk.IsEmpty)
					{
						num++;
					}
				}
				return num;
			}
		}

		public int hash;

		public SpriteChannel spriteChannel;

		private const int tileMask = 16777215;

		private const int flagMask = -16777216;

		public int width;

		public int height;

		public int numColumns;

		public int numRows;

		public int divX;

		public int divY;

		public GameObject gameObject;
	}
}
