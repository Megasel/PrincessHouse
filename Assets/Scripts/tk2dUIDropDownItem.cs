// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIDropDownItem
using System;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIDropDownItem")]
public class tk2dUIDropDownItem : tk2dUIBaseItemControl
{
	public int Index
	{
		get
		{
			return this.index;
		}
		set
		{
			this.index = value;
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<tk2dUIDropDownItem> OnItemSelected;

	public string LabelText
	{
		get
		{
			return this.label.text;
		}
		set
		{
			this.label.text = value;
			this.label.Commit();
		}
	}

	private void OnEnable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnClick += this.ItemSelected;
		}
	}

	private void OnDisable()
	{
		if (this.uiItem)
		{
			this.uiItem.OnClick -= this.ItemSelected;
		}
	}

	private void ItemSelected()
	{
		if (this.OnItemSelected != null)
		{
			this.OnItemSelected(this);
		}
	}

	public tk2dTextMesh label;

	public float height;

	public tk2dUIUpDownHoverButton upDownHoverBtn;

	private int index;
}
