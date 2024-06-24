// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIItem
using System;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/Core/tk2dUIItem")]
public class tk2dUIItem : MonoBehaviour
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnDown;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnUp;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnClick;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnRelease;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnHoverOver;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnHoverOut;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIItem> OnDownUIItem;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIItem> OnUpUIItem;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIItem> OnClickUIItem;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIItem> OnReleaseUIItem;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIItem> OnHoverOverUIItem;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIItem> OnHoverOutUIItem;

	private void Awake()
	{
		if (this.isChildOfAnotherUIItem)
		{
			this.UpdateParent();
		}
	}

	private void Start()
	{
		if (tk2dUIManager.Instance == null)
		{
			UnityEngine.Debug.LogError("Unable to find tk2dUIManager. Please create a tk2dUIManager in the scene before proceeding.");
		}
		if (this.isChildOfAnotherUIItem && this.parentUIItem == null)
		{
			this.UpdateParent();
		}
	}

	public bool IsPressed
	{
		get
		{
			return this.isPressed;
		}
	}

	public tk2dUITouch Touch
	{
		get
		{
			return this.touch;
		}
	}

	public tk2dUIItem ParentUIItem
	{
		get
		{
			return this.parentUIItem;
		}
	}

	public void UpdateParent()
	{
		this.parentUIItem = this.GetParentUIItem();
	}

	public void ManuallySetParent(tk2dUIItem newParentUIItem)
	{
		this.parentUIItem = newParentUIItem;
	}

	public void RemoveParent()
	{
		this.parentUIItem = null;
	}

	public bool Press(tk2dUITouch touch)
	{
		return this.Press(touch, null);
	}

	public bool Press(tk2dUITouch touch, tk2dUIItem sentFromChild)
	{
		if (this.isPressed)
		{
			return false;
		}
		if (!this.isPressed)
		{
			this.touch = touch;
			if ((this.registerPressFromChildren || sentFromChild == null) && base.enabled)
			{
				this.isPressed = true;
				if (this.OnDown != null)
				{
					this.OnDown();
				}
				if (this.OnDownUIItem != null)
				{
					this.OnDownUIItem(this);
				}
				this.DoSendMessage(this.SendMessageOnDownMethodName);
			}
			if (this.parentUIItem != null)
			{
				this.parentUIItem.Press(touch, this);
			}
		}
		return true;
	}

	public void UpdateTouch(tk2dUITouch touch)
	{
		this.touch = touch;
		if (this.parentUIItem != null)
		{
			this.parentUIItem.UpdateTouch(touch);
		}
	}

	private void DoSendMessage(string methodName)
	{
		if (this.sendMessageTarget != null && methodName.Length > 0)
		{
			this.sendMessageTarget.SendMessage(methodName, this, SendMessageOptions.RequireReceiver);
		}
	}

	public void Release()
	{
		if (this.isPressed)
		{
			this.isPressed = false;
			if (this.OnUp != null)
			{
				this.OnUp();
			}
			if (this.OnUpUIItem != null)
			{
				this.OnUpUIItem(this);
			}
			this.DoSendMessage(this.SendMessageOnUpMethodName);
			if (this.OnClick != null)
			{
				this.OnClick();
			}
			if (this.OnClickUIItem != null)
			{
				this.OnClickUIItem(this);
			}
			this.DoSendMessage(this.SendMessageOnClickMethodName);
		}
		if (this.OnRelease != null)
		{
			this.OnRelease();
		}
		if (this.OnReleaseUIItem != null)
		{
			this.OnReleaseUIItem(this);
		}
		this.DoSendMessage(this.SendMessageOnReleaseMethodName);
		if (this.parentUIItem != null)
		{
			this.parentUIItem.Release();
		}
	}

	public void CurrentOverUIItem(tk2dUIItem overUIItem)
	{
		if (overUIItem != this)
		{
			if (this.isPressed)
			{
				if (!this.CheckIsUIItemChildOfMe(overUIItem))
				{
					this.Exit();
					if (this.parentUIItem != null)
					{
						this.parentUIItem.CurrentOverUIItem(overUIItem);
					}
				}
			}
			else if (this.parentUIItem != null)
			{
				this.parentUIItem.CurrentOverUIItem(overUIItem);
			}
		}
	}

	public bool CheckIsUIItemChildOfMe(tk2dUIItem uiItem)
	{
		tk2dUIItem tk2dUIItem = null;
		bool result = false;
		if (uiItem != null)
		{
			tk2dUIItem = uiItem.parentUIItem;
		}
		while (tk2dUIItem != null)
		{
			if (tk2dUIItem == this)
			{
				result = true;
				break;
			}
			tk2dUIItem = tk2dUIItem.parentUIItem;
		}
		return result;
	}

	public void Exit()
	{
		if (this.isPressed)
		{
			this.isPressed = false;
			if (this.OnUp != null)
			{
				this.OnUp();
			}
			if (this.OnUpUIItem != null)
			{
				this.OnUpUIItem(this);
			}
			this.DoSendMessage(this.SendMessageOnUpMethodName);
		}
	}

	public bool HoverOver(tk2dUIItem prevHover)
	{
		bool flag = false;
		tk2dUIItem tk2dUIItem = null;
		if (!this.isHoverOver)
		{
			if (this.OnHoverOver != null)
			{
				this.OnHoverOver();
			}
			if (this.OnHoverOverUIItem != null)
			{
				this.OnHoverOverUIItem(this);
			}
			this.isHoverOver = true;
		}
		if (prevHover == this)
		{
			flag = true;
		}
		if (this.parentUIItem != null && this.parentUIItem.isHoverEnabled)
		{
			tk2dUIItem = this.parentUIItem;
		}
		if (tk2dUIItem == null)
		{
			return flag;
		}
		return tk2dUIItem.HoverOver(prevHover) || flag;
	}

	public void HoverOut(tk2dUIItem currHoverButton)
	{
		if (this.isHoverOver)
		{
			if (this.OnHoverOut != null)
			{
				this.OnHoverOut();
			}
			if (this.OnHoverOutUIItem != null)
			{
				this.OnHoverOutUIItem(this);
			}
			this.isHoverOver = false;
		}
		if (this.parentUIItem != null && this.parentUIItem.isHoverEnabled)
		{
			if (currHoverButton == null)
			{
				this.parentUIItem.HoverOut(currHoverButton);
			}
			else if (!this.parentUIItem.CheckIsUIItemChildOfMe(currHoverButton) && currHoverButton != this.parentUIItem)
			{
				this.parentUIItem.HoverOut(currHoverButton);
			}
		}
	}

	private tk2dUIItem GetParentUIItem()
	{
		Transform parent = base.transform.parent;
		while (parent != null)
		{
			tk2dUIItem component = parent.GetComponent<tk2dUIItem>();
			if (component != null)
			{
				return component;
			}
			parent = parent.parent;
		}
		return null;
	}

	public void SimulateClick()
	{
		if (this.OnDown != null)
		{
			this.OnDown();
		}
		if (this.OnDownUIItem != null)
		{
			this.OnDownUIItem(this);
		}
		this.DoSendMessage(this.SendMessageOnDownMethodName);
		if (this.OnUp != null)
		{
			this.OnUp();
		}
		if (this.OnUpUIItem != null)
		{
			this.OnUpUIItem(this);
		}
		this.DoSendMessage(this.SendMessageOnUpMethodName);
		if (this.OnClick != null)
		{
			this.OnClick();
		}
		if (this.OnClickUIItem != null)
		{
			this.OnClickUIItem(this);
		}
		this.DoSendMessage(this.SendMessageOnClickMethodName);
		if (this.OnRelease != null)
		{
			this.OnRelease();
		}
		if (this.OnReleaseUIItem != null)
		{
			this.OnReleaseUIItem(this);
		}
		this.DoSendMessage(this.SendMessageOnReleaseMethodName);
	}

	public void InternalSetIsChildOfAnotherUIItem(bool state)
	{
		this.isChildOfAnotherUIItem = state;
	}

	public bool InternalGetIsChildOfAnotherUIItem()
	{
		return this.isChildOfAnotherUIItem;
	}

	public GameObject sendMessageTarget;

	public string SendMessageOnDownMethodName = string.Empty;

	public string SendMessageOnUpMethodName = string.Empty;

	public string SendMessageOnClickMethodName = string.Empty;

	public string SendMessageOnReleaseMethodName = string.Empty;

	[SerializeField]
	private bool isChildOfAnotherUIItem;

	public bool registerPressFromChildren;

	public bool isHoverEnabled;

	public Transform[] editorExtraBounds = new Transform[0];

	public Transform[] editorIgnoreBounds = new Transform[0];

	private bool isPressed;

	private bool isHoverOver;

	private tk2dUITouch touch;

	private tk2dUIItem parentUIItem;
}
