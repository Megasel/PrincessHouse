// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUISpriteAnimator
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/Core/tk2dUISpriteAnimator")]
public class tk2dUISpriteAnimator : tk2dSpriteAnimator
{
	public override void LateUpdate()
	{
		base.UpdateAnimation(tk2dUITime.deltaTime);
	}
}
