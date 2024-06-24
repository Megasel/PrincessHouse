// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIToggleButtonGroup
using System;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIToggleButtonGroup")]
public class tk2dUIToggleButtonGroup : MonoBehaviour
{
	public tk2dUIToggleButton[] ToggleBtns
	{
		get
		{
			return this.toggleBtns;
		}
	}

	public int SelectedIndex
	{
		get
		{
			return this.selectedIndex;
		}
		set
		{
			if (this.selectedIndex != value)
			{
				this.selectedIndex = value;
				this.SetToggleButtonUsingSelectedIndex();
			}
		}
	}

	public tk2dUIToggleButton SelectedToggleButton
	{
		get
		{
			return this.selectedToggleButton;
		}
		set
		{
			this.ButtonToggle(value);
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIToggleButtonGroup> OnChange;

	protected virtual void Awake()
	{
		this.Setup();
	}

	protected void Setup()
	{
		foreach (tk2dUIToggleButton tk2dUIToggleButton in this.toggleBtns)
		{
			if (tk2dUIToggleButton != null)
			{
				tk2dUIToggleButton.IsInToggleGroup = true;
				tk2dUIToggleButton.IsOn = false;
				tk2dUIToggleButton.OnToggle += this.ButtonToggle;
			}
		}
		this.SetToggleButtonUsingSelectedIndex();
	}

	public void AddNewToggleButtons(tk2dUIToggleButton[] newToggleBtns)
	{
		this.ClearExistingToggleBtns();
		this.toggleBtns = newToggleBtns;
		this.Setup();
	}

	private void ClearExistingToggleBtns()
	{
		if (this.toggleBtns != null && this.toggleBtns.Length > 0)
		{
			foreach (tk2dUIToggleButton tk2dUIToggleButton in this.toggleBtns)
			{
				tk2dUIToggleButton.IsInToggleGroup = false;
				tk2dUIToggleButton.OnToggle -= this.ButtonToggle;
				tk2dUIToggleButton.IsOn = false;
			}
		}
	}

	private void SetToggleButtonUsingSelectedIndex()
	{
		if (this.selectedIndex >= 0 && this.selectedIndex < this.toggleBtns.Length)
		{
			tk2dUIToggleButton tk2dUIToggleButton = this.toggleBtns[this.selectedIndex];
			tk2dUIToggleButton.IsOn = true;
		}
		else
		{
			tk2dUIToggleButton tk2dUIToggleButton = null;
			this.selectedIndex = -1;
			this.ButtonToggle(tk2dUIToggleButton);
		}
	}

	private void ButtonToggle(tk2dUIToggleButton toggleButton)
	{
		if (toggleButton == null || toggleButton.IsOn)
		{
			foreach (tk2dUIToggleButton tk2dUIToggleButton in this.toggleBtns)
			{
				if (tk2dUIToggleButton != toggleButton)
				{
					tk2dUIToggleButton.IsOn = false;
				}
			}
			if (toggleButton != this.selectedToggleButton)
			{
				this.selectedToggleButton = toggleButton;
				this.SetSelectedIndexFromSelectedToggleButton();
				if (this.OnChange != null)
				{
					this.OnChange(this);
				}
				if (this.sendMessageTarget != null && this.SendMessageOnChangeMethodName.Length > 0)
				{
					this.sendMessageTarget.SendMessage(this.SendMessageOnChangeMethodName, this, SendMessageOptions.RequireReceiver);
				}
			}
		}
	}

	private void SetSelectedIndexFromSelectedToggleButton()
	{
		this.selectedIndex = -1;
		for (int i = 0; i < this.toggleBtns.Length; i++)
		{
			tk2dUIToggleButton x = this.toggleBtns[i];
			if (x == this.selectedToggleButton)
			{
				this.selectedIndex = i;
				break;
			}
		}
	}

	[SerializeField]
	private tk2dUIToggleButton[] toggleBtns;

	public GameObject sendMessageTarget;

	[SerializeField]
	private int selectedIndex;

	private tk2dUIToggleButton selectedToggleButton;

	public string SendMessageOnChangeMethodName = string.Empty;
}
