// dnSpy decompiler from Assembly-CSharp.dll class: UnityChan.SpringCollider
using System;
using UnityEngine;

namespace UnityChan
{
	public class SpringCollider : MonoBehaviour
	{
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(base.transform.position, this.radius);
		}

		public float radius = 0.5f;
	}
}
