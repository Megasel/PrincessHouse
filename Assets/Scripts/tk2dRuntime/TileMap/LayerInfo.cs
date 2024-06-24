// dnSpy decompiler from Assembly-CSharp.dll class: tk2dRuntime.TileMap.LayerInfo
using System;
using UnityEngine;

namespace tk2dRuntime.TileMap
{
	[Serializable]
	public class LayerInfo
	{
		public LayerInfo()
		{
			this.unityLayer = 0;
			this.useColor = true;
			this.generateCollider = true;
			this.skipMeshGeneration = false;
		}

		public string name;

		public int hash;

		public bool useColor;

		public bool generateCollider;

		public float z = 0.1f;

		public int unityLayer;

		public string sortingLayerName = string.Empty;

		public int sortingOrder;

		public bool skipMeshGeneration;

		public PhysicMaterial physicMaterial;

		public PhysicsMaterial2D physicsMaterial2D;
	}
}
