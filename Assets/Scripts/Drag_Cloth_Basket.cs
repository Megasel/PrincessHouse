// dnSpy decompiler from Assembly-CSharp.dll class: Drag_Cloth_Basket
using System;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Drag_Cloth_Basket : MonoBehaviour
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
		if (base.gameObject.tag == "black_basket_cloth")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 16;
			base.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 17;
		}
		if (base.gameObject.tag == "clr_basket_cloth")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 16;
			base.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 17;
		}
		if (base.gameObject.tag == "detergent")
		{
			if (DeterGent_Coll.drag == 0)
			{
				Dress_Washing_Main._inst.Hand_Detergent_1.SetActive(false);
				Dress_Washing_Main._inst.Hand_Detergent_2.SetActive(true);
			}
			else
			{
				Dress_Washing_Main._inst.Hand_Detergent_1.SetActive(false);
				Dress_Washing_Main._inst.Hand_Detergent_2.SetActive(false);
			}
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
		if (base.gameObject.tag == "black_basket_cloth")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
			base.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 12;
		}
		if (base.gameObject.tag == "clr_basket_cloth")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
			base.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 12;
		}
		if (base.gameObject.tag == "detergent")
		{
			if (DeterGent_Coll.drag == 0)
			{
				Dress_Washing_Main._inst.Hand_Detergent_1.SetActive(true);
				Dress_Washing_Main._inst.Hand_Detergent_2.SetActive(false);
			}
			else
			{
				Dress_Washing_Main._inst.Hand_Detergent_1.SetActive(false);
				Dress_Washing_Main._inst.Hand_Detergent_2.SetActive(false);
			}
		}
		if (this.ActionUpEvent != null)
		{
			this.ActionUpEvent();
		}
	}

	private Vector3 screenPoint;

	private Vector3 offset;
}
