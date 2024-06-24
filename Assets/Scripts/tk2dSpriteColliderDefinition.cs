// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteColliderDefinition
using System;
using UnityEngine;

[Serializable]
public class tk2dSpriteColliderDefinition
{
	public tk2dSpriteColliderDefinition(tk2dSpriteColliderDefinition.Type type, Vector3 origin, float angle)
	{
		this.type = type;
		this.origin = origin;
		this.angle = angle;
	}

	public float Radius
	{
		get
		{
			return (this.type != tk2dSpriteColliderDefinition.Type.Circle) ? 0f : this.floats[0];
		}
	}

	public Vector3 Size
	{
		get
		{
			return (this.type != tk2dSpriteColliderDefinition.Type.Box) ? Vector3.zero : this.vectors[0];
		}
	}

	public tk2dSpriteColliderDefinition.Type type;

	public Vector3 origin;

	public float angle;

	public string name = string.Empty;

	public Vector3[] vectors = new Vector3[0];

	public float[] floats = new float[0];

	public enum Type
	{
		Box,
		Circle
	}
}
