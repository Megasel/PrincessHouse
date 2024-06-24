// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIDragItem
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIDragItem")]
public class tk2dUIDragItem : tk2dUIBaseItemControl
{
	private void OnEnable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnDown += this.ButtonDown;
			this.uiItem.OnRelease += this.ButtonRelease;
		}
	}

	private void OnDisable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnDown -= this.ButtonDown;
			this.uiItem.OnRelease -= this.ButtonRelease;
		}
		if (this.isBtnActive)
		{
			if (tk2dUIManager.Instance__NoCreate != null)
			{
				tk2dUIManager.Instance.OnInputUpdate -= this.UpdateBtnPosition;
			}
			this.isBtnActive = false;
		}
	}

	private void UpdateBtnPosition()
	{
		base.transform.position = this.CalculateNewPos();
	}

	private Vector3 CalculateNewPos()
	{
		Vector2 position = this.uiItem.Touch.position;
		Camera uicameraForControl = tk2dUIManager.Instance.GetUICameraForControl(base.gameObject);
		Vector3 a = uicameraForControl.ScreenToWorldPoint(new Vector3(position.x, position.y, base.transform.position.z - uicameraForControl.transform.position.z));
		a.z = base.transform.position.z;
		return a + this.offset;
	}

	public void ButtonDown()
	{
		if (!this.isBtnActive)
		{
			tk2dUIManager.Instance.OnInputUpdate += this.UpdateBtnPosition;
		}
		this.isBtnActive = true;
		this.offset = Vector3.zero;
		Vector3 b = this.CalculateNewPos();
		this.offset = base.transform.position - b;
	}

	public void ButtonRelease()
	{
		if (this.isBtnActive)
		{
			tk2dUIManager.Instance.OnInputUpdate -= this.UpdateBtnPosition;
		}
		this.isBtnActive = false;
	}

	public tk2dUIManager uiManager;

	private Vector3 offset = Vector3.zero;

	private bool isBtnActive;
}
