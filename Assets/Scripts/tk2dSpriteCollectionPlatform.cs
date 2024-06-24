// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteCollectionPlatform
using System;

[Serializable]
public class tk2dSpriteCollectionPlatform
{
	public bool Valid
	{
		get
		{
			return this.name.Length > 0 && this.spriteCollection != null;
		}
	}

	public void CopyFrom(tk2dSpriteCollectionPlatform source)
	{
		this.name = source.name;
		this.spriteCollection = source.spriteCollection;
	}

	public string name = string.Empty;

	public tk2dSpriteCollection spriteCollection;
}
