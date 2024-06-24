// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUILayoutContainer
using System;
using System.Diagnostics;
using UnityEngine;

public abstract class tk2dUILayoutContainer : tk2dUILayout
{
	public Vector2 GetInnerSize()
	{
		return this.innerSize;
	}

	protected abstract void DoChildLayout();

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnChangeContent;

	public override void Reshape(Vector3 dMin, Vector3 dMax, bool updateChildren)
	{
		this.bMin += dMin;
		this.bMax += dMax;
		Vector3 b = new Vector3(this.bMin.x, this.bMax.y);
		base.transform.position += b;
		this.bMin -= b;
		this.bMax -= b;
		this.DoChildLayout();
		if (this.OnChangeContent != null)
		{
			this.OnChangeContent();
		}
	}

	public void AddLayout(tk2dUILayout layout, tk2dUILayoutItem item)
	{
		item.gameObj = layout.gameObject;
		item.layout = layout;
		this.layoutItems.Add(item);
		layout.gameObject.transform.parent = base.transform;
		base.Refresh();
	}

	public void AddLayoutAtIndex(tk2dUILayout layout, tk2dUILayoutItem item, int index)
	{
		item.gameObj = layout.gameObject;
		item.layout = layout;
		this.layoutItems.Insert(index, item);
		layout.gameObject.transform.parent = base.transform;
		base.Refresh();
	}

	public void RemoveLayout(tk2dUILayout layout)
	{
		foreach (tk2dUILayoutItem tk2dUILayoutItem in this.layoutItems)
		{
			if (tk2dUILayoutItem.layout == layout)
			{
				this.layoutItems.Remove(tk2dUILayoutItem);
				layout.gameObject.transform.parent = null;
				break;
			}
		}
		base.Refresh();
	}

	protected Vector2 innerSize = Vector2.zero;
}
