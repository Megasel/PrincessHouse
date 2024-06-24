// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUITweenItem
using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUITweenItem")]
public class tk2dUITweenItem : tk2dUIBaseItemControl
{
	public bool UseOnReleaseInsteadOfOnUp
	{
		get
		{
			return this.useOnReleaseInsteadOfOnUp;
		}
	}

	private void Awake()
	{
		this.onUpScale = base.transform.localScale;
	}

	private void OnEnable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnDown += this.ButtonDown;
			if (this.canButtonBeHeldDown)
			{
				if (this.useOnReleaseInsteadOfOnUp)
				{
					this.uiItem.OnRelease += this.ButtonUp;
				}
				else
				{
					this.uiItem.OnUp += this.ButtonUp;
				}
			}
		}
		this.internalTweenInProgress = false;
		this.tweenTimeElapsed = 0f;
		base.transform.localScale = this.onUpScale;
	}

	private void OnDisable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnDown -= this.ButtonDown;
			if (this.canButtonBeHeldDown)
			{
				if (this.useOnReleaseInsteadOfOnUp)
				{
					this.uiItem.OnRelease -= this.ButtonUp;
				}
				else
				{
					this.uiItem.OnUp -= this.ButtonUp;
				}
			}
		}
	}

	private void ButtonDown()
	{
		if (this.tweenDuration <= 0f)
		{
			base.transform.localScale = this.onDownScale;
		}
		else
		{
			base.transform.localScale = this.onUpScale;
			this.tweenTargetScale = this.onDownScale;
			this.tweenStartingScale = base.transform.localScale;
			if (!this.internalTweenInProgress)
			{
				base.StartCoroutine(this.ScaleTween());
				this.internalTweenInProgress = true;
			}
		}
	}

	private void ButtonUp()
	{
		if (this.tweenDuration <= 0f)
		{
			base.transform.localScale = this.onUpScale;
		}
		else
		{
			this.tweenTargetScale = this.onUpScale;
			this.tweenStartingScale = base.transform.localScale;
			if (!this.internalTweenInProgress)
			{
				base.StartCoroutine(this.ScaleTween());
				this.internalTweenInProgress = true;
			}
		}
	}

	private IEnumerator ScaleTween()
	{
		this.tweenTimeElapsed = 0f;
		while (this.tweenTimeElapsed < this.tweenDuration)
		{
			base.transform.localScale = Vector3.Lerp(this.tweenStartingScale, this.tweenTargetScale, this.tweenTimeElapsed / this.tweenDuration);
			yield return null;
			this.tweenTimeElapsed += tk2dUITime.deltaTime;
		}
		base.transform.localScale = this.tweenTargetScale;
		this.internalTweenInProgress = false;
		if (!this.canButtonBeHeldDown)
		{
			if (this.tweenDuration <= 0f)
			{
				base.transform.localScale = this.onUpScale;
			}
			else
			{
				this.tweenTargetScale = this.onUpScale;
				this.tweenStartingScale = base.transform.localScale;
				base.StartCoroutine(this.ScaleTween());
				this.internalTweenInProgress = true;
			}
		}
		yield break;
	}

	public void InternalSetUseOnReleaseInsteadOfOnUp(bool state)
	{
		this.useOnReleaseInsteadOfOnUp = state;
	}

	private Vector3 onUpScale;

	public Vector3 onDownScale = new Vector3(0.9f, 0.9f, 0.9f);

	public float tweenDuration = 0.1f;

	public bool canButtonBeHeldDown = true;

	[SerializeField]
	private bool useOnReleaseInsteadOfOnUp;

	private bool internalTweenInProgress;

	private Vector3 tweenTargetScale = Vector3.one;

	private Vector3 tweenStartingScale = Vector3.one;

	private float tweenTimeElapsed;
}
