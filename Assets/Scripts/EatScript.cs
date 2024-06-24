// dnSpy decompiler from Assembly-CSharp.dll class: EatScript
using System;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EatScript : MonoBehaviour
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action ActionDownEvent;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action ActionMoveEvent;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action ActionUpEvent;

	private void OnMouseDown()
	{
		this.offset = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, this.screenPoint.z));
		this.fsScript.AddMask(this.offset.x, this.offset.y);
		Cursor.visible = false;
		if (this.ActionDownEvent != null)
		{
			this.ActionDownEvent();
		}
	}

	private Vector3 screenPoint;

	private Vector3 offset;

	private int i;

	private FinalScene fsScript = new FinalScene();
}
