// dnSpy decompiler from Assembly-CSharp.dll class: MoreAppsHandler
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Area730.MoreAppsPage;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MoreAppsHandler : MonoBehaviour
{
	private static MoreAppsHandler.AppItem parseItem(JSONNode node)
	{
		return new MoreAppsHandler.AppItem
		{
			AppTitle = node["AppTitle"],
			IconUrl = node["IconUrl"],
			Id = node["Id"]
		};
	}

	private static MoreAppsHandler.MoreAppsDescriptor parseResponse(string json)
	{
		string str = "-Android";
		try
		{
			MoreAppsHandler.MoreAppsDescriptor moreAppsDescriptor = new MoreAppsHandler.MoreAppsDescriptor();
			JSONNode jsonnode = JSON.Parse(json);
			moreAppsDescriptor.Version = jsonnode["Version"].AsInt;
			JSONArray asArray = jsonnode["AppsList" + str].AsArray;
			moreAppsDescriptor.AppsCount = asArray.Count;
			for (int i = 0; i < asArray.Count; i++)
			{
				MoreAppsHandler.AppItem item = MoreAppsHandler.parseItem(asArray[i]);
				moreAppsDescriptor.Items.Add(item);
			}
			return moreAppsDescriptor;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("MoreAppsPage: Json parse error");
		}
		return null;
	}

	private IEnumerator DownloadJsonDescriptor()
	{
		WWW www = new WWW(string.Empty);
		yield return www;
		if (www.error != null)
		{
			UnityEngine.Debug.LogError("MoreAppsPage: WWW error: " + www.error);
		}
		else
		{
			if (this.logOn)
			{
				UnityEngine.Debug.Log("MoreAppsPage: Descriptor downloaded");
			}
			yield return base.StartCoroutine(this.updateMoreAppsPage(www.text));
		}
		yield break;
	}

	private UnityAction getBtnAction(string id)
	{
		return delegate()
		{
			string url = "https://play.google.com/store/apps/details?id=" + id;
			Application.OpenURL(url);
		};
	}

	private void checkForUpdates()
	{
		MoreAppsHandler._updatesChecked = true;
		base.StartCoroutine(this.DownloadJsonDescriptor());
	}

	private void loadConfig()
	{
		string json = File.ReadAllText(Utils.GetSettingsFilePath());
		this._currentDesc = MoreAppsHandler.parseResponse(json);
	}

	private IEnumerator updateMoreAppsPage(string responseJson)
	{
		MoreAppsHandler.MoreAppsDescriptor desc = MoreAppsHandler.parseResponse(responseJson);
		if (desc == null)
		{
			UnityEngine.Debug.LogError("MoreAppsPage: Error parsing downloaded descriptor file");
			yield break;
		}
		if (Utils.ConfigFileExists() && !this.SplashLoadCrossPromotion)
		{
			if (desc.Version > this._currentDesc.Version)
			{
				for (int i = desc.AppsCount; i < this._currentDesc.AppsCount; i++)
				{
					string imagePath = Utils.GetImagePath(i);
					if (File.Exists(imagePath))
					{
						File.Delete(imagePath);
					}
				}
				File.WriteAllText(Utils.GetSettingsFilePath(), responseJson);
				this._currentDesc = desc;
				yield return base.StartCoroutine(this.updateUI(true));
			}
			else if (this.logOn)
			{
				UnityEngine.Debug.Log("MoreAppsPage: Downloaded version is not new.");
			}
		}
		else
		{
			if (this.logOn)
			{
				UnityEngine.Debug.Log("MoreAppsPage: First time file load");
			}
			File.WriteAllText(Utils.GetSettingsFilePath(), responseJson);
			this._currentDesc = desc;
			yield return base.StartCoroutine(this.updateUI(false));
		}
		yield break;
	}

	private IEnumerator updateUI(bool forceReplace = false)
	{
		if (this._currentDesc == null)
		{
			UnityEngine.Debug.LogError("MoreAppsPage: desciptor is NULL");
			yield break;
		}
		bool listEmpty = false;
		MoreAppsScrollController scrollViewController = base.GetComponent<MoreAppsScrollController>();
		int i = 0;
		while (i < this._currentDesc.AppsCount)
		{
			if (File.Exists(Utils.GetImagePath(i)) && !forceReplace)
			{
				goto IL_FB;
			}
			MoreAppsHandler.CoroutineWithData cd = new MoreAppsHandler.CoroutineWithData(this, this.downloadImage(i));
			yield return cd.coroutine;
			if ((bool)cd.result)
			{
				goto IL_FB;
			}
			UnityEngine.Debug.LogError("MoreAppsPage: Failded to load image");
			IL_1D3:
			i++;
			continue;
			IL_FB:
			if (this.SplashLoadCrossPromotion)
			{
				goto IL_1D3;
			}
			if (!listEmpty)
			{
				scrollViewController.ClearList();
				listEmpty = true;
			}
			if (Application.identifier != this._currentDesc.Items[i].Id)
			{
				MoreAppsHandler.ItemContainer itemContainer = new MoreAppsHandler.ItemContainer();
				itemContainer.appName = this._currentDesc.Items[i].AppTitle;
				string id = this._currentDesc.Items[i].Id;
				itemContainer.btnAction = this.getBtnAction(id);
				itemContainer.sprite = Utils.GetSprite(i);
				scrollViewController.AddItem(itemContainer);
				goto IL_1D3;
			}
			goto IL_1D3;
		}
		if (!this.SplashLoadCrossPromotion && !forceReplace && !MoreAppsHandler._updatesChecked)
		{
			this.checkForUpdates();
		}
		yield break;
	}

	private void loadStaticItems()
	{
		ItemLoader component = base.GetComponent<ItemLoader>();
		if (component != null)
		{
			MoreAppsScrollController component2 = base.GetComponent<MoreAppsScrollController>();
			ItemLoader.ItemElement[] androidApps = component.AndroidApps;
			foreach (ItemLoader.ItemElement itemElement in androidApps)
			{
				if (Application.identifier != itemElement.AppId)
				{
					component2.AddItem(new MoreAppsHandler.ItemContainer
					{
						sprite = itemElement.AppIcon,
						appName = itemElement.AppName,
						btnAction = this.getBtnAction(itemElement.AppId)
					});
				}
			}
			if (component2.ItemCount > 0)
			{
			}
			UnityEngine.Object.Destroy(component, 0.1f);
		}
	}

	private IEnumerator downloadImage(int index)
	{
		WWW www = new WWW(this._currentDesc.Items[index].IconUrl);
		yield return www;
		if (www.error != null)
		{
			UnityEngine.Debug.LogError("MoreAppsPage: WWW error: " + www.error);
			yield return false;
		}
		else
		{
			File.WriteAllBytes(Utils.GetImagePath(index), www.bytes);
			yield return true;
		}
		yield break;
	}

	private void Start()
	{
		this.configFileUrl = string.Empty;
		this.loadStaticItems();
		if (Utils.ConfigFileExists())
		{
			this.loadConfig();
			base.StartCoroutine(this.updateUI(false));
		}
		else
		{
			this.checkForUpdates();
		}
		if (this.logOn)
		{
			UnityEngine.Debug.Log("MoreAppsPage: data path: " + Utils.GetSettingsFilePath());
		}
	}

	private const string KEY_VERSION = "Version";

	private const string KEY_APPLIST = "AppsList";

	private const string KEY_IOS = "-iOS";

	private const string KEY_ANDROID = "-Android";

	public bool SplashLoadCrossPromotion;

	public bool logOn;

	[SerializeField]
	private Button _moreAppsBtn;

	private string configFileUrl;

	private MoreAppsHandler.MoreAppsDescriptor _currentDesc;

	private static bool _updatesChecked;

	public class ItemContainer
	{
		public Sprite sprite;

		public string appName;

		public UnityAction btnAction;
	}

	private class AppItem
	{
		public string AppTitle { get; set; }

		public string IconUrl { get; set; }

		public string Id { get; set; }

		public void Print()
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				"Title: ",
				this.AppTitle,
				", icon url: ",
				this.IconUrl,
				", id: ",
				this.Id
			}));
		}
	}

	private class MoreAppsDescriptor
	{
		public MoreAppsDescriptor()
		{
			this.Items = new List<MoreAppsHandler.AppItem>();
		}

		public int AppsCount { get; set; }

		public int Version { get; set; }

		public List<MoreAppsHandler.AppItem> Items { get; set; }

		public void Print()
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Version: ",
				this.Version,
				", AppsCount: ",
				this.AppsCount
			}));
		}
	}

	private class CoroutineWithData
	{
		public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
		{
			this.target = target;
			this.coroutine = owner.StartCoroutine(this.Run());
		}

		public Coroutine coroutine { get; private set; }

		private IEnumerator Run()
		{
			while (this.target.MoveNext())
			{
				this.result = this.target.Current;
				yield return this.result;
			}
			yield break;
		}

		public object result;

		private IEnumerator target;
	}
}
