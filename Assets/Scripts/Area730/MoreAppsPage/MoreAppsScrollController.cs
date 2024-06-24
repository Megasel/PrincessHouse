// dnSpy decompiler from Assembly-CSharp.dll class: Area730.MoreAppsPage.MoreAppsScrollController
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Area730.MoreAppsPage
{
	public class MoreAppsScrollController : MonoBehaviour
	{
		private void Awake()
		{
		}

		public void AddItem(MoreAppsHandler.ItemContainer itemData)
		{
			Image image = UnityEngine.Object.Instantiate<Image>(this._itemPrefab);
			ItemView component = image.GetComponent<ItemView>();
			component.btn.onClick.AddListener(itemData.btnAction);
			component.appName.text = itemData.appName;
			component.icon.sprite = itemData.sprite;
			image.transform.SetParent(this._fitter.transform);
			image.transform.localScale = Vector3.one;
			this._itemCount++;
		}

		public void ClearList()
		{
			this._itemCount = 0;
			IEnumerator enumerator = this._fitter.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform != this._fitter.transform)
					{
						UnityEngine.Object.Destroy(transform.gameObject);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public int ItemCount
		{
			get
			{
				return this._itemCount;
			}
		}

		[SerializeField]
		private Image _itemPrefab;

		[SerializeField]
		private ContentSizeFitter _fitter;

		private int _itemCount;
	}
}
