// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIDropDownMenu
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIDropDownMenu")]
public class tk2dUIDropDownMenu : MonoBehaviour
{
	public List<string> ItemList
	{
		get
		{
			return this.itemList;
		}
		set
		{
			this.itemList = value;
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnSelectedItemChange;

	public int Index
	{
		get
		{
			return this.index;
		}
		set
		{
			this.index = Mathf.Clamp(value, 0, this.ItemList.Count - 1);
			this.SetSelectedItem();
		}
	}

	public string SelectedItem
	{
		get
		{
			if (this.index >= 0 && this.index < this.itemList.Count)
			{
				return this.itemList[this.index];
			}
			return string.Empty;
		}
	}

	public GameObject SendMessageTarget
	{
		get
		{
			if (this.dropDownButton != null)
			{
				return this.dropDownButton.sendMessageTarget;
			}
			return null;
		}
		set
		{
			if (this.dropDownButton != null && this.dropDownButton.sendMessageTarget != value)
			{
				this.dropDownButton.sendMessageTarget = value;
			}
		}
	}

	public tk2dUILayout MenuLayoutItem
	{
		get
		{
			return this.menuLayoutItem;
		}
		set
		{
			this.menuLayoutItem = value;
		}
	}

	public tk2dUILayout TemplateLayoutItem
	{
		get
		{
			return this.templateLayoutItem;
		}
		set
		{
			this.templateLayoutItem = value;
		}
	}

	private void Awake()
	{
		foreach (string item in this.startingItemList)
		{
			this.itemList.Add(item);
		}
		this.index = this.startingIndex;
		this.dropDownItemTemplate.gameObject.SetActive(false);
		this.UpdateList();
	}

	private void OnEnable()
	{
		this.dropDownButton.OnDown += this.ExpandButtonPressed;
	}

	private void OnDisable()
	{
		this.dropDownButton.OnDown -= this.ExpandButtonPressed;
	}

	public void UpdateList()
	{
		if (this.dropDownItems.Count > this.ItemList.Count)
		{
			for (int i = this.ItemList.Count; i < this.dropDownItems.Count; i++)
			{
				this.dropDownItems[i].gameObject.SetActive(false);
			}
		}
		while (this.dropDownItems.Count < this.ItemList.Count)
		{
			this.dropDownItems.Add(this.CreateAnotherDropDownItem());
		}
		for (int j = 0; j < this.ItemList.Count; j++)
		{
			tk2dUIDropDownItem tk2dUIDropDownItem = this.dropDownItems[j];
			Vector3 localPosition = tk2dUIDropDownItem.transform.localPosition;
			if (this.menuLayoutItem != null && this.templateLayoutItem != null)
			{
				localPosition.y = this.menuLayoutItem.bMin.y - (float)j * (this.templateLayoutItem.bMax.y - this.templateLayoutItem.bMin.y);
			}
			else
			{
				localPosition.y = -this.height - (float)j * tk2dUIDropDownItem.height;
			}
			tk2dUIDropDownItem.transform.localPosition = localPosition;
			if (tk2dUIDropDownItem.label != null)
			{
				tk2dUIDropDownItem.LabelText = this.itemList[j];
			}
			tk2dUIDropDownItem.Index = j;
		}
		this.SetSelectedItem();
	}

	public void SetSelectedItem()
	{
		if (this.index < 0 || this.index >= this.ItemList.Count)
		{
			this.index = 0;
		}
		if (this.index >= 0 && this.index < this.ItemList.Count)
		{
			this.selectedTextMesh.text = this.ItemList[this.index];
			this.selectedTextMesh.Commit();
		}
		else
		{
			this.selectedTextMesh.text = string.Empty;
			this.selectedTextMesh.Commit();
		}
		if (this.OnSelectedItemChange != null)
		{
			this.OnSelectedItemChange();
		}
		if (this.SendMessageTarget != null && this.SendMessageOnSelectedItemChangeMethodName.Length > 0)
		{
			this.SendMessageTarget.SendMessage(this.SendMessageOnSelectedItemChangeMethodName, this, SendMessageOptions.RequireReceiver);
		}
	}

	private tk2dUIDropDownItem CreateAnotherDropDownItem()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.dropDownItemTemplate.gameObject);
		gameObject.name = "DropDownItem";
		gameObject.transform.parent = base.transform;
		gameObject.transform.localPosition = this.dropDownItemTemplate.transform.localPosition;
		gameObject.transform.localRotation = this.dropDownItemTemplate.transform.localRotation;
		gameObject.transform.localScale = this.dropDownItemTemplate.transform.localScale;
		tk2dUIDropDownItem component = gameObject.GetComponent<tk2dUIDropDownItem>();
		component.OnItemSelected += this.ItemSelected;
		tk2dUIUpDownHoverButton component2 = gameObject.GetComponent<tk2dUIUpDownHoverButton>();
		component.upDownHoverBtn = component2;
		component2.OnToggleOver += this.DropDownItemHoverBtnToggle;
		return component;
	}

	private void ItemSelected(tk2dUIDropDownItem item)
	{
		if (this.isExpanded)
		{
			this.CollapseList();
		}
		this.Index = item.Index;
	}

	private void ExpandButtonPressed()
	{
		if (this.isExpanded)
		{
			this.CollapseList();
		}
		else
		{
			this.ExpandList();
		}
	}

	private void ExpandList()
	{
		this.isExpanded = true;
		int num = Mathf.Min(this.ItemList.Count, this.dropDownItems.Count);
		for (int i = 0; i < num; i++)
		{
			this.dropDownItems[i].gameObject.SetActive(true);
		}
		tk2dUIDropDownItem tk2dUIDropDownItem = this.dropDownItems[this.index];
		if (tk2dUIDropDownItem.upDownHoverBtn != null)
		{
			tk2dUIDropDownItem.upDownHoverBtn.IsOver = true;
		}
	}

	private void CollapseList()
	{
		this.isExpanded = false;
		foreach (tk2dUIDropDownItem tk2dUIDropDownItem in this.dropDownItems)
		{
			tk2dUIDropDownItem.gameObject.SetActive(false);
		}
	}

	private void DropDownItemHoverBtnToggle(tk2dUIUpDownHoverButton upDownHoverButton)
	{
		if (upDownHoverButton.IsOver)
		{
			foreach (tk2dUIDropDownItem tk2dUIDropDownItem in this.dropDownItems)
			{
				if (tk2dUIDropDownItem.upDownHoverBtn != upDownHoverButton && tk2dUIDropDownItem.upDownHoverBtn != null)
				{
					tk2dUIDropDownItem.upDownHoverBtn.IsOver = false;
				}
			}
		}
	}

	private void OnDestroy()
	{
		foreach (tk2dUIDropDownItem tk2dUIDropDownItem in this.dropDownItems)
		{
			tk2dUIDropDownItem.OnItemSelected -= this.ItemSelected;
			if (tk2dUIDropDownItem.upDownHoverBtn != null)
			{
				tk2dUIDropDownItem.upDownHoverBtn.OnToggleOver -= this.DropDownItemHoverBtnToggle;
			}
		}
	}

	public tk2dUIItem dropDownButton;

	public tk2dTextMesh selectedTextMesh;

	[HideInInspector]
	public float height;

	public tk2dUIDropDownItem dropDownItemTemplate;

	[SerializeField]
	private string[] startingItemList;

	[SerializeField]
	private int startingIndex;

	private List<string> itemList = new List<string>();

	public string SendMessageOnSelectedItemChangeMethodName = string.Empty;

	private int index;

	private List<tk2dUIDropDownItem> dropDownItems = new List<tk2dUIDropDownItem>();

	private bool isExpanded;

	[SerializeField]
	[HideInInspector]
	private tk2dUILayout menuLayoutItem;

	[SerializeField]
	[HideInInspector]
	private tk2dUILayout templateLayoutItem;
}
