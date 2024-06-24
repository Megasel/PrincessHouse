// dnSpy decompiler from Assembly-CSharp.dll class: Drag_Tool
using System;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Drag_Tool : MonoBehaviour
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
		if (base.gameObject.name == "shower_tool")
		{
			GameObject.Find("shower_part").GetComponent<ParticleSystem>().Play();
			Room_Cleaning_Main._inst.hand_window.SetActive(false);
		}
		if (base.gameObject.name == "shelf_cleaner")
		{
			Room_Cleaning_Main._inst.hand_window.SetActive(false);
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
		if (base.gameObject.name == "shower_tool")
		{
			GameObject.Find("shower_part").GetComponent<ParticleSystem>().Stop();
			Room_Cleaning_Main._inst.hand_window.SetActive(true);
		}
		if (base.gameObject.name == "shelf_cleaner")
		{
			Room_Cleaning_Main._inst.hand_window.SetActive(true);
		}
	}

	private Vector3 screenPoint;

	private Vector3 offset;
}
