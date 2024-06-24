// dnSpy decompiler from Assembly-CSharp.dll class: Drag_Mini_Game_Fridge
using System;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Drag_Mini_Game_Fridge : MonoBehaviour
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
		if (base.gameObject.name == "shelf_cleaner")
		{
			Fridge_Mini_Game_Main._inst.mud_hand.SetActive(false);
		}
		if (base.gameObject.name == "snow remove")
		{
			Fridge_Mini_Game_Main._inst.smoke_p.Play();
			Fridge_Mini_Game_Main._inst.blower_s.Play();
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
		if (base.gameObject.name == "shelf_cleaner")
		{
			Fridge_Mini_Game_Main._inst.mud_hand.SetActive(true);
		}
		if (base.gameObject.name == "snow remove")
		{
			Fridge_Mini_Game_Main._inst.smoke_p.Stop();
			Fridge_Mini_Game_Main._inst.blower_s.Stop();
		}
	}

	private Vector3 screenPoint;

	private Vector3 offset;
}
