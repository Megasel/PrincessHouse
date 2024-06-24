// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUILayout
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/Core/tk2dUILayout")]
public class tk2dUILayout : MonoBehaviour
{
	public int ItemCount
	{
		get
		{
			return this.layoutItems.Count;
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<Vector3, Vector3> OnReshape;

	private void Reset()
	{
		if (base.GetComponent<Collider>() != null)
		{
			BoxCollider boxCollider = base.GetComponent<Collider>() as BoxCollider;
			if (boxCollider != null)
			{
				Bounds bounds = boxCollider.bounds;
				Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
				Vector3 position = base.transform.position;
				this.Reshape(worldToLocalMatrix.MultiplyPoint(bounds.min) - this.bMin, worldToLocalMatrix.MultiplyPoint(bounds.max) - this.bMax, true);
				Vector3 b = worldToLocalMatrix.MultiplyVector(base.transform.position - position);
				Transform transform = base.transform;
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					Vector3 localPosition = child.localPosition - b;
					child.localPosition = localPosition;
				}
				boxCollider.center -= b;
				this.autoResizeCollider = true;
			}
		}
	}

	public virtual void Reshape(Vector3 dMin, Vector3 dMax, bool updateChildren)
	{
		foreach (tk2dUILayoutItem tk2dUILayoutItem in this.layoutItems)
		{
			tk2dUILayoutItem.oldPos = tk2dUILayoutItem.gameObj.transform.position;
		}
		this.bMin += dMin;
		this.bMax += dMax;
		Vector3 vector = new Vector3(this.bMin.x, this.bMax.y);
		base.transform.position += base.transform.localToWorldMatrix.MultiplyVector(vector);
		this.bMin -= vector;
		this.bMax -= vector;
		if (this.autoResizeCollider)
		{
			BoxCollider component = base.GetComponent<BoxCollider>();
			if (component != null)
			{
				component.center += (dMin + dMax) / 2f - vector;
				component.size += dMax - dMin;
			}
		}
		foreach (tk2dUILayoutItem tk2dUILayoutItem2 in this.layoutItems)
		{
			Vector3 a = base.transform.worldToLocalMatrix.MultiplyVector(tk2dUILayoutItem2.gameObj.transform.position - tk2dUILayoutItem2.oldPos);
			Vector3 vector2 = -a;
			Vector3 vector3 = -a;
			if (updateChildren)
			{
				vector2.x += ((!tk2dUILayoutItem2.snapLeft) ? ((!tk2dUILayoutItem2.snapRight) ? 0f : dMax.x) : dMin.x);
				vector2.y += ((!tk2dUILayoutItem2.snapBottom) ? ((!tk2dUILayoutItem2.snapTop) ? 0f : dMax.y) : dMin.y);
				vector3.x += ((!tk2dUILayoutItem2.snapRight) ? ((!tk2dUILayoutItem2.snapLeft) ? 0f : dMin.x) : dMax.x);
				vector3.y += ((!tk2dUILayoutItem2.snapTop) ? ((!tk2dUILayoutItem2.snapBottom) ? 0f : dMin.y) : dMax.y);
			}
			if (tk2dUILayoutItem2.sprite != null || tk2dUILayoutItem2.UIMask != null || tk2dUILayoutItem2.layout != null)
			{
				Matrix4x4 matrix4x = base.transform.localToWorldMatrix * tk2dUILayoutItem2.gameObj.transform.worldToLocalMatrix;
				vector2 = matrix4x.MultiplyVector(vector2);
				vector3 = matrix4x.MultiplyVector(vector3);
			}
			if (tk2dUILayoutItem2.sprite != null)
			{
				tk2dUILayoutItem2.sprite.ReshapeBounds(vector2, vector3);
			}
			else if (tk2dUILayoutItem2.UIMask != null)
			{
				tk2dUILayoutItem2.UIMask.ReshapeBounds(vector2, vector3);
			}
			else if (tk2dUILayoutItem2.layout != null)
			{
				tk2dUILayoutItem2.layout.Reshape(vector2, vector3, true);
			}
			else
			{
				Vector3 b = vector2;
				if (tk2dUILayoutItem2.snapLeft && tk2dUILayoutItem2.snapRight)
				{
					b.x = 0.5f * (vector2.x + vector3.x);
				}
				if (tk2dUILayoutItem2.snapTop && tk2dUILayoutItem2.snapBottom)
				{
					b.y = 0.5f * (vector2.y + vector3.y);
				}
				tk2dUILayoutItem2.gameObj.transform.position += b;
			}
		}
		if (this.OnReshape != null)
		{
			this.OnReshape(dMin, dMax);
		}
	}

	public void SetBounds(Vector3 pMin, Vector3 pMax)
	{
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		this.Reshape(worldToLocalMatrix.MultiplyPoint(pMin) - this.bMin, worldToLocalMatrix.MultiplyPoint(pMax) - this.bMax, true);
	}

	public Vector3 GetMinBounds()
	{
		return base.transform.localToWorldMatrix.MultiplyPoint(this.bMin);
	}

	public Vector3 GetMaxBounds()
	{
		return base.transform.localToWorldMatrix.MultiplyPoint(this.bMax);
	}

	public void Refresh()
	{
		this.Reshape(Vector3.zero, Vector3.zero, true);
	}

	public Vector3 bMin = new Vector3(0f, -1f, 0f);

	public Vector3 bMax = new Vector3(1f, 0f, 0f);

	public List<tk2dUILayoutItem> layoutItems = new List<tk2dUILayoutItem>();

	public bool autoResizeCollider;
}
