// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUITouch
using System;
using UnityEngine;

public struct tk2dUITouch
{
	public tk2dUITouch(TouchPhase _phase, int _fingerId, Vector2 _position, Vector2 _deltaPosition, float _deltaTime)
	{
		this = default(tk2dUITouch);
		this.phase = _phase;
		this.fingerId = _fingerId;
		this.position = _position;
		this.deltaPosition = _deltaPosition;
		this.deltaTime = _deltaTime;
	}

	public tk2dUITouch(Touch touch)
	{
		this = default(tk2dUITouch);
		this.phase = touch.phase;
		this.fingerId = touch.fingerId;
		this.position = touch.position;
		this.deltaPosition = this.deltaPosition;
		this.deltaTime = this.deltaTime;
	}

	public TouchPhase phase { get; private set; }

	public int fingerId { get; private set; }

	public Vector2 position { get; private set; }

	public Vector2 deltaPosition { get; private set; }

	public float deltaTime { get; private set; }

	public override string ToString()
	{
		return string.Concat(new object[]
		{
			this.phase.ToString(),
			",",
			this.fingerId,
			",",
			this.position,
			",",
			this.deltaPosition,
			",",
			this.deltaTime
		});
	}

	public const int MOUSE_POINTER_FINGER_ID = 9999;
}
