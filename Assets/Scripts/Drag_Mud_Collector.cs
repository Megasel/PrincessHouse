// dnSpy decompiler from Assembly-CSharp.dll class: Drag_Mud_Collector
using System;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Drag_Mud_Collector : MonoBehaviour
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action ActionDownEvent;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action ActionMoveEvent;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action ActionUpEvent;

	private void OnMouseDown()
	{
		this.offset = base.gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, this.screenPoint.z));
		GameManager.Instance.is_old_position = base.gameObject.transform.position;
		if (this.ActionDownEvent != null)
		{
			this.ActionDownEvent();
		}
	}

	private void OnMouseDrag()
	{
		Vector3 position = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, this.screenPoint.z);
		Vector3 position2 = Camera.main.ScreenToWorldPoint(position) + this.offset;
		base.transform.position = position2;
		if (this.ActionMoveEvent != null)
		{
			this.ActionMoveEvent();
		}
	}

	private void OnMouseUp()
	{
		base.gameObject.transform.position = GameManager.Instance.is_old_position;
		if (this.ActionUpEvent != null)
		{
			this.ActionUpEvent();
		}
	}

	private Vector3 screenPoint;

	private Vector3 offset;
}
