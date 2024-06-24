// dnSpy decompiler from Assembly-CSharp.dll class: tk2dDemoButtonController
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Demo/tk2dDemoButtonController")]
public class tk2dDemoButtonController : MonoBehaviour
{
	private void Update()
	{
		base.transform.Rotate(Vector3.up, this.spinSpeed * Time.deltaTime);
	}

	private void SpinLeft()
	{
		this.spinSpeed = 4f;
	}

	private void SpinRight()
	{
		this.spinSpeed = -4f;
	}

	private void StopSpinning()
	{
		this.spinSpeed = 0f;
	}

	private float spinSpeed;
}
