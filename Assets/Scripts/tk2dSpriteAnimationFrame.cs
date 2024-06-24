// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteAnimationFrame
using System;

[Serializable]
public class tk2dSpriteAnimationFrame
{
	public void CopyFrom(tk2dSpriteAnimationFrame source)
	{
		this.CopyFrom(source, true);
	}

	public void CopyTriggerFrom(tk2dSpriteAnimationFrame source)
	{
		this.triggerEvent = source.triggerEvent;
		this.eventInfo = source.eventInfo;
		this.eventInt = source.eventInt;
		this.eventFloat = source.eventFloat;
	}

	public void ClearTrigger()
	{
		this.triggerEvent = false;
		this.eventInt = 0;
		this.eventFloat = 0f;
		this.eventInfo = string.Empty;
	}

	public void CopyFrom(tk2dSpriteAnimationFrame source, bool full)
	{
		this.spriteCollection = source.spriteCollection;
		this.spriteId = source.spriteId;
		if (full)
		{
			this.CopyTriggerFrom(source);
		}
	}

	public tk2dSpriteCollectionData spriteCollection;

	public int spriteId;

	public bool triggerEvent;

	public string eventInfo = string.Empty;

	public int eventInt;

	public float eventFloat;
}
