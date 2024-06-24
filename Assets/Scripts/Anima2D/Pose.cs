// dnSpy decompiler from Assembly-CSharp.dll class: Anima2D.Pose
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Anima2D
{
	public class Pose : ScriptableObject
	{
		[SerializeField]
		private List<Pose.PoseEntry> m_PoseEntries;

		[Serializable]
		public class PoseEntry
		{
			public string path;

			public Vector3 localPosition;

			public Quaternion localRotation;

			public Vector3 localScale;
		}
	}
}
