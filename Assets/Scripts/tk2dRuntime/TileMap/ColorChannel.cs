// dnSpy decompiler from Assembly-CSharp.dll class: tk2dRuntime.TileMap.ColorChannel
using System;
using UnityEngine;

namespace tk2dRuntime.TileMap
{
	[Serializable]
	public class ColorChannel
	{
		public ColorChannel(int width, int height, int divX, int divY)
		{
			this.Init(width, height, divX, divY);
		}

		public ColorChannel()
		{
			this.chunks = new ColorChunk[0];
		}

		public void Init(int width, int height, int divX, int divY)
		{
			this.numColumns = (width + divX - 1) / divX;
			this.numRows = (height + divY - 1) / divY;
			this.chunks = new ColorChunk[0];
			this.divX = divX;
			this.divY = divY;
		}

		public ColorChunk FindChunkAndCoordinate(int x, int y, out int offset)
		{
			int num = x / this.divX;
			int num2 = y / this.divY;
			num = Mathf.Clamp(num, 0, this.numColumns - 1);
			num2 = Mathf.Clamp(num2, 0, this.numRows - 1);
			int num3 = num2 * this.numColumns + num;
			ColorChunk result = this.chunks[num3];
			int num4 = x - num * this.divX;
			int num5 = y - num2 * this.divY;
			offset = num5 * (this.divX + 1) + num4;
			return result;
		}

		public Color GetColor(int x, int y)
		{
			if (this.IsEmpty)
			{
				return this.clearColor;
			}
			int num;
			ColorChunk colorChunk = this.FindChunkAndCoordinate(x, y, out num);
			if (colorChunk.colors.Length == 0)
			{
				return this.clearColor;
			}
			return colorChunk.colors[num];
		}

		private void InitChunk(ColorChunk chunk)
		{
			if (chunk.colors.Length == 0)
			{
				chunk.colors = new Color32[(this.divX + 1) * (this.divY + 1)];
				for (int i = 0; i < chunk.colors.Length; i++)
				{
					chunk.colors[i] = this.clearColor;
				}
			}
		}

		public void SetColor(int x, int y, Color color)
		{
			if (this.IsEmpty)
			{
				this.Create();
			}
			int num = this.divX + 1;
			int num2 = Mathf.Max(x - 1, 0) / this.divX;
			int num3 = Mathf.Max(y - 1, 0) / this.divY;
			ColorChunk chunk = this.GetChunk(num2, num3, true);
			int num4 = x - num2 * this.divX;
			int num5 = y - num3 * this.divY;
			chunk.colors[num5 * num + num4] = color;
			chunk.Dirty = true;
			bool flag = false;
			bool flag2 = false;
			if (x != 0 && x % this.divX == 0 && num2 + 1 < this.numColumns)
			{
				flag = true;
			}
			if (y != 0 && y % this.divY == 0 && num3 + 1 < this.numRows)
			{
				flag2 = true;
			}
			if (flag)
			{
				int num6 = num2 + 1;
				chunk = this.GetChunk(num6, num3, true);
				num4 = x - num6 * this.divX;
				num5 = y - num3 * this.divY;
				chunk.colors[num5 * num + num4] = color;
				chunk.Dirty = true;
			}
			if (flag2)
			{
				int num7 = num3 + 1;
				chunk = this.GetChunk(num2, num7, true);
				num4 = x - num2 * this.divX;
				num5 = y - num7 * this.divY;
				chunk.colors[num5 * num + num4] = color;
				chunk.Dirty = true;
			}
			if (flag && flag2)
			{
				int num8 = num2 + 1;
				int num9 = num3 + 1;
				chunk = this.GetChunk(num8, num9, true);
				num4 = x - num8 * this.divX;
				num5 = y - num9 * this.divY;
				chunk.colors[num5 * num + num4] = color;
				chunk.Dirty = true;
			}
		}

		public ColorChunk GetChunk(int x, int y)
		{
			if (this.chunks == null || this.chunks.Length == 0)
			{
				return null;
			}
			return this.chunks[y * this.numColumns + x];
		}

		public ColorChunk GetChunk(int x, int y, bool init)
		{
			if (this.chunks == null || this.chunks.Length == 0)
			{
				return null;
			}
			ColorChunk colorChunk = this.chunks[y * this.numColumns + x];
			this.InitChunk(colorChunk);
			return colorChunk;
		}

		public void ClearChunk(ColorChunk chunk)
		{
			for (int i = 0; i < chunk.colors.Length; i++)
			{
				chunk.colors[i] = this.clearColor;
			}
		}

		public void ClearDirtyFlag()
		{
			foreach (ColorChunk colorChunk in this.chunks)
			{
				colorChunk.Dirty = false;
			}
		}

		public void Clear(Color color)
		{
			this.clearColor = color;
			foreach (ColorChunk chunk in this.chunks)
			{
				this.ClearChunk(chunk);
			}
			this.Optimize();
		}

		public void Delete()
		{
			this.chunks = new ColorChunk[0];
		}

		public void Create()
		{
			this.chunks = new ColorChunk[this.numColumns * this.numRows];
			for (int i = 0; i < this.chunks.Length; i++)
			{
				this.chunks[i] = new ColorChunk();
			}
		}

		private void Optimize(ColorChunk chunk)
		{
			bool flag = true;
			Color32 color = this.clearColor;
			foreach (Color32 color2 in chunk.colors)
			{
				if (color2.r != color.r || color2.g != color.g || color2.b != color.b || color2.a != color.a)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				chunk.colors = new Color32[0];
			}
		}

		public void Optimize()
		{
			foreach (ColorChunk chunk in this.chunks)
			{
				this.Optimize(chunk);
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.chunks.Length == 0;
			}
		}

		public int NumActiveChunks
		{
			get
			{
				int num = 0;
				foreach (ColorChunk colorChunk in this.chunks)
				{
					if (colorChunk != null && colorChunk.colors != null && colorChunk.colors.Length > 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		public Color clearColor = Color.white;

		public ColorChunk[] chunks;

		public int numColumns;

		public int numRows;

		public int divX;

		public int divY;
	}
}
