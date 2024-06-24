// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteAnimation
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Backend/tk2dSpriteAnimation")]
public class tk2dSpriteAnimation : MonoBehaviour
{
	public tk2dSpriteAnimationClip GetClipByName(string name)
	{
		for (int i = 0; i < this.clips.Length; i++)
		{
			if (this.clips[i].name == name)
			{
				return this.clips[i];
			}
		}
		return null;
	}

	public tk2dSpriteAnimationClip GetClipById(int id)
	{
		if (id < 0 || id >= this.clips.Length || this.clips[id].Empty)
		{
			return null;
		}
		return this.clips[id];
	}

	public int GetClipIdByName(string name)
	{
		for (int i = 0; i < this.clips.Length; i++)
		{
			if (this.clips[i].name == name)
			{
				return i;
			}
		}
		return -1;
	}

	public int GetClipIdByName(tk2dSpriteAnimationClip clip)
	{
		for (int i = 0; i < this.clips.Length; i++)
		{
			if (this.clips[i] == clip)
			{
				return i;
			}
		}
		return -1;
	}

	public tk2dSpriteAnimationClip FirstValidClip
	{
		get
		{
			for (int i = 0; i < this.clips.Length; i++)
			{
				if (!this.clips[i].Empty && this.clips[i].frames[0].spriteCollection != null && this.clips[i].frames[0].spriteId != -1)
				{
					return this.clips[i];
				}
			}
			return null;
		}
	}

	public tk2dSpriteAnimationClip[] clips;
}
