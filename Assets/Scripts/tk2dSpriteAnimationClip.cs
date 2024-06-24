// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteAnimationClip
using System;
using UnityEngine;

[Serializable]
public class tk2dSpriteAnimationClip
{
	public tk2dSpriteAnimationClip()
	{
	}

	public tk2dSpriteAnimationClip(tk2dSpriteAnimationClip source)
	{
		this.CopyFrom(source);
	}

	public void CopyFrom(tk2dSpriteAnimationClip source)
	{
		this.name = source.name;
		if (source.frames == null)
		{
			this.frames = null;
		}
		else
		{
			this.frames = new tk2dSpriteAnimationFrame[source.frames.Length];
			for (int i = 0; i < this.frames.Length; i++)
			{
				if (source.frames[i] == null)
				{
					this.frames[i] = null;
				}
				else
				{
					this.frames[i] = new tk2dSpriteAnimationFrame();
					this.frames[i].CopyFrom(source.frames[i]);
				}
			}
		}
		this.fps = source.fps;
		this.loopStart = source.loopStart;
		this.wrapMode = source.wrapMode;
		if (this.wrapMode == tk2dSpriteAnimationClip.WrapMode.Single && this.frames.Length > 1)
		{
			this.frames = new tk2dSpriteAnimationFrame[]
			{
				this.frames[0]
			};
			UnityEngine.Debug.LogError(string.Format("Clip: '{0}' Fixed up frames for WrapMode.Single", this.name));
		}
	}

	public void Clear()
	{
		this.name = string.Empty;
		this.frames = new tk2dSpriteAnimationFrame[0];
		this.fps = 30f;
		this.loopStart = 0;
		this.wrapMode = tk2dSpriteAnimationClip.WrapMode.Loop;
	}

	public bool Empty
	{
		get
		{
			return this.name.Length == 0 || this.frames == null || this.frames.Length == 0;
		}
	}

	public tk2dSpriteAnimationFrame GetFrame(int frame)
	{
		return this.frames[frame];
	}

	public string name = "Default";

	public tk2dSpriteAnimationFrame[] frames = new tk2dSpriteAnimationFrame[0];

	public float fps = 30f;

	public int loopStart;

	public tk2dSpriteAnimationClip.WrapMode wrapMode;

	public enum WrapMode
	{
		Loop,
		LoopSection,
		Once,
		PingPong,
		RandomFrame,
		RandomLoop,
		Single
	}
}
