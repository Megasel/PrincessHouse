// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIScrollableArea
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("2D Toolkit/UI/tk2dUIScrollableArea")]
public class tk2dUIScrollableArea : MonoBehaviour
{
	public float ContentLength
	{
		get
		{
			return this.contentLength;
		}
		set
		{
			this.ContentLengthVisibleAreaLengthChange(this.contentLength, value, this.visibleAreaLength, this.visibleAreaLength);
		}
	}

	public float VisibleAreaLength
	{
		get
		{
			return this.visibleAreaLength;
		}
		set
		{
			this.ContentLengthVisibleAreaLengthChange(this.contentLength, this.contentLength, this.visibleAreaLength, value);
		}
	}

	public tk2dUILayout BackgroundLayoutItem
	{
		get
		{
			return this.backgroundLayoutItem;
		}
		set
		{
			if (this.backgroundLayoutItem != value)
			{
				if (this.backgroundLayoutItem != null)
				{
					this.backgroundLayoutItem.OnReshape -= this.LayoutReshaped;
				}
				this.backgroundLayoutItem = value;
				if (this.backgroundLayoutItem != null)
				{
					this.backgroundLayoutItem.OnReshape += this.LayoutReshaped;
				}
			}
		}
	}

	public tk2dUILayoutContainer ContentLayoutContainer
	{
		get
		{
			return this.contentLayoutContainer;
		}
		set
		{
			if (this.contentLayoutContainer != value)
			{
				if (this.contentLayoutContainer != null)
				{
					this.contentLayoutContainer.OnChangeContent -= this.ContentLayoutChangeCallback;
				}
				this.contentLayoutContainer = value;
				if (this.contentLayoutContainer != null)
				{
					this.contentLayoutContainer.OnChangeContent += this.ContentLayoutChangeCallback;
				}
			}
		}
	}

	public GameObject SendMessageTarget
	{
		get
		{
			if (this.backgroundUIItem != null)
			{
				return this.backgroundUIItem.sendMessageTarget;
			}
			return null;
		}
		set
		{
			if (this.backgroundUIItem != null && this.backgroundUIItem.sendMessageTarget != value)
			{
				this.backgroundUIItem.sendMessageTarget = value;
			}
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIScrollableArea> OnScroll;

	public float Value
	{
		get
		{
			return Mathf.Clamp01(this.percent);
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (value != this.percent)
			{
				this.UnpressAllUIItemChildren();
				this.percent = value;
				if (this.OnScroll != null)
				{
					this.OnScroll(this);
				}
				if (this.isBackgroundButtonDown || this.isSwipeScrollingInProgress)
				{
					if (tk2dUIManager.Instance__NoCreate != null)
					{
						tk2dUIManager.Instance.OnInputUpdate -= this.BackgroundOverUpdate;
					}
					this.isBackgroundButtonDown = false;
					this.isSwipeScrollingInProgress = false;
				}
				this.TargetOnScrollCallback();
			}
			if (this.scrollBar != null)
			{
				this.scrollBar.SetScrollPercentWithoutEvent(this.percent);
			}
			this.SetContentPosition();
		}
	}

	public void SetScrollPercentWithoutEvent(float newScrollPercent)
	{
		this.percent = Mathf.Clamp(newScrollPercent, 0f, 1f);
		this.UnpressAllUIItemChildren();
		if (this.scrollBar != null)
		{
			this.scrollBar.SetScrollPercentWithoutEvent(this.percent);
		}
		this.SetContentPosition();
	}

	public float MeasureContentLength()
	{
		Vector3 vector = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		Vector3 vector2 = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		Vector3[] array = new Vector3[]
		{
			vector2,
			vector
		};
		Transform transform = this.contentContainer.transform;
		tk2dUIScrollableArea.GetRendererBoundsInChildren(transform.worldToLocalMatrix, array, transform);
		if (array[0] != vector2 && array[1] != vector)
		{
			array[0] = Vector3.Min(array[0], Vector3.zero);
			array[1] = Vector3.Max(array[1], Vector3.zero);
			return (this.scrollAxes != tk2dUIScrollableArea.Axes.YAxis) ? (array[1].x - array[0].x) : (array[1].y - array[0].y);
		}
		UnityEngine.Debug.LogError("Unable to measure content length");
		return this.VisibleAreaLength * 0.9f;
	}

	private void OnEnable()
	{
		if (this.scrollBar != null)
		{
			this.scrollBar.OnScroll += this.ScrollBarMove;
		}
		if (this.backgroundUIItem != null)
		{
			this.backgroundUIItem.OnDown += this.BackgroundButtonDown;
			this.backgroundUIItem.OnRelease += this.BackgroundButtonRelease;
			this.backgroundUIItem.OnHoverOver += this.BackgroundButtonHoverOver;
			this.backgroundUIItem.OnHoverOut += this.BackgroundButtonHoverOut;
		}
		if (this.backgroundLayoutItem != null)
		{
			this.backgroundLayoutItem.OnReshape += this.LayoutReshaped;
		}
		if (this.contentLayoutContainer != null)
		{
			this.contentLayoutContainer.OnChangeContent += this.ContentLayoutChangeCallback;
		}
	}

	private void OnDisable()
	{
		if (this.scrollBar != null)
		{
			this.scrollBar.OnScroll -= this.ScrollBarMove;
		}
		if (this.backgroundUIItem != null)
		{
			this.backgroundUIItem.OnDown -= this.BackgroundButtonDown;
			this.backgroundUIItem.OnRelease -= this.BackgroundButtonRelease;
			this.backgroundUIItem.OnHoverOver -= this.BackgroundButtonHoverOver;
			this.backgroundUIItem.OnHoverOut -= this.BackgroundButtonHoverOut;
		}
		if (this.isBackgroundButtonOver)
		{
			if (tk2dUIManager.Instance__NoCreate != null)
			{
				tk2dUIManager.Instance.OnScrollWheelChange -= this.BackgroundHoverOverScrollWheelChange;
			}
			this.isBackgroundButtonOver = false;
		}
		if (this.isBackgroundButtonDown || this.isSwipeScrollingInProgress)
		{
			if (tk2dUIManager.Instance__NoCreate != null)
			{
				tk2dUIManager.Instance.OnInputUpdate -= this.BackgroundOverUpdate;
			}
			this.isBackgroundButtonDown = false;
			this.isSwipeScrollingInProgress = false;
		}
		if (this.backgroundLayoutItem != null)
		{
			this.backgroundLayoutItem.OnReshape -= this.LayoutReshaped;
		}
		if (this.contentLayoutContainer != null)
		{
			this.contentLayoutContainer.OnChangeContent -= this.ContentLayoutChangeCallback;
		}
		this.swipeCurrVelocity = 0f;
	}

	private void Start()
	{
		this.UpdateScrollbarActiveState();
	}

	private void BackgroundHoverOverScrollWheelChange(float mouseWheelChange)
	{
		if (mouseWheelChange > 0f)
		{
			if (this.scrollBar)
			{
				this.scrollBar.ScrollUpFixed();
			}
			else
			{
				this.Value -= 0.1f;
			}
		}
		else if (mouseWheelChange < 0f)
		{
			if (this.scrollBar)
			{
				this.scrollBar.ScrollDownFixed();
			}
			else
			{
				this.Value += 0.1f;
			}
		}
	}

	private void ScrollBarMove(tk2dUIScrollbar scrollBar)
	{
		this.Value = scrollBar.Value;
		this.isSwipeScrollingInProgress = false;
		if (this.isBackgroundButtonDown)
		{
			this.BackgroundButtonRelease();
		}
	}

	private Vector3 ContentContainerOffset
	{
		get
		{
			return Vector3.Scale(new Vector3(-1f, 1f, 1f), this.contentContainer.transform.localPosition);
		}
		set
		{
			this.contentContainer.transform.localPosition = Vector3.Scale(new Vector3(-1f, 1f, 1f), value);
		}
	}

	private void SetContentPosition()
	{
		Vector3 contentContainerOffset = this.ContentContainerOffset;
		float num = (this.contentLength - this.visibleAreaLength) * this.Value;
		if (num < 0f)
		{
			num = 0f;
		}
		if (this.scrollAxes == tk2dUIScrollableArea.Axes.XAxis)
		{
			contentContainerOffset.x = num;
		}
		else if (this.scrollAxes == tk2dUIScrollableArea.Axes.YAxis)
		{
			contentContainerOffset.y = num;
		}
		this.ContentContainerOffset = contentContainerOffset;
	}

	private void BackgroundButtonDown()
	{
		if (this.allowSwipeScrolling && this.contentLength > this.visibleAreaLength)
		{
			if (!this.isBackgroundButtonDown && !this.isSwipeScrollingInProgress)
			{
				tk2dUIManager.Instance.OnInputUpdate += this.BackgroundOverUpdate;
			}
			this.swipeScrollingPressDownStartLocalPos = base.transform.InverseTransformPoint(this.CalculateClickWorldPos(this.backgroundUIItem));
			this.swipePrevScrollingContentPressLocalPos = this.swipeScrollingPressDownStartLocalPos;
			this.swipeScrollingContentStartLocalPos = this.ContentContainerOffset;
			this.swipeScrollingContentDestLocalPos = this.swipeScrollingContentStartLocalPos;
			this.isBackgroundButtonDown = true;
			if (this.swipeCurrVelocity != 0f)
			{
				base.StartCoroutine(this.coDeferredClearChildrenPresses());
			}
			this.swipeCurrVelocity = 0f;
		}
	}

	private IEnumerator coDeferredClearChildrenPresses()
	{
		yield return new WaitForEndOfFrame();
		tk2dUIManager.Instance.OverrideClearAllChildrenPresses(this.backgroundUIItem);
		yield break;
	}

	private void BackgroundOverUpdate()
	{
		if (this.isBackgroundButtonDown)
		{
			this.UpdateSwipeScrollDestintationPosition();
		}
		if (this.isSwipeScrollingInProgress)
		{
			float num = this.percent;
			float num2 = 0f;
			if (this.scrollAxes == tk2dUIScrollableArea.Axes.XAxis)
			{
				num2 = this.swipeScrollingContentDestLocalPos.x;
			}
			else if (this.scrollAxes == tk2dUIScrollableArea.Axes.YAxis)
			{
				num2 = this.swipeScrollingContentDestLocalPos.y;
			}
			float num3 = 0f;
			float num4 = this.contentLength - this.visibleAreaLength;
			if (this.isBackgroundButtonDown)
			{
				if (num2 < num3)
				{
					num2 += -num2 / this.visibleAreaLength / 2f;
					if (num2 > num3)
					{
						num2 = num3;
					}
				}
				else if (num2 > num4)
				{
					num2 -= (num2 - num4) / this.visibleAreaLength / 2f;
					if (num2 < num4)
					{
						num2 = num4;
					}
				}
				if (this.scrollAxes == tk2dUIScrollableArea.Axes.XAxis)
				{
					this.swipeScrollingContentDestLocalPos.x = num2;
				}
				else if (this.scrollAxes == tk2dUIScrollableArea.Axes.YAxis)
				{
					this.swipeScrollingContentDestLocalPos.y = num2;
				}
				if (this.contentLength - this.visibleAreaLength > Mathf.Epsilon)
				{
					num = num2 / (this.contentLength - this.visibleAreaLength);
				}
				else
				{
					num = 0f;
				}
			}
			else
			{
				float num5 = this.visibleAreaLength * 0.001f;
				if (num2 < num3 || num2 > num4)
				{
					float num6 = (num2 >= num3) ? num4 : num3;
					num2 = Mathf.SmoothDamp(num2, num6, ref this.snapBackVelocity, 0.05f, float.PositiveInfinity, tk2dUITime.deltaTime);
					if (Mathf.Abs(this.snapBackVelocity) < num5)
					{
						num2 = num6;
						this.snapBackVelocity = 0f;
					}
					this.swipeCurrVelocity = 0f;
				}
				else if (this.swipeCurrVelocity != 0f)
				{
					num2 += this.swipeCurrVelocity * tk2dUITime.deltaTime * 20f;
					if (this.swipeCurrVelocity > num5 || this.swipeCurrVelocity < -num5)
					{
						this.swipeCurrVelocity = Mathf.Lerp(this.swipeCurrVelocity, 0f, tk2dUITime.deltaTime * 2.5f);
					}
					else
					{
						this.swipeCurrVelocity = 0f;
					}
				}
				else
				{
					this.isSwipeScrollingInProgress = false;
					tk2dUIManager.Instance.OnInputUpdate -= this.BackgroundOverUpdate;
				}
				if (this.scrollAxes == tk2dUIScrollableArea.Axes.XAxis)
				{
					this.swipeScrollingContentDestLocalPos.x = num2;
				}
				else if (this.scrollAxes == tk2dUIScrollableArea.Axes.YAxis)
				{
					this.swipeScrollingContentDestLocalPos.y = num2;
				}
				num = num2 / (this.contentLength - this.visibleAreaLength);
			}
			if (num != this.percent)
			{
				this.percent = num;
				this.ContentContainerOffset = this.swipeScrollingContentDestLocalPos;
				if (this.OnScroll != null)
				{
					this.OnScroll(this);
				}
				this.TargetOnScrollCallback();
			}
			if (this.scrollBar != null)
			{
				float scrollPercentWithoutEvent = this.percent;
				if (this.scrollAxes == tk2dUIScrollableArea.Axes.XAxis)
				{
					scrollPercentWithoutEvent = this.ContentContainerOffset.x / (this.contentLength - this.visibleAreaLength);
				}
				else if (this.scrollAxes == tk2dUIScrollableArea.Axes.YAxis)
				{
					scrollPercentWithoutEvent = this.ContentContainerOffset.y / (this.contentLength - this.visibleAreaLength);
				}
				this.scrollBar.SetScrollPercentWithoutEvent(scrollPercentWithoutEvent);
			}
		}
	}

	private void UpdateSwipeScrollDestintationPosition()
	{
		Vector3 a = base.transform.InverseTransformPoint(this.CalculateClickWorldPos(this.backgroundUIItem));
		Vector3 b = a - this.swipeScrollingPressDownStartLocalPos;
		b.x *= -1f;
		float f = 0f;
		if (this.scrollAxes == tk2dUIScrollableArea.Axes.XAxis)
		{
			f = b.x;
			this.swipeCurrVelocity = -(a.x - this.swipePrevScrollingContentPressLocalPos.x);
		}
		else if (this.scrollAxes == tk2dUIScrollableArea.Axes.YAxis)
		{
			f = b.y;
			this.swipeCurrVelocity = a.y - this.swipePrevScrollingContentPressLocalPos.y;
		}
		if (!this.isSwipeScrollingInProgress && Mathf.Abs(f) > 0.02f)
		{
			this.isSwipeScrollingInProgress = true;
			tk2dUIManager.Instance.OverrideClearAllChildrenPresses(this.backgroundUIItem);
		}
		if (this.isSwipeScrollingInProgress)
		{
			Vector3 vector = this.swipeScrollingContentStartLocalPos + b;
			vector.z = this.ContentContainerOffset.z;
			if (this.scrollAxes == tk2dUIScrollableArea.Axes.XAxis)
			{
				vector.y = this.ContentContainerOffset.y;
			}
			else if (this.scrollAxes == tk2dUIScrollableArea.Axes.YAxis)
			{
				vector.x = this.ContentContainerOffset.x;
			}
			vector.z = this.ContentContainerOffset.z;
			this.swipeScrollingContentDestLocalPos = vector;
			this.swipePrevScrollingContentPressLocalPos = a;
		}
	}

	private void BackgroundButtonRelease()
	{
		if (this.allowSwipeScrolling)
		{
			if (this.isBackgroundButtonDown && !this.isSwipeScrollingInProgress)
			{
				tk2dUIManager.Instance.OnInputUpdate -= this.BackgroundOverUpdate;
			}
			this.isBackgroundButtonDown = false;
		}
	}

	private void BackgroundButtonHoverOver()
	{
		if (this.allowScrollWheel)
		{
			if (!this.isBackgroundButtonOver)
			{
				tk2dUIManager.Instance.OnScrollWheelChange += this.BackgroundHoverOverScrollWheelChange;
			}
			this.isBackgroundButtonOver = true;
		}
	}

	private void BackgroundButtonHoverOut()
	{
		if (this.isBackgroundButtonOver)
		{
			tk2dUIManager.Instance.OnScrollWheelChange -= this.BackgroundHoverOverScrollWheelChange;
		}
		this.isBackgroundButtonOver = false;
	}

	private Vector3 CalculateClickWorldPos(tk2dUIItem btn)
	{
		Vector2 position = btn.Touch.position;
		Camera uicameraForControl = tk2dUIManager.Instance.GetUICameraForControl(base.gameObject);
		Vector3 result = uicameraForControl.ScreenToWorldPoint(new Vector3(position.x, position.y, btn.transform.position.z - uicameraForControl.transform.position.z));
		result.z = btn.transform.position.z;
		return result;
	}

	private void UpdateScrollbarActiveState()
	{
		bool flag = this.contentLength > this.visibleAreaLength;
		if (this.scrollBar != null && this.scrollBar.gameObject.activeSelf != flag)
		{
			tk2dUIBaseItemControl.ChangeGameObjectActiveState(this.scrollBar.gameObject, flag);
		}
	}

	private void ContentLengthVisibleAreaLengthChange(float prevContentLength, float newContentLength, float prevVisibleAreaLength, float newVisibleAreaLength)
	{
		float value;
		if (newContentLength - this.visibleAreaLength != 0f)
		{
			value = (prevContentLength - prevVisibleAreaLength) * this.Value / (newContentLength - newVisibleAreaLength);
		}
		else
		{
			value = 0f;
		}
		this.contentLength = newContentLength;
		this.visibleAreaLength = newVisibleAreaLength;
		this.UpdateScrollbarActiveState();
		this.Value = value;
	}

	private void UnpressAllUIItemChildren()
	{
	}

	private void TargetOnScrollCallback()
	{
		if (this.SendMessageTarget != null && this.SendMessageOnScrollMethodName.Length > 0)
		{
			this.SendMessageTarget.SendMessage(this.SendMessageOnScrollMethodName, this, SendMessageOptions.RequireReceiver);
		}
	}

	private static void GetRendererBoundsInChildren(Matrix4x4 rootWorldToLocal, Vector3[] minMax, Transform t)
	{
		MeshFilter component = t.GetComponent<MeshFilter>();
		if (component != null && component.sharedMesh != null)
		{
			Bounds bounds = component.sharedMesh.bounds;
			Matrix4x4 matrix4x = rootWorldToLocal * t.localToWorldMatrix;
			for (int i = 0; i < 8; i++)
			{
				Vector3 point = bounds.center + Vector3.Scale(bounds.extents, tk2dUIScrollableArea.boxExtents[i]);
				Vector3 rhs = matrix4x.MultiplyPoint(point);
				minMax[0] = Vector3.Min(minMax[0], rhs);
				minMax[1] = Vector3.Max(minMax[1], rhs);
			}
		}
		int childCount = t.childCount;
		for (int j = 0; j < childCount; j++)
		{
			Transform child = t.GetChild(j);
			if (t.gameObject.activeSelf)
			{
				tk2dUIScrollableArea.GetRendererBoundsInChildren(rootWorldToLocal, minMax, child);
			}
		}
	}

	private void LayoutReshaped(Vector3 dMin, Vector3 dMax)
	{
		this.VisibleAreaLength += ((this.scrollAxes != tk2dUIScrollableArea.Axes.XAxis) ? (dMax.y - dMin.y) : (dMax.x - dMin.x));
	}

	private void ContentLayoutChangeCallback()
	{
		if (this.contentLayoutContainer != null)
		{
			Vector2 innerSize = this.contentLayoutContainer.GetInnerSize();
			this.ContentLength = ((this.scrollAxes != tk2dUIScrollableArea.Axes.XAxis) ? innerSize.y : innerSize.x);
		}
	}

	[SerializeField]
	private float contentLength = 1f;

	[SerializeField]
	private float visibleAreaLength = 1f;

	public GameObject contentContainer;

	public tk2dUIScrollbar scrollBar;

	public tk2dUIItem backgroundUIItem;

	public tk2dUIScrollableArea.Axes scrollAxes = tk2dUIScrollableArea.Axes.YAxis;

	public bool allowSwipeScrolling = true;

	public bool allowScrollWheel = true;

	[SerializeField]
	[HideInInspector]
	private tk2dUILayout backgroundLayoutItem;

	[SerializeField]
	[HideInInspector]
	private tk2dUILayoutContainer contentLayoutContainer;

	private bool isBackgroundButtonDown;

	private bool isBackgroundButtonOver;

	private Vector3 swipeScrollingPressDownStartLocalPos = Vector3.zero;

	private Vector3 swipeScrollingContentStartLocalPos = Vector3.zero;

	private Vector3 swipeScrollingContentDestLocalPos = Vector3.zero;

	private bool isSwipeScrollingInProgress;

	private const float SWIPE_SCROLLING_FIRST_SCROLL_THRESHOLD = 0.02f;

	private const float WITHOUT_SCROLLBAR_FIXED_SCROLL_WHEEL_PERCENT = 0.1f;

	private Vector3 swipePrevScrollingContentPressLocalPos = Vector3.zero;

	private float swipeCurrVelocity;

	private float snapBackVelocity;

	public string SendMessageOnScrollMethodName = string.Empty;

	private float percent;

	private static readonly Vector3[] boxExtents = new Vector3[]
	{
		new Vector3(-1f, -1f, -1f),
		new Vector3(1f, -1f, -1f),
		new Vector3(-1f, 1f, -1f),
		new Vector3(1f, 1f, -1f),
		new Vector3(-1f, -1f, 1f),
		new Vector3(1f, -1f, 1f),
		new Vector3(-1f, 1f, 1f),
		new Vector3(1f, 1f, 1f)
	};

	public enum Axes
	{
		XAxis,
		YAxis
	}
}
