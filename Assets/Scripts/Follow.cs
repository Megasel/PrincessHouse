// dnSpy decompiler from Assembly-CSharp.dll class: Follow
using System;
using UnityEngine;

[ExecuteInEditMode]
public class Follow : MonoBehaviour
{
	private void LateUpdate()
	{
		if (this.target)
		{
			base.transform.position = this.target.position + this.offset;
		}
	}

	public Transform target;

	public Vector3 offset;
}
