// dnSpy decompiler from Assembly-CSharp.dll class: Anima2D.SpriteMesh
using System;
using UnityEngine;

namespace Anima2D
{
	public class SpriteMesh : ScriptableObject
	{
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
		}

		public Mesh sharedMesh
		{
			get
			{
				return this.m_SharedMesh;
			}
		}

		public const int api_version = 4;

		[SerializeField]
		[HideInInspector]
		private int m_ApiVersion;

		[SerializeField]
		private Sprite m_Sprite;

		[SerializeField]
		private Mesh m_SharedMesh;
	}
}
