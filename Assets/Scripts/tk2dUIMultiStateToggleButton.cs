// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIMultiStateToggleButton
using System;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIMultiStateToggleButton")]
public class tk2dUIMultiStateToggleButton : tk2dUIBaseItemControl
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIMultiStateToggleButton> OnStateToggle;

	public int Index
	{
		get
		{
			return this.index;
		}
		set
		{
			if (value >= this.states.Length)
			{
				value = this.states.Length;
			}
			if (value < 0)
			{
				value = 0;
			}
			if (this.index != value)
			{
				this.index = value;
				this.SetState();
				if (this.OnStateToggle != null)
				{
					this.OnStateToggle(this);
				}
				base.DoSendMessage(this.SendMessageOnStateToggleMethodName, this);
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
		if (this.Index + 1 >= this.states.Length)
		{
			this.Index = 0;
		}
		else
		{
			this.Index++;
		}
	}

	private void SetState()
	{
		for (int i = 0; i < this.states.Length; i++)
		{
			GameObject x = this.states[i];
			if (x != null)
			{
				if (i != this.index)
				{
					if (this.states[i].activeInHierarchy)
					{
						this.states[i].SetActive(false);
					}
				}
				else if (!this.states[i].activeInHierarchy)
				{
					this.states[i].SetActive(true);
				}
			}
		}
	}

	public GameObject[] states;

	public bool activateOnPress;

	private int index;

	public string SendMessageOnStateToggleMethodName = string.Empty;
}
