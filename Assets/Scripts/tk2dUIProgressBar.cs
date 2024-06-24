// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIProgressBar
using System;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIProgressBar")]
public class tk2dUIProgressBar : MonoBehaviour
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnProgressComplete;

	private void Start()
	{
		this.InitializeSlicedSpriteDimensions();
		this.Value = this.percent;
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
			if (Application.isPlaying)
			{
				if (this.clippedSpriteBar != null)
				{
					this.clippedSpriteBar.clipTopRight = new Vector2(this.Value, 1f);
				}
				else if (this.scalableBar != null)
				{
					this.scalableBar.localScale = new Vector3(this.Value, this.scalableBar.localScale.y, this.scalableBar.localScale.z);
				}
				else if (this.slicedSpriteBar != null)
				{
					this.InitializeSlicedSpriteDimensions();
					float newX = Mathf.Lerp(this.emptySlicedSpriteDimensions.x, this.fullSlicedSpriteDimensions.x, this.Value);
					this.currentDimensions.Set(newX, this.fullSlicedSpriteDimensions.y);
					this.slicedSpriteBar.dimensions = this.currentDimensions;
				}
				if (!this.isProgressComplete && this.Value == 1f)
				{
					this.isProgressComplete = true;
					if (this.OnProgressComplete != null)
					{
						this.OnProgressComplete();
					}
					if (this.sendMessageTarget != null && this.SendMessageOnProgressCompleteMethodName.Length > 0)
					{
						this.sendMessageTarget.SendMessage(this.SendMessageOnProgressCompleteMethodName, this, SendMessageOptions.RequireReceiver);
					}
				}
				else if (this.isProgressComplete && this.Value < 1f)
				{
					this.isProgressComplete = false;
				}
			}
		}
	}

	private void InitializeSlicedSpriteDimensions()
	{
		if (!this.initializedSlicedSpriteDimensions)
		{
			if (this.slicedSpriteBar != null)
			{
				tk2dSpriteDefinition currentSprite = this.slicedSpriteBar.CurrentSprite;
				Vector3 vector = currentSprite.boundsData[1];
				this.fullSlicedSpriteDimensions = this.slicedSpriteBar.dimensions;
				this.emptySlicedSpriteDimensions.Set((this.slicedSpriteBar.borderLeft + this.slicedSpriteBar.borderRight) * vector.x / currentSprite.texelSize.x, this.fullSlicedSpriteDimensions.y);
			}
			this.initializedSlicedSpriteDimensions = true;
		}
	}

	public Transform scalableBar;

	public tk2dClippedSprite clippedSpriteBar;

	public tk2dSlicedSprite slicedSpriteBar;

	private bool initializedSlicedSpriteDimensions;

	private Vector2 emptySlicedSpriteDimensions = Vector2.zero;

	private Vector2 fullSlicedSpriteDimensions = Vector2.zero;

	private Vector2 currentDimensions = Vector2.zero;

	[SerializeField]
	private float percent;

	private bool isProgressComplete;

	public GameObject sendMessageTarget;

	public string SendMessageOnProgressCompleteMethodName = string.Empty;
}
