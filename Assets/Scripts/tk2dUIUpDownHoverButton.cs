// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIUpDownHoverButton
using System;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIUpDownHoverButton")]
public class tk2dUIUpDownHoverButton : tk2dUIBaseItemControl
{
	public bool UseOnReleaseInsteadOfOnUp
	{
		get
		{
			return this.useOnReleaseInsteadOfOnUp;
		}
	}

	public bool IsOver
	{
		get
		{
			return this.isDown || this.isHover;
		}
		set
		{
			if (value != this.isDown || this.isHover)
			{
				if (value)
				{
					this.isHover = true;
					this.SetState();
					if (this.OnToggleOver != null)
					{
						this.OnToggleOver(this);
					}
				}
				else if (this.isDown && this.isHover)
				{
					this.isDown = false;
					this.isHover = false;
					this.SetState();
					if (this.OnToggleOver != null)
					{
						this.OnToggleOver(this);
					}
				}
				else if (this.isDown)
				{
					this.isDown = false;
					this.SetState();
					if (this.OnToggleOver != null)
					{
						this.OnToggleOver(this);
					}
				}
				else
				{
					this.isHover = false;
					this.SetState();
					if (this.OnToggleOver != null)
					{
						this.OnToggleOver(this);
					}
				}
				base.DoSendMessage(this.SendMessageOnToggleOverMethodName, this);
			}
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIUpDownHoverButton> OnToggleOver;

	private void Start()
	{
		this.SetState();
	}

	private void OnEnable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnDown += this.ButtonDown;
			if (this.useOnReleaseInsteadOfOnUp)
			{
				this.uiItem.OnRelease += this.ButtonUp;
			}
			else
			{
				this.uiItem.OnUp += this.ButtonUp;
			}
			this.uiItem.OnHoverOver += this.ButtonHoverOver;
			this.uiItem.OnHoverOut += this.ButtonHoverOut;
		}
	}

	private void OnDisable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnDown -= this.ButtonDown;
			if (this.useOnReleaseInsteadOfOnUp)
			{
				this.uiItem.OnRelease -= this.ButtonUp;
			}
			else
			{
				this.uiItem.OnUp -= this.ButtonUp;
			}
			this.uiItem.OnHoverOver -= this.ButtonHoverOver;
			this.uiItem.OnHoverOut -= this.ButtonHoverOut;
		}
	}

	private void ButtonUp()
	{
		if (this.isDown)
		{
			this.isDown = false;
			this.SetState();
			if (!this.isHover && this.OnToggleOver != null)
			{
				this.OnToggleOver(this);
			}
		}
	}

	private void ButtonDown()
	{
		if (!this.isDown)
		{
			this.isDown = true;
			this.SetState();
			if (!this.isHover && this.OnToggleOver != null)
			{
				this.OnToggleOver(this);
			}
		}
	}

	private void ButtonHoverOver()
	{
		if (!this.isHover)
		{
			this.isHover = true;
			this.SetState();
			if (!this.isDown && this.OnToggleOver != null)
			{
				this.OnToggleOver(this);
			}
		}
	}

	private void ButtonHoverOut()
	{
		if (this.isHover)
		{
			this.isHover = false;
			this.SetState();
			if (!this.isDown && this.OnToggleOver != null)
			{
				this.OnToggleOver(this);
			}
		}
	}

	public void SetState()
	{
		tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.upStateGO, !this.isDown && !this.isHover);
		if (this.downStateGO == this.hoverOverStateGO)
		{
			tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.downStateGO, this.isDown || this.isHover);
		}
		else
		{
			tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.downStateGO, this.isDown);
			tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.hoverOverStateGO, this.isHover);
		}
	}

	public void InternalSetUseOnReleaseInsteadOfOnUp(bool state)
	{
		this.useOnReleaseInsteadOfOnUp = state;
	}

	public GameObject upStateGO;

	public GameObject downStateGO;

	public GameObject hoverOverStateGO;

	[SerializeField]
	private bool useOnReleaseInsteadOfOnUp;

	private bool isDown;

	private bool isHover;

	public string SendMessageOnToggleOverMethodName = string.Empty;
}
