// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUISoundItem
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUISoundItem")]
public class tk2dUISoundItem : tk2dUIBaseItemControl
{
	private void OnEnable()
	{
		if (this.uiItem)
		{
			if (this.downButtonSound != null)
			{
				this.uiItem.OnDown += this.PlayDownSound;
			}
			if (this.upButtonSound != null)
			{
				this.uiItem.OnUp += this.PlayUpSound;
			}
			if (this.clickButtonSound != null)
			{
				this.uiItem.OnClick += this.PlayClickSound;
			}
			if (this.releaseButtonSound != null)
			{
				this.uiItem.OnRelease += this.PlayReleaseSound;
			}
		}
	}

	private void OnDisable()
	{
		if (this.uiItem)
		{
			if (this.downButtonSound != null)
			{
				this.uiItem.OnDown -= this.PlayDownSound;
			}
			if (this.upButtonSound != null)
			{
				this.uiItem.OnUp -= this.PlayUpSound;
			}
			if (this.clickButtonSound != null)
			{
				this.uiItem.OnClick -= this.PlayClickSound;
			}
			if (this.releaseButtonSound != null)
			{
				this.uiItem.OnRelease -= this.PlayReleaseSound;
			}
		}
	}

	private void PlayDownSound()
	{
		this.PlaySound(this.downButtonSound);
	}

	private void PlayUpSound()
	{
		this.PlaySound(this.upButtonSound);
	}

	private void PlayClickSound()
	{
		this.PlaySound(this.clickButtonSound);
	}

	private void PlayReleaseSound()
	{
		this.PlaySound(this.releaseButtonSound);
	}

	private void PlaySound(AudioClip source)
	{
		tk2dUIAudioManager.Instance.Play(source);
	}

	public AudioClip downButtonSound;

	public AudioClip upButtonSound;

	public AudioClip clickButtonSound;

	public AudioClip releaseButtonSound;
}
