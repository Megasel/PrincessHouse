// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteColliderIsland
using System;
using UnityEngine;

[Serializable]
public class tk2dSpriteColliderIsland
{
	public bool IsValid()
	{
		if (this.connected)
		{
			return this.points.Length >= 3;
		}
		return this.points.Length >= 2;
	}

	public void CopyFrom(tk2dSpriteColliderIsland src)
	{
		this.connected = src.connected;
		this.points = new Vector2[src.points.Length];
		for (int i = 0; i < this.points.Length; i++)
		{
			this.points[i] = src.points[i];
		}
	}

	public bool CompareTo(tk2dSpriteColliderIsland src)
	{
		if (this.connected != src.connected)
		{
			return false;
		}
		if (this.points.Length != src.points.Length)
		{
			return false;
		}
		for (int i = 0; i < this.points.Length; i++)
		{
			if (this.points[i] != src.points[i])
			{
				return false;
			}
		}
		return true;
	}

	public bool connected = true;

	public Vector2[] points;
}
