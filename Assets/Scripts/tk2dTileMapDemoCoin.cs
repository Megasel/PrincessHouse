// dnSpy decompiler from Assembly-CSharp.dll class: tk2dTileMapDemoCoin
using System;
using UnityEngine;

public class tk2dTileMapDemoCoin : MonoBehaviour
{
	private void Awake()
	{
		if (this.animator == null)
		{
			UnityEngine.Debug.LogError("Coin - Assign animator in the inspector before proceeding.");
			base.enabled = false;
		}
		else
		{
			this.animator.enabled = false;
		}
	}

	private void OnBecameInvisible()
	{
		if (this.animator.enabled)
		{
			this.animator.enabled = false;
		}
	}

	private void OnBecameVisible()
	{
		if (!this.animator.enabled)
		{
			this.animator.enabled = true;
		}
	}

	public tk2dSpriteAnimator animator;
}
