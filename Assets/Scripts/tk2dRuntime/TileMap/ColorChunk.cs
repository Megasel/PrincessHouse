// dnSpy decompiler from Assembly-CSharp.dll class: tk2dRuntime.TileMap.ColorChunk
using System;
using UnityEngine;

namespace tk2dRuntime.TileMap
{
	[Serializable]
	public class ColorChunk
	{
		public ColorChunk()
		{
			this.colors = new Color32[0];
		}

		public bool Dirty { get; set; }

		public bool Empty
		{
			get
			{
				return this.colors.Length == 0;
			}
		}

		public Color32[] colors;
	}
}
