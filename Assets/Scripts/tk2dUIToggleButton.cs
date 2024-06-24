// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIToggleButton
using System;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIToggleButton")]
public class tk2dUIToggleButton : tk2dUIBaseItemControl
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIToggleButton> OnToggle;

	public bool IsOn
	{
		get
		{
			return this.isOn;
		}
		set
		{
			if (this.isOn != value)
			{
				this.isOn = value;
				this.SetState();
				if (this.OnToggle != null)
				{
					this.OnToggle(this);
				}
			}
		}
	}

	public bool IsInToggleGroup
	{
		get
		{
			return this.isInToggleGroup;
		}
		set
		{
			this.isInToggleGroup = value;
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
			this.uiItem.OnClick += this.ButtonClick;
			this.uiItem.OnDown += this.ButtonDown;
		}
	}

	private void OnDisable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnClick -= this.ButtonClick;
			this.uiItem.OnDown -= this.ButtonDown;
		}
	}

	private void ButtonClick()
	{
		if (!this.activateOnPress)
		{
			this.ButtonToggle();
		}
	}

	private void ButtonDown()
	{
		if (this.activateOnPress)
		{
			this.ButtonToggle();
		}
	}

	private void ButtonToggle()
	{
		if (!this.isOn || !this.isInToggleGroup)
		{
			this.isOn = !this.isOn;
			this.SetState();
			if (this.OnToggle != null)
			{
				this.OnToggle(this);
			}
			base.DoSendMessage(this.SendMessageOnToggleMethodName, this);
		}
	}

	private void SetState()
	{
		tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.offStateGO, !this.isOn);
		tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.onStateGO, this.isOn);
	}

	public GameObject offStateGO;

	public GameObject onStateGO;

	public bool activateOnPress;

	[SerializeField]
	private bool isOn = true;

	private bool isInToggleGroup;

	public string SendMessageOnToggleMethodName = string.Empty;
}
