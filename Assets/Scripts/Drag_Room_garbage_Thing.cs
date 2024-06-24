// dnSpy decompiler from Assembly-CSharp.dll class: Drag_Room_garbage_Thing
using System;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Drag_Room_garbage_Thing : MonoBehaviour
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
		if (base.gameObject.tag == "garbage")
		{
			iTween.MoveTo(this.Basket, iTween.Hash(new object[]
			{
				"x",
				5.14f,
				"y",
				-2.03f,
				"time",
				1.5,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			this.hand.SetActive(true);
		}
		if (base.gameObject.tag == "red_flower" || base.gameObject.tag == "yellow_flower" || base.gameObject.tag == "red_green_flower")
		{
			iTween.MoveTo(this.Basket, iTween.Hash(new object[]
			{
				"x",
				2.7f,
				"y",
				0.79f,
				"time",
				1.5,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			this.hand.SetActive(true);
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
		if (base.gameObject.tag == "garbage")
		{
			iTween.MoveTo(this.Basket, iTween.Hash(new object[]
			{
				"x",
				10.14f,
				"y",
				-2.03f,
				"time",
				1.5,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			this.hand.SetActive(false);
		}
		if (base.gameObject.tag == "red_flower" || base.gameObject.tag == "yellow_flower" || base.gameObject.tag == "red_green_flower")
		{
			iTween.MoveTo(this.Basket, iTween.Hash(new object[]
			{
				"x",
				10.14f,
				"y",
				-2.03f,
				"time",
				1.5,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
			this.hand.SetActive(false);
		}
	}

	public GameObject hand;

	public GameObject Basket;

	private Vector3 screenPoint;

	private Vector3 offset;
}
