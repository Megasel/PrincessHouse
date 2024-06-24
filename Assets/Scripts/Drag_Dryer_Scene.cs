// dnSpy decompiler from Assembly-CSharp.dll class: Drag_Dryer_Scene
using System;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Drag_Dryer_Scene : MonoBehaviour
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
		Alpha_Collider_Dryer_Scene._Drag = 0;
		this.hand.SetActive(true);
		base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 26;
		if (base.gameObject.tag == "suit_wash")
		{
			iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
			{
				"x",
				1f,
				"y",
				1f,
				"time",
				4.0,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
		}
		if (base.gameObject.name == "3a")
		{
			iTween.RotateTo(base.gameObject, iTween.Hash(new object[]
			{
				"z",
				0f,
				"time",
				0.0,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
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
		this.hand.SetActive(false);
		base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
		if (base.gameObject.tag == "suit_wash")
		{
			if (Alpha_Collider_Dryer_Scene._Drag == 1)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
				{
					"x",
					0.67f,
					"y",
					0.45f,
					"time",
					0.0,
					"eastype",
					iTween.EaseType.linear,
					"islocal",
					true
				}));
			}
		}
		if (base.gameObject.name == "3a")
		{
			iTween.RotateTo(base.gameObject, iTween.Hash(new object[]
			{
				"z",
				-52.3f,
				"time",
				0.0,
				"eastype",
				iTween.EaseType.linear,
				"islocal",
				true
			}));
		}
	}

	public GameObject hand;

	private Vector3 screenPoint;

	private Vector3 offset;
}
