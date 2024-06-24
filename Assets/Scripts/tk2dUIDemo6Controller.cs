// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIDemo6Controller
using System;
using System.Collections.Generic;
using UnityEngine;

public class tk2dUIDemo6Controller : tk2dUIBaseDemoController
{
	private void OnEnable()
	{
		this.scrollableArea.OnScroll += this.OnScroll;
	}

	private void OnDisable()
	{
		this.scrollableArea.OnScroll -= this.OnScroll;
	}

	private void Start()
	{
		this.prefabItem.transform.parent = null;
		base.DoSetActive(this.prefabItem.transform, false);
		this.itemStride = (this.prefabItem.GetMaxBounds() - this.prefabItem.GetMinBounds()).x;
		this.maxVisibleItems = Mathf.CeilToInt(this.scrollableArea.VisibleAreaLength / this.itemStride) + 1;
		float num = 0f;
		for (int i = 0; i < this.maxVisibleItems; i++)
		{
			tk2dUILayout tk2dUILayout = UnityEngine.Object.Instantiate<tk2dUILayout>(this.prefabItem);
			tk2dUILayout.transform.parent = this.scrollableArea.contentContainer.transform;
			tk2dUILayout.transform.localPosition = new Vector3(num, 0f, 0f);
			base.DoSetActive(tk2dUILayout.transform, false);
			this.unusedContentItems.Add(tk2dUILayout.transform);
			num += this.itemStride;
		}
		this.SetItemCount(100);
	}

	private void CustomizeListObject(Transform contentRoot, int itemId)
	{
		contentRoot.Find("Name").GetComponent<tk2dTextMesh>().text = this.allItems[itemId].name;
		contentRoot.Find("Score").GetComponent<tk2dTextMesh>().text = "Score: " + this.allItems[itemId].score;
		contentRoot.Find("Time").GetComponent<tk2dTextMesh>().text = "Time: " + this.allItems[itemId].time;
		contentRoot.Find("Portrait").GetComponent<tk2dBaseSprite>().color = this.allItems[itemId].color;
		contentRoot.localPosition = new Vector3((float)itemId * this.itemStride, 0f, 0f);
	}

	private void SetItemCount(int numItems)
	{
		if (numItems < this.allItems.Count)
		{
			this.allItems.RemoveRange(numItems, this.allItems.Count - numItems);
		}
		else
		{
			for (int i = this.allItems.Count; i < numItems; i++)
			{
				string[] array = new string[]
				{
					"Ba",
					"Po",
					"Re",
					"Zu",
					"Meh",
					"Ra'",
					"B'k",
					"Adam",
					"Ben",
					"George"
				};
				string[] array2 = new string[]
				{
					"Hoopler",
					"Hysleria",
					"Yeinydd",
					"Nekmit",
					"Novanoid",
					"Toog1t",
					"Yboiveth",
					"Resaix",
					"Voquev",
					"Yimello",
					"Oleald",
					"Digikiki",
					"Nocobot",
					"Morath",
					"Toximble",
					"Rodrup",
					"Chillaid",
					"Brewtine",
					"Surogou",
					"Winooze",
					"Hendassa",
					"Ekcle",
					"Noelind",
					"Animepolis",
					"Tupress",
					"Jeren",
					"Yoffa",
					"Acaer"
				};
				string name = string.Format("[{0}] {1} {2}", i, array[UnityEngine.Random.Range(0, array.Length)], array2[UnityEngine.Random.Range(0, array2.Length)]);
				Color color = new Color32((byte)UnityEngine.Random.Range(192, 255), (byte)UnityEngine.Random.Range(192, 255), (byte)UnityEngine.Random.Range(192, 255), byte.MaxValue);
				tk2dUIDemo6Controller.ItemDef itemDef = new tk2dUIDemo6Controller.ItemDef();
				itemDef.name = name;
				itemDef.color = color;
				itemDef.time = UnityEngine.Random.Range(10, 1000);
				itemDef.score = itemDef.time * UnityEngine.Random.Range(0, 30) / 60;
				this.allItems.Add(itemDef);
			}
		}
		this.UpdateListGraphics();
		this.numItemsTextMesh.text = "COUNT: " + numItems.ToString();
	}

	private void OnScroll(tk2dUIScrollableArea scrollableArea)
	{
		this.UpdateListGraphics();
	}

	private void UpdateListGraphics()
	{
		float num = this.scrollableArea.Value * (this.scrollableArea.ContentLength - this.scrollableArea.VisibleAreaLength);
		int num2 = Mathf.FloorToInt(num / this.itemStride);
		float num3 = (float)this.allItems.Count * this.itemStride;
		if (!Mathf.Approximately(num3, this.scrollableArea.ContentLength))
		{
			if (num3 < this.scrollableArea.VisibleAreaLength)
			{
				this.scrollableArea.Value = 0f;
				for (int i = 0; i < this.cachedContentItems.Count; i++)
				{
					base.DoSetActive(this.cachedContentItems[i], false);
					this.unusedContentItems.Add(this.cachedContentItems[i]);
				}
				this.cachedContentItems.Clear();
				this.firstCachedItem = -1;
				num2 = 0;
			}
			this.scrollableArea.ContentLength = num3;
			if (this.scrollableArea.ContentLength > 0f)
			{
				this.scrollableArea.Value = num / (this.scrollableArea.ContentLength - this.scrollableArea.VisibleAreaLength);
			}
		}
		int num4 = Mathf.Min(num2 + this.maxVisibleItems, this.allItems.Count);
		while (this.firstCachedItem >= 0 && this.firstCachedItem < num2)
		{
			this.firstCachedItem++;
			base.DoSetActive(this.cachedContentItems[0], false);
			this.unusedContentItems.Add(this.cachedContentItems[0]);
			this.cachedContentItems.RemoveAt(0);
			if (this.cachedContentItems.Count == 0)
			{
				this.firstCachedItem = -1;
			}
		}
		while (this.firstCachedItem >= 0 && this.firstCachedItem + this.cachedContentItems.Count > num4)
		{
			base.DoSetActive(this.cachedContentItems[this.cachedContentItems.Count - 1], false);
			this.unusedContentItems.Add(this.cachedContentItems[this.cachedContentItems.Count - 1]);
			this.cachedContentItems.RemoveAt(this.cachedContentItems.Count - 1);
			if (this.cachedContentItems.Count == 0)
			{
				this.firstCachedItem = -1;
			}
		}
		if (this.firstCachedItem < 0)
		{
			this.firstCachedItem = num2;
			int num5 = Mathf.Min(this.firstCachedItem + this.maxVisibleItems, this.allItems.Count);
			for (int j = this.firstCachedItem; j < num5; j++)
			{
				Transform transform = this.unusedContentItems[0];
				this.cachedContentItems.Add(transform);
				this.unusedContentItems.RemoveAt(0);
				this.CustomizeListObject(transform, j);
				base.DoSetActive(transform, true);
			}
		}
		else
		{
			while (this.firstCachedItem > num2)
			{
				this.firstCachedItem--;
				Transform transform2 = this.unusedContentItems[0];
				this.unusedContentItems.RemoveAt(0);
				this.cachedContentItems.Insert(0, transform2);
				this.CustomizeListObject(transform2, this.firstCachedItem);
				base.DoSetActive(transform2, true);
			}
			while (this.firstCachedItem + this.cachedContentItems.Count < num4)
			{
				Transform transform3 = this.unusedContentItems[0];
				this.unusedContentItems.RemoveAt(0);
				this.CustomizeListObject(transform3, this.firstCachedItem + this.cachedContentItems.Count);
				this.cachedContentItems.Add(transform3);
				base.DoSetActive(transform3, true);
			}
		}
	}

	private void AddMoreItems()
	{
		this.SetItemCount(this.allItems.Count + UnityEngine.Random.Range(this.numToAdd / 10, this.numToAdd));
		this.numToAdd *= 2;
	}

	private void ResetItems()
	{
		this.numToAdd = 100;
		this.SetItemCount(3);
	}

	public tk2dUILayout prefabItem;

	private float itemStride;

	public tk2dUIScrollableArea scrollableArea;

	public tk2dTextMesh numItemsTextMesh;

	private List<tk2dUIDemo6Controller.ItemDef> allItems = new List<tk2dUIDemo6Controller.ItemDef>();

	private List<Transform> cachedContentItems = new List<Transform>();

	private List<Transform> unusedContentItems = new List<Transform>();

	private int firstCachedItem = -1;

	private int maxVisibleItems;

	private int numToAdd = 100;

	private class ItemDef
	{
		public string name = string.Empty;

		public int score = 10;

		public int time = 200;

		public Color color = Color.white;
	}
}
