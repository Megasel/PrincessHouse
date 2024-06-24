// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteAnimator
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Sprite/tk2dSpriteAnimator")]
public class tk2dSpriteAnimator : MonoBehaviour
{
	public static bool g_Paused
	{
		get
		{
			return (tk2dSpriteAnimator.globalState & tk2dSpriteAnimator.State.Paused) != tk2dSpriteAnimator.State.Init;
		}
		set
		{
			tk2dSpriteAnimator.globalState = ((!value) ? tk2dSpriteAnimator.State.Init : tk2dSpriteAnimator.State.Paused);
		}
	}

	public bool Paused
	{
		get
		{
			return (this.state & tk2dSpriteAnimator.State.Paused) != tk2dSpriteAnimator.State.Init;
		}
		set
		{
			if (value)
			{
				this.state |= tk2dSpriteAnimator.State.Paused;
			}
			else
			{
				this.state &= (tk2dSpriteAnimator.State)(-3);
			}
		}
	}

	public tk2dSpriteAnimation Library
	{
		get
		{
			return this.library;
		}
		set
		{
			this.library = value;
		}
	}

	public int DefaultClipId
	{
		get
		{
			return this.defaultClipId;
		}
		set
		{
			this.defaultClipId = value;
		}
	}

	public tk2dSpriteAnimationClip DefaultClip
	{
		get
		{
			return this.GetClipById(this.defaultClipId);
		}
	}

	private void OnEnable()
	{
		if (this.Sprite == null)
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		if (this.playAutomatically)
		{
			this.Play(this.DefaultClip);
		}
	}

	public virtual tk2dBaseSprite Sprite
	{
		get
		{
			if (this._sprite == null)
			{
				this._sprite = base.GetComponent<tk2dBaseSprite>();
				if (this._sprite == null)
				{
					UnityEngine.Debug.LogError("Sprite not found attached to tk2dSpriteAnimator.");
				}
			}
			return this._sprite;
		}
	}

	public static tk2dSpriteAnimator AddComponent(GameObject go, tk2dSpriteAnimation anim, int clipId)
	{
		tk2dSpriteAnimationClip tk2dSpriteAnimationClip = anim.clips[clipId];
		tk2dSpriteAnimator tk2dSpriteAnimator = go.AddComponent<tk2dSpriteAnimator>();
		tk2dSpriteAnimator.Library = anim;
		tk2dSpriteAnimator.SetSprite(tk2dSpriteAnimationClip.frames[0].spriteCollection, tk2dSpriteAnimationClip.frames[0].spriteId);
		return tk2dSpriteAnimator;
	}

	private tk2dSpriteAnimationClip GetClipByNameVerbose(string name)
	{
		if (this.library == null)
		{
			UnityEngine.Debug.LogError("Library not set");
			return null;
		}
		tk2dSpriteAnimationClip clipByName = this.library.GetClipByName(name);
		if (clipByName == null)
		{
			UnityEngine.Debug.LogError("Unable to find clip '" + name + "' in library");
			return null;
		}
		return clipByName;
	}

	public void Play()
	{
		if (this.currentClip == null)
		{
			this.currentClip = this.DefaultClip;
		}
		this.Play(this.currentClip);
	}

	public void Play(string name)
	{
		this.Play(this.GetClipByNameVerbose(name));
	}

	public void Play(tk2dSpriteAnimationClip clip)
	{
		this.Play(clip, 0f, tk2dSpriteAnimator.DefaultFps);
	}

	public void PlayFromFrame(int frame)
	{
		if (this.currentClip == null)
		{
			this.currentClip = this.DefaultClip;
		}
		this.PlayFromFrame(this.currentClip, frame);
	}

	public void PlayFromFrame(string name, int frame)
	{
		this.PlayFromFrame(this.GetClipByNameVerbose(name), frame);
	}

	public void PlayFromFrame(tk2dSpriteAnimationClip clip, int frame)
	{
		this.PlayFrom(clip, ((float)frame + 0.001f) / clip.fps);
	}

	public void PlayFrom(float clipStartTime)
	{
		if (this.currentClip == null)
		{
			this.currentClip = this.DefaultClip;
		}
		this.PlayFrom(this.currentClip, clipStartTime);
	}

	public void PlayFrom(string name, float clipStartTime)
	{
		tk2dSpriteAnimationClip tk2dSpriteAnimationClip = (!this.library) ? null : this.library.GetClipByName(name);
		if (tk2dSpriteAnimationClip == null)
		{
			this.ClipNameError(name);
		}
		else
		{
			this.PlayFrom(tk2dSpriteAnimationClip, clipStartTime);
		}
	}

	public void PlayFrom(tk2dSpriteAnimationClip clip, float clipStartTime)
	{
		this.Play(clip, clipStartTime, tk2dSpriteAnimator.DefaultFps);
	}

	public void Play(tk2dSpriteAnimationClip clip, float clipStartTime, float overrideFps)
	{
		if (clip != null)
		{
			float num = (overrideFps <= 0f) ? clip.fps : overrideFps;
			bool flag = clipStartTime == 0f && this.IsPlaying(clip);
			if (flag)
			{
				this.clipFps = num;
			}
			else
			{
				this.state |= tk2dSpriteAnimator.State.Playing;
				this.currentClip = clip;
				this.clipFps = num;
				if (this.currentClip.wrapMode == tk2dSpriteAnimationClip.WrapMode.Single || this.currentClip.frames == null)
				{
					this.WarpClipToLocalTime(this.currentClip, 0f);
					this.state &= (tk2dSpriteAnimator.State)(-2);
				}
				else if (this.currentClip.wrapMode == tk2dSpriteAnimationClip.WrapMode.RandomFrame || this.currentClip.wrapMode == tk2dSpriteAnimationClip.WrapMode.RandomLoop)
				{
					int num2 = UnityEngine.Random.Range(0, this.currentClip.frames.Length);
					this.WarpClipToLocalTime(this.currentClip, (float)num2);
					if (this.currentClip.wrapMode == tk2dSpriteAnimationClip.WrapMode.RandomFrame)
					{
						this.previousFrame = -1;
						this.state &= (tk2dSpriteAnimator.State)(-2);
					}
				}
				else
				{
					float num3 = clipStartTime * this.clipFps;
					if (this.currentClip.wrapMode == tk2dSpriteAnimationClip.WrapMode.Once && num3 >= this.clipFps * (float)this.currentClip.frames.Length)
					{
						this.WarpClipToLocalTime(this.currentClip, (float)(this.currentClip.frames.Length - 1));
						this.OnAnimationCompleted();
						this.state &= (tk2dSpriteAnimator.State)(-2);
					}
					else
					{
						this.WarpClipToLocalTime(this.currentClip, num3);
						this.clipTime = num3;
					}
				}
			}
		}
		else
		{
			UnityEngine.Debug.LogError("Calling clip.Play() with a null clip");
			this.OnAnimationCompleted();
			this.state &= (tk2dSpriteAnimator.State)(-2);
		}
	}

	public void Stop()
	{
		this.state &= (tk2dSpriteAnimator.State)(-2);
	}

	public void StopAndResetFrame()
	{
		if (this.currentClip != null)
		{
			this.SetSprite(this.currentClip.frames[0].spriteCollection, this.currentClip.frames[0].spriteId);
		}
		this.Stop();
	}

	public bool IsPlaying(string name)
	{
		return this.Playing && this.CurrentClip != null && this.CurrentClip.name == name;
	}

	public bool IsPlaying(tk2dSpriteAnimationClip clip)
	{
		return this.Playing && this.CurrentClip != null && this.CurrentClip == clip;
	}

	public bool Playing
	{
		get
		{
			return (this.state & tk2dSpriteAnimator.State.Playing) != tk2dSpriteAnimator.State.Init;
		}
	}

	public tk2dSpriteAnimationClip CurrentClip
	{
		get
		{
			return this.currentClip;
		}
	}

	public float ClipTimeSeconds
	{
		get
		{
			return (this.clipFps <= 0f) ? (this.clipTime / this.currentClip.fps) : (this.clipTime / this.clipFps);
		}
	}

	public float ClipFps
	{
		get
		{
			return this.clipFps;
		}
		set
		{
			if (this.currentClip != null)
			{
				this.clipFps = ((value <= 0f) ? this.currentClip.fps : value);
			}
		}
	}

	public tk2dSpriteAnimationClip GetClipById(int id)
	{
		if (this.library == null)
		{
			return null;
		}
		return this.library.GetClipById(id);
	}

	public static float DefaultFps
	{
		get
		{
			return 0f;
		}
	}

	public int GetClipIdByName(string name)
	{
		return (!this.library) ? -1 : this.library.GetClipIdByName(name);
	}

	public tk2dSpriteAnimationClip GetClipByName(string name)
	{
		return (!this.library) ? null : this.library.GetClipByName(name);
	}

	public void Pause()
	{
		this.state |= tk2dSpriteAnimator.State.Paused;
	}

	public void Resume()
	{
		this.state &= (tk2dSpriteAnimator.State)(-3);
	}

	public void SetFrame(int currFrame)
	{
		this.SetFrame(currFrame, true);
	}

	public void SetFrame(int currFrame, bool triggerEvent)
	{
		if (this.currentClip == null)
		{
			this.currentClip = this.DefaultClip;
		}
		if (this.currentClip != null)
		{
			int num = currFrame % this.currentClip.frames.Length;
			this.SetFrameInternal(num);
			if (triggerEvent && this.currentClip.frames.Length > 0 && currFrame >= 0)
			{
				this.ProcessEvents(num - 1, num, 1);
			}
		}
	}

	public int CurrentFrame
	{
		get
		{
			switch (this.currentClip.wrapMode)
			{
			case tk2dSpriteAnimationClip.WrapMode.Loop:
			case tk2dSpriteAnimationClip.WrapMode.RandomLoop:
				break;
			case tk2dSpriteAnimationClip.WrapMode.LoopSection:
			{
				int num = (int)this.clipTime;
				int result = this.currentClip.loopStart + (num - this.currentClip.loopStart) % (this.currentClip.frames.Length - this.currentClip.loopStart);
				if (num >= this.currentClip.loopStart)
				{
					return result;
				}
				return num;
			}
			case tk2dSpriteAnimationClip.WrapMode.Once:
				return Mathf.Min((int)this.clipTime, this.currentClip.frames.Length);
			case tk2dSpriteAnimationClip.WrapMode.PingPong:
			{
				int num2 = (this.currentClip.frames.Length <= 1) ? 0 : ((int)this.clipTime % (this.currentClip.frames.Length + this.currentClip.frames.Length - 2));
				if (num2 >= this.currentClip.frames.Length)
				{
					num2 = 2 * this.currentClip.frames.Length - 2 - num2;
				}
				return num2;
			}
			case tk2dSpriteAnimationClip.WrapMode.RandomFrame:
				goto IL_11E;
			case tk2dSpriteAnimationClip.WrapMode.Single:
				return 0;
			default:
				goto IL_11E;
			}
			IL_4D:
			return (int)this.clipTime % this.currentClip.frames.Length;
			IL_11E:
			UnityEngine.Debug.LogError("Unhandled clip wrap mode");
			goto IL_4D;
		}
	}

	public void UpdateAnimation(float deltaTime)
	{
		tk2dSpriteAnimator.State state = this.state | tk2dSpriteAnimator.globalState;
		if (state != tk2dSpriteAnimator.State.Playing)
		{
			return;
		}
		this.clipTime += deltaTime * this.clipFps;
		int num = this.previousFrame;
		switch (this.currentClip.wrapMode)
		{
		case tk2dSpriteAnimationClip.WrapMode.Loop:
		case tk2dSpriteAnimationClip.WrapMode.RandomLoop:
		{
			int num2 = (int)this.clipTime % this.currentClip.frames.Length;
			this.SetFrameInternal(num2);
			if (num2 < num)
			{
				this.ProcessEvents(num, this.currentClip.frames.Length - 1, 1);
				this.ProcessEvents(-1, num2, 1);
			}
			else
			{
				this.ProcessEvents(num, num2, 1);
			}
			break;
		}
		case tk2dSpriteAnimationClip.WrapMode.LoopSection:
		{
			int num3 = (int)this.clipTime;
			int num4 = this.currentClip.loopStart + (num3 - this.currentClip.loopStart) % (this.currentClip.frames.Length - this.currentClip.loopStart);
			if (num3 >= this.currentClip.loopStart)
			{
				this.SetFrameInternal(num4);
				num3 = num4;
				if (num < this.currentClip.loopStart)
				{
					this.ProcessEvents(num, this.currentClip.loopStart - 1, 1);
					this.ProcessEvents(this.currentClip.loopStart - 1, num3, 1);
				}
				else if (num3 < num)
				{
					this.ProcessEvents(num, this.currentClip.frames.Length - 1, 1);
					this.ProcessEvents(this.currentClip.loopStart - 1, num3, 1);
				}
				else
				{
					this.ProcessEvents(num, num3, 1);
				}
			}
			else
			{
				this.SetFrameInternal(num3);
				this.ProcessEvents(num, num3, 1);
			}
			break;
		}
		case tk2dSpriteAnimationClip.WrapMode.Once:
		{
			int num5 = (int)this.clipTime;
			if (num5 >= this.currentClip.frames.Length)
			{
				this.SetFrameInternal(this.currentClip.frames.Length - 1);
				this.state &= (tk2dSpriteAnimator.State)(-2);
				this.ProcessEvents(num, this.currentClip.frames.Length - 1, 1);
				this.OnAnimationCompleted();
			}
			else
			{
				this.SetFrameInternal(num5);
				this.ProcessEvents(num, num5, 1);
			}
			break;
		}
		case tk2dSpriteAnimationClip.WrapMode.PingPong:
		{
			int num6 = (this.currentClip.frames.Length <= 1) ? 0 : ((int)this.clipTime % (this.currentClip.frames.Length + this.currentClip.frames.Length - 2));
			int direction = 1;
			if (num6 >= this.currentClip.frames.Length)
			{
				num6 = 2 * this.currentClip.frames.Length - 2 - num6;
				direction = -1;
			}
			if (num6 < num)
			{
				direction = -1;
			}
			this.SetFrameInternal(num6);
			this.ProcessEvents(num, num6, direction);
			break;
		}
		}
	}

	private void ClipNameError(string name)
	{
		UnityEngine.Debug.LogError("Unable to find clip named '" + name + "' in library");
	}

	private void ClipIdError(int id)
	{
		UnityEngine.Debug.LogError("Play - Invalid clip id '" + id.ToString() + "' in library");
	}

	private void WarpClipToLocalTime(tk2dSpriteAnimationClip clip, float time)
	{
		this.clipTime = time;
		int num = (int)this.clipTime % clip.frames.Length;
		tk2dSpriteAnimationFrame tk2dSpriteAnimationFrame = clip.frames[num];
		this.SetSprite(tk2dSpriteAnimationFrame.spriteCollection, tk2dSpriteAnimationFrame.spriteId);
		if (tk2dSpriteAnimationFrame.triggerEvent && this.AnimationEventTriggered != null)
		{
			this.AnimationEventTriggered(this, clip, num);
		}
		this.previousFrame = num;
	}

	private void SetFrameInternal(int currFrame)
	{
		if (this.previousFrame != currFrame)
		{
			this.SetSprite(this.currentClip.frames[currFrame].spriteCollection, this.currentClip.frames[currFrame].spriteId);
			this.previousFrame = currFrame;
		}
	}

	private void ProcessEvents(int start, int last, int direction)
	{
		if (this.AnimationEventTriggered == null || start == last || Mathf.Sign((float)(last - start)) != Mathf.Sign((float)direction))
		{
			return;
		}
		int num = last + direction;
		tk2dSpriteAnimationFrame[] frames = this.currentClip.frames;
		for (int num2 = start + direction; num2 != num; num2 += direction)
		{
			if (frames[num2].triggerEvent && this.AnimationEventTriggered != null)
			{
				this.AnimationEventTriggered(this, this.currentClip, num2);
			}
		}
	}

	private void OnAnimationCompleted()
	{
		this.previousFrame = -1;
		if (this.AnimationCompleted != null)
		{
			this.AnimationCompleted(this, this.currentClip);
		}
	}

	public virtual void LateUpdate()
	{
		this.UpdateAnimation(Time.deltaTime);
	}

	public virtual void SetSprite(tk2dSpriteCollectionData spriteCollection, int spriteId)
	{
		this.Sprite.SetSprite(spriteCollection, spriteId);
	}

	[SerializeField]
	private tk2dSpriteAnimation library;

	[SerializeField]
	private int defaultClipId;

	public bool playAutomatically;

	private static tk2dSpriteAnimator.State globalState;

	private tk2dSpriteAnimationClip currentClip;

	private float clipTime;

	private float clipFps = -1f;

	private int previousFrame = -1;

	public Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip> AnimationCompleted;

	public Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int> AnimationEventTriggered;

	private tk2dSpriteAnimator.State state;

	protected tk2dBaseSprite _sprite;

	private enum State
	{
		Init,
		Playing,
		Paused
	}
}
