// dnSpy decompiler from Assembly-CSharp.dll class: Anima2D.IkGroup
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Anima2D
{
	public class IkGroup : MonoBehaviour
	{
		public void UpdateGroup()
		{
			for (int i = 0; i < this.m_IkComponents.Count; i++)
			{
				Ik2D ik2D = this.m_IkComponents[i];
				if (ik2D)
				{
					ik2D.enabled = false;
					ik2D.UpdateIK();
				}
			}
		}

		private void LateUpdate()
		{
			this.UpdateGroup();
		}

		[SerializeField]
		[HideInInspector]
		private List<Ik2D> m_IkComponents = new List<Ik2D>();
	}
}
