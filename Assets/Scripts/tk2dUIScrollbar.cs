// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIScrollbar
using System;
using System.Diagnostics;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("2D Toolkit/UI/tk2dUIScrollbar")]
public class tk2dUIScrollbar : MonoBehaviour
{
	public tk2dUILayout BarLayoutItem
	{
		get
		{
			return this.barLayoutItem;
		}
		set
		{
			if (this.barLayoutItem != value)
			{
				if (this.barLayoutItem != null)
				{
					this.barLayoutItem.OnReshape -= this.LayoutReshaped;
				}
				this.barLayoutItem = value;
				if (this.barLayoutItem != null)
				{
					this.barLayoutItem.OnReshape += this.LayoutReshaped;
				}
			}
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIScrollbar> OnScroll;

	public GameObject SendMessageTarget
	{
		get
		{
			if (this.barUIItem != null)
			{
				return this.barUIItem.sendMessageTarget;
			}
			return null;
		}
		set
		{
			if (this.barUIItem != null && this.barUIItem.sendMessageTarget != value)
			{
				this.barUIItem.sendMessageTarget = value;
			}
		}
	}

	public float Value
	{
		get
		{
			return this.percent;
		}
		set
		{
			this.percent = Mathf.Clamp(value, 0f, 1f);
			if (this.OnScroll != null)
			{
				this.OnScroll(this);
			}
			this.SetScrollThumbPosition();
			if (this.SendMessageTarget != null && this.SendMessageOnScrollMethodName.Length > 0)
			{
				this.SendMessageTarget.SendMessage(this.SendMessageOnScrollMethodName, this, SendMessageOptions.RequireReceiver);
			}
		}
	}

	public void SetScrollPercentWithoutEvent(float newScrollPercent)
	{
		this.percent = Mathf.Clamp(newScrollPercent, 0f, 1f);
		this.SetScrollThumbPosition();
	}

	private void OnEnable()
	{
		if (this.barUIItem != null)
		{
			this.barUIItem.OnDown += this.ScrollTrackButtonDown;
			this.barUIItem.OnHoverOver += this.ScrollTrackButtonHoverOver;
			this.barUIItem.OnHoverOut += this.ScrollTrackButtonHoverOut;
		}
		if (this.thumbBtn != null)
		{
			this.thumbBtn.OnDown += this.ScrollThumbButtonDown;
			this.thumbBtn.OnRelease += this.ScrollThumbButtonRelease;
		}
		if (this.upButton != null)
		{
			this.upButton.OnDown += this.ScrollUpButtonDown;
			this.upButton.OnUp += this.ScrollUpButtonUp;
		}
		if (this.downButton != null)
		{
			this.downButton.OnDown += this.ScrollDownButtonDown;
			this.downButton.OnUp += this.ScrollDownButtonUp;
		}
		if (this.barLayoutItem != null)
		{
			this.barLayoutItem.OnReshape += this.LayoutReshaped;
		}
	}

	private void OnDisable()
	{
		if (this.barUIItem != null)
		{
			this.barUIItem.OnDown -= this.ScrollTrackButtonDown;
			this.barUIItem.OnHoverOver -= this.ScrollTrackButtonHoverOver;
			this.barUIItem.OnHoverOut -= this.ScrollTrackButtonHoverOut;
		}
		if (this.thumbBtn != null)
		{
			this.thumbBtn.OnDown -= this.ScrollThumbButtonDown;
			this.thumbBtn.OnRelease -= this.ScrollThumbButtonRelease;
		}
		if (this.upButton != null)
		{
			this.upButton.OnDown -= this.ScrollUpButtonDown;
			this.upButton.OnUp -= this.ScrollUpButtonUp;
		}
		if (this.downButton != null)
		{
			this.downButton.OnDown -= this.ScrollDownButtonDown;
			this.downButton.OnUp -= this.ScrollDownButtonUp;
		}
		if (this.isScrollThumbButtonDown)
		{
			if (tk2dUIManager.Instance__NoCreate != null)
			{
				tk2dUIManager.Instance.OnInputUpdate -= this.MoveScrollThumbButton;
			}
			this.isScrollThumbButtonDown = false;
		}
		if (this.isTrackHoverOver)
		{
			if (tk2dUIManager.Instance__NoCreate != null)
			{
				tk2dUIManager.Instance.OnScrollWheelChange -= this.TrackHoverScrollWheelChange;
			}
			this.isTrackHoverOver = false;
		}
		if (this.scrollUpDownButtonState != 0)
		{
			tk2dUIManager.Instance.OnInputUpdate -= this.CheckRepeatScrollUpDownButton;
			this.scrollUpDownButtonState = 0;
		}
		if (this.barLayoutItem != null)
		{
			this.barLayoutItem.OnReshape -= this.LayoutReshaped;
		}
	}

	private void Awake()
	{
		if (this.upButton != null)
		{
			this.hoverUpButton = this.upButton.GetComponent<tk2dUIHoverItem>();
		}
		if (this.downButton != null)
		{
			this.hoverDownButton = this.downButton.GetComponent<tk2dUIHoverItem>();
		}
	}

	private void Start()
	{
		this.SetScrollThumbPosition();
	}

	private void TrackHoverScrollWheelChange(float mouseWheelChange)
	{
		if (mouseWheelChange > 0f)
		{
			this.ScrollUpFixed();
		}
		else if (mouseWheelChange < 0f)
		{
			this.ScrollDownFixed();
		}
	}

	private void SetScrollThumbPosition()
	{
		if (this.thumbTransform != null)
		{
			float num = -((this.scrollBarLength - this.thumbLength) * this.Value);
			Vector3 localPosition = this.thumbTransform.localPosition;
			if (this.scrollAxes == tk2dUIScrollbar.Axes.XAxis)
			{
				localPosition.x = -num;
			}
			else if (this.scrollAxes == tk2dUIScrollbar.Axes.YAxis)
			{
				localPosition.y = num;
			}
			this.thumbTransform.localPosition = localPosition;
		}
		if (this.highlightProgressBar != null)
		{
			this.highlightProgressBar.Value = this.Value;
		}
	}

	private void MoveScrollThumbButton()
	{
		this.ScrollToPosition(this.CalculateClickWorldPos(this.thumbBtn) + this.moveThumbBtnOffset);
	}

	private Vector3 CalculateClickWorldPos(tk2dUIItem btn)
	{
		Camera uicameraForControl = tk2dUIManager.Instance.GetUICameraForControl(base.gameObject);
		Vector2 position = btn.Touch.position;
		Vector3 result = uicameraForControl.ScreenToWorldPoint(new Vector3(position.x, position.y, btn.transform.position.z - uicameraForControl.transform.position.z));
		result.z = btn.transform.position.z;
		return result;
	}

	private void ScrollToPosition(Vector3 worldPos)
	{
		Vector3 vector = this.thumbTransform.parent.InverseTransformPoint(worldPos);
		float num = 0f;
		if (this.scrollAxes == tk2dUIScrollbar.Axes.XAxis)
		{
			num = vector.x;
		}
		else if (this.scrollAxes == tk2dUIScrollbar.Axes.YAxis)
		{
			num = -vector.y;
		}
		this.Value = num / (this.scrollBarLength - this.thumbLength);
	}

	private void ScrollTrackButtonDown()
	{
		this.ScrollToPosition(this.CalculateClickWorldPos(this.barUIItem));
	}

	private void ScrollTrackButtonHoverOver()
	{
		if (this.allowScrollWheel)
		{
			if (!this.isTrackHoverOver)
			{
				tk2dUIManager.Instance.OnScrollWheelChange += this.TrackHoverScrollWheelChange;
			}
			this.isTrackHoverOver = true;
		}
	}

	private void ScrollTrackButtonHoverOut()
	{
		if (this.isTrackHoverOver)
		{
			tk2dUIManager.Instance.OnScrollWheelChange -= this.TrackHoverScrollWheelChange;
		}
		this.isTrackHoverOver = false;
	}

	private void ScrollThumbButtonDown()
	{
		if (!this.isScrollThumbButtonDown)
		{
			tk2dUIManager.Instance.OnInputUpdate += this.MoveScrollThumbButton;
		}
		this.isScrollThumbButtonDown = true;
		Vector3 b = this.CalculateClickWorldPos(this.thumbBtn);
		this.moveThumbBtnOffset = this.thumbBtn.transform.position - b;
		this.moveThumbBtnOffset.z = 0f;
		if (this.hoverUpButton != null)
		{
			this.hoverUpButton.IsOver = true;
		}
		if (this.hoverDownButton != null)
		{
			this.hoverDownButton.IsOver = true;
		}
	}

	private void ScrollThumbButtonRelease()
	{
		if (this.isScrollThumbButtonDown)
		{
			tk2dUIManager.Instance.OnInputUpdate -= this.MoveScrollThumbButton;
		}
		this.isScrollThumbButtonDown = false;
		if (this.hoverUpButton != null)
		{
			this.hoverUpButton.IsOver = false;
		}
		if (this.hoverDownButton != null)
		{
			this.hoverDownButton.IsOver = false;
		}
	}

	private void ScrollUpButtonDown()
	{
		this.timeOfUpDownButtonPressStart = Time.realtimeSinceStartup;
		this.repeatUpDownButtonHoldCounter = 0f;
		if (this.scrollUpDownButtonState == 0)
		{
			tk2dUIManager.Instance.OnInputUpdate += this.CheckRepeatScrollUpDownButton;
		}
		this.scrollUpDownButtonState = -1;
		this.ScrollUpFixed();
	}

	private void ScrollUpButtonUp()
	{
		if (this.scrollUpDownButtonState != 0)
		{
			tk2dUIManager.Instance.OnInputUpdate -= this.CheckRepeatScrollUpDownButton;
		}
		this.scrollUpDownButtonState = 0;
	}

	private void ScrollDownButtonDown()
	{
		this.timeOfUpDownButtonPressStart = Time.realtimeSinceStartup;
		this.repeatUpDownButtonHoldCounter = 0f;
		if (this.scrollUpDownButtonState == 0)
		{
			tk2dUIManager.Instance.OnInputUpdate += this.CheckRepeatScrollUpDownButton;
		}
		this.scrollUpDownButtonState = 1;
		this.ScrollDownFixed();
	}

	private void ScrollDownButtonUp()
	{
		if (this.scrollUpDownButtonState != 0)
		{
			tk2dUIManager.Instance.OnInputUpdate -= this.CheckRepeatScrollUpDownButton;
		}
		this.scrollUpDownButtonState = 0;
	}

	public void ScrollUpFixed()
	{
		this.ScrollDirection(-1);
	}

	public void ScrollDownFixed()
	{
		this.ScrollDirection(1);
	}

	private void CheckRepeatScrollUpDownButton()
	{
		if (this.scrollUpDownButtonState != 0)
		{
			float num = Time.realtimeSinceStartup - this.timeOfUpDownButtonPressStart;
			if (this.repeatUpDownButtonHoldCounter == 0f)
			{
				if (num > 0.55f)
				{
					this.repeatUpDownButtonHoldCounter += 1f;
					num -= 0.55f;
					this.ScrollDirection(this.scrollUpDownButtonState);
				}
			}
			else if (num > 0.45f)
			{
				this.repeatUpDownButtonHoldCounter += 1f;
				num -= 0.45f;
				this.ScrollDirection(this.scrollUpDownButtonState);
			}
		}
	}

	public void ScrollDirection(int dir)
	{
		if (this.scrollAxes == tk2dUIScrollbar.Axes.XAxis)
		{
			this.Value -= this.CalcScrollPercentOffsetButtonScrollDistance() * (float)dir * this.buttonUpDownScrollDistance;
		}
		else
		{
			this.Value += this.CalcScrollPercentOffsetButtonScrollDistance() * (float)dir * this.buttonUpDownScrollDistance;
		}
	}

	private float CalcScrollPercentOffsetButtonScrollDistance()
	{
		return 0.1f;
	}

	private void LayoutReshaped(Vector3 dMin, Vector3 dMax)
	{
		this.scrollBarLength += ((this.scrollAxes != tk2dUIScrollbar.Axes.XAxis) ? (dMax.y - dMin.y) : (dMax.x - dMin.x));
	}

	public tk2dUIItem barUIItem;

	public float scrollBarLength;

	public tk2dUIItem thumbBtn;

	public Transform thumbTransform;

	public float thumbLength;

	public tk2dUIItem upButton;

	private tk2dUIHoverItem hoverUpButton;

	public tk2dUIItem downButton;

	private tk2dUIHoverItem hoverDownButton;

	public float buttonUpDownScrollDistance = 1f;

	public bool allowScrollWheel = true;

	public tk2dUIScrollbar.Axes scrollAxes = tk2dUIScrollbar.Axes.YAxis;

	public tk2dUIProgressBar highlightProgressBar;

	[SerializeField]
	[HideInInspector]
	private tk2dUILayout barLayoutItem;

	private bool isScrollThumbButtonDown;

	private bool isTrackHoverOver;

	private float percent;

	private Vector3 moveThumbBtnOffset = Vector3.zero;

	private int scrollUpDownButtonState;

	private float timeOfUpDownButtonPressStart;

	private float repeatUpDownButtonHoldCounter;

	private const float WITHOUT_SCROLLBAR_FIXED_SCROLL_WHEEL_PERCENT = 0.1f;

	private const float INITIAL_TIME_TO_REPEAT_UP_DOWN_SCROLL_BUTTON_SCROLLING_ON_HOLD = 0.55f;

	private const float TIME_TO_REPEAT_UP_DOWN_SCROLL_BUTTON_SCROLLING_ON_HOLD = 0.45f;

	public string SendMessageOnScrollMethodName = string.Empty;

	public enum Axes
	{
		XAxis,
		YAxis
	}
}
