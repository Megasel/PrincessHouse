// dnSpy decompiler from Assembly-CSharp.dll class: tk2dAnimatedSprite
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Sprite/tk2dAnimatedSprite (Obsolete)")]
public class tk2dAnimatedSprite : tk2dSprite
{
	public tk2dSpriteAnimator Animator
	{
		get
		{
			this.CheckAddAnimatorInternal();
			return this._animator;
		}
	}

	private void CheckAddAnimatorInternal()
	{
		if (this._animator == null)
		{
			this._animator = base.gameObject.GetComponent<tk2dSpriteAnimator>();
			if (this._animator == null)
			{
				this._animator = base.gameObject.AddComponent<tk2dSpriteAnimator>();
				this._animator.Library = this.anim;
				this._animator.DefaultClipId = this.clipId;
				this._animator.playAutomatically = this.playAutomatically;
			}
		}
	}

	protected override bool NeedBoxCollider()
	{
		return this.createCollider;
	}

	public tk2dSpriteAnimation Library
	{
		get
		{
			return this.Animator.Library;
		}
		set
		{
			this.Animator.Library = value;
		}
	}

	public int DefaultClipId
	{
		get
		{
			return this.Animator.DefaultClipId;
		}
		set
		{
			this.Animator.DefaultClipId = value;
		}
	}

	public static bool g_paused
	{
		get
		{
			return tk2dSpriteAnimator.g_Paused;
		}
		set
		{
			tk2dSpriteAnimator.g_Paused = value;
		}
	}

	public bool Paused
	{
		get
		{
			return this.Animator.Paused;
		}
		set
		{
			this.Animator.Paused = value;
		}
	}

	private void ProxyCompletedHandler(tk2dSpriteAnimator anim, tk2dSpriteAnimationClip clip)
	{
		if (this.animationCompleteDelegate != null)
		{
			int num = -1;
			tk2dSpriteAnimationClip[] array = (!(anim.Library != null)) ? null : anim.Library.clips;
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == clip)
					{
						num = i;
						break;
					}
				}
			}
			this.animationCompleteDelegate(this, num);
		}
	}

	private void ProxyEventTriggeredHandler(tk2dSpriteAnimator anim, tk2dSpriteAnimationClip clip, int frame)
	{
		if (this.animationEventDelegate != null)
		{
			this.animationEventDelegate(this, clip, clip.frames[frame], frame);
		}
	}

	private void OnEnable()
	{
		this.Animator.AnimationCompleted = new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(this.ProxyCompletedHandler);
		this.Animator.AnimationEventTriggered = new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>(this.ProxyEventTriggeredHandler);
	}

	private void OnDisable()
	{
		this.Animator.AnimationCompleted = null;
		this.Animator.AnimationEventTriggered = null;
	}

	private void Start()
	{
		this.CheckAddAnimatorInternal();
	}

	public static tk2dAnimatedSprite AddComponent(GameObject go, tk2dSpriteAnimation anim, int clipId)
	{
		tk2dSpriteAnimationClip tk2dSpriteAnimationClip = anim.clips[clipId];
		tk2dAnimatedSprite tk2dAnimatedSprite = go.AddComponent<tk2dAnimatedSprite>();
		tk2dAnimatedSprite.SetSprite(tk2dSpriteAnimationClip.frames[0].spriteCollection, tk2dSpriteAnimationClip.frames[0].spriteId);
		tk2dAnimatedSprite.anim = anim;
		return tk2dAnimatedSprite;
	}

	public void Play()
	{
		if (this.Animator.DefaultClip != null)
		{
			this.Animator.Play(this.Animator.DefaultClip);
		}
	}

	public void Play(float clipStartTime)
	{
		if (this.Animator.DefaultClip != null)
		{
			this.Animator.PlayFrom(this.Animator.DefaultClip, clipStartTime);
		}
	}

	public void PlayFromFrame(int frame)
	{
		if (this.Animator.DefaultClip != null)
		{
			this.Animator.PlayFromFrame(this.Animator.DefaultClip, frame);
		}
	}

	public void Play(string name)
	{
		this.Animator.Play(name);
	}

	public void PlayFromFrame(string name, int frame)
	{
		this.Animator.PlayFromFrame(name, frame);
	}

	public void Play(string name, float clipStartTime)
	{
		this.Animator.PlayFrom(name, clipStartTime);
	}

	public void Play(tk2dSpriteAnimationClip clip, float clipStartTime)
	{
		this.Animator.PlayFrom(clip, clipStartTime);
	}

	public void Play(tk2dSpriteAnimationClip clip, float clipStartTime, float overrideFps)
	{
		this.Animator.Play(clip, clipStartTime, overrideFps);
	}

	public tk2dSpriteAnimationClip CurrentClip
	{
		get
		{
			return this.Animator.CurrentClip;
		}
	}

	public float ClipTimeSeconds
	{
		get
		{
			return this.Animator.ClipTimeSeconds;
		}
	}

	public float ClipFps
	{
		get
		{
			return this.Animator.ClipFps;
		}
		set
		{
			this.Animator.ClipFps = value;
		}
	}

	public void Stop()
	{
		this.Animator.Stop();
	}

	public void StopAndResetFrame()
	{
		this.Animator.StopAndResetFrame();
	}

	[Obsolete]
	public bool isPlaying()
	{
		return this.Animator.Playing;
	}

	public bool IsPlaying(string name)
	{
		return this.Animator.IsPlaying(name);
	}

	public bool IsPlaying(tk2dSpriteAnimationClip clip)
	{
		return this.Animator.IsPlaying(clip);
	}

	public bool Playing
	{
		get
		{
			return this.Animator.Playing;
		}
	}

	public int GetClipIdByName(string name)
	{
		return this.Animator.GetClipIdByName(name);
	}

	public tk2dSpriteAnimationClip GetClipByName(string name)
	{
		return this.Animator.GetClipByName(name);
	}

	public static float DefaultFps
	{
		get
		{
			return tk2dSpriteAnimator.DefaultFps;
		}
	}

	public void Pause()
	{
		this.Animator.Pause();
	}

	public void Resume()
	{
		this.Animator.Resume();
	}

	public void SetFrame(int currFrame)
	{
		this.Animator.SetFrame(currFrame);
	}

	public void SetFrame(int currFrame, bool triggerEvent)
	{
		this.Animator.SetFrame(currFrame, triggerEvent);
	}

	public void UpdateAnimation(float deltaTime)
	{
		this.Animator.UpdateAnimation(deltaTime);
	}

	[SerializeField]
	private tk2dSpriteAnimator _animator;

	[SerializeField]
	private tk2dSpriteAnimation anim;

	[SerializeField]
	private int clipId;

	public bool playAutomatically;

	public bool createCollider;

	public tk2dAnimatedSprite.AnimationCompleteDelegate animationCompleteDelegate;

	public tk2dAnimatedSprite.AnimationEventDelegate animationEventDelegate;

	public delegate void AnimationCompleteDelegate(tk2dAnimatedSprite sprite, int clipId);

	public delegate void AnimationEventDelegate(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int frameNum);
}
