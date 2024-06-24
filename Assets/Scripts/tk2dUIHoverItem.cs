// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIHoverItem
using System;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIHoverItem")]
public class tk2dUIHoverItem : tk2dUIBaseItemControl
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIHoverItem> OnToggleHover;

	public bool IsOver
	{
		get
		{
			return this.isOver;
		}
		set
		{
			if (this.isOver != value)
			{
				this.isOver = value;
				this.SetState();
				if (this.OnToggleHover != null)
				{
					this.OnToggleHover(this);
				}
				base.DoSendMessage(this.SendMessageOnToggleHoverMethodName, this);
			}
		}
	}

	private void Start()
	{
		this.SetState();
	}

	private void OnEnable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnHoverOver += this.HoverOver;
			this.uiItem.OnHoverOut += this.HoverOut;
		}
	}

	private void OnDisable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnHoverOver -= this.HoverOver;
			this.uiItem.OnHoverOut -= this.HoverOut;
		}
	}

	private void HoverOver()
	{
		this.IsOver = true;
	}

	private void HoverOut()
	{
		this.IsOver = false;
	}

	public void SetState()
	{
		tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.overStateGO, this.isOver);
		tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.outStateGO, !this.isOver);
	}

	public GameObject outStateGO;

	public GameObject overStateGO;

	private bool isOver;

	public string SendMessageOnToggleHoverMethodName = string.Empty;
}
