// dnSpy decompiler from Assembly-CSharp.dll class: DrawIfAttribute
using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DrawIfAttribute : PropertyAttribute
{
	public DrawIfAttribute(string comparedPropertyName, object comparedValue, DrawIfAttribute.DisablingType disablingType = DrawIfAttribute.DisablingType.DontDraw)
	{
		this.comparedPropertyName = comparedPropertyName;
		this.comparedValue = comparedValue;
		this.disablingType = disablingType;
	}

	public string comparedPropertyName { get; private set; }

	public object comparedValue { get; private set; }

	public DrawIfAttribute.DisablingType disablingType { get; private set; }

	public enum DisablingType
	{
		ReadOnly = 2,
		DontDraw
	}
}
