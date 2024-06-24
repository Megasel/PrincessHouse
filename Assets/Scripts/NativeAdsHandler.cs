// dnSpy decompiler from Assembly-CSharp.dll class: NativeAdsHandler
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Area730.SelfCrossPromo;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NativeAdsHandler : MonoBehaviour
{
	private static NativeAdsHandler.AppItem parseItem(JSONNode node)
	{
		return new NativeAdsHandler.AppItem
		{
			AppTitle = node["AppTitle"],
			IconUrl = node["IconUrl"],
			Id = node["Id"]
		};
	}

	private static NativeAdsHandler.CrossPromoDescriptor parseResponse(string json)
	{
		string str = "-Android";
		try
		{
			NativeAdsHandler.CrossPromoDescriptor crossPromoDescriptor = new NativeAdsHandler.CrossPromoDescriptor();
			JSONNode jsonnode = JSON.Parse(json);
			crossPromoDescriptor.Version = jsonnode["Version"].AsInt;
			JSONArray asArray = jsonnode["AppsList" + str].AsArray;
			crossPromoDescriptor.AppsCount = asArray.Count;
			for (int i = 0; i < asArray.Count; i++)
			{
				NativeAdsHandler.AppItem appItem = NativeAdsHandler.parseItem(asArray[i]);
				if (Application.identifier != appItem.Id)
				{
					crossPromoDescriptor.Items.Add(appItem);
				}
			}
			return crossPromoDescriptor;
		}
		catch (Exception)
		{
			UnityEngine.Debug.LogError("NativeAds: Json parse error");
		}
		return null;
	}

	private IEnumerator DownloadJsonDescriptor()
	{
		
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
		NativeAdsHandler._updatesChecked = true;
		base.StartCoroutine(this.DownloadJsonDescriptor());
	}

	private void loadConfig()
	{
		string json = File.ReadAllText(NativeAdsUtils.GetSettingsFilePath());
		this._currentDesc = NativeAdsHandler.parseResponse(json);
	}

	private IEnumerator updateNativeAds(string responseJson)
	{
		NativeAdsHandler.CrossPromoDescriptor desc = NativeAdsHandler.parseResponse(responseJson);
		if (desc == null)
		{
			UnityEngine.Debug.LogError("NativeAds: Error parsing downloaded descriptor file");
			yield break;
		}
		if (NativeAdsUtils.ConfigFileExists())
		{
			if (desc.Version > this._currentDesc.Version)
			{
				for (int i = desc.AppsCount; i < this._currentDesc.AppsCount; i++)
				{
					string imagePath = NativeAdsUtils.GetImagePath(i);
					if (File.Exists(imagePath))
					{
						File.Delete(imagePath);
					}
				}
				File.WriteAllText(NativeAdsUtils.GetSettingsFilePath(), responseJson);
				this._currentDesc = desc;
				yield return base.StartCoroutine(this.updateUI(true));
			}
			else if (this.logOn)
			{
				UnityEngine.Debug.Log("NativeAds: Downloaded version is not new.");
			}
		}
		else
		{
			if (this.logOn)
			{
				UnityEngine.Debug.Log("NativeAds: First time file load");
			}
			File.WriteAllText(NativeAdsUtils.GetSettingsFilePath(), responseJson);
			this._currentDesc = desc;
			yield return base.StartCoroutine(this.updateUI(false));
		}
		yield break;
	}

	private IEnumerator updateUI(bool forceReplace = false)
	{
		if (this._currentDesc == null)
		{
			UnityEngine.Debug.LogError("NativeAds: desciptor is NULL");
			yield break;
		}
		if (this.SplashLoadCrossPromotion)
		{
			for (int i = 0; i < this._currentDesc.AppsCount; i++)
			{
				if (!File.Exists(NativeAdsUtils.GetImagePath(i)) || forceReplace)
				{
					NativeAdsHandler.CoroutineWithData cd = new NativeAdsHandler.CoroutineWithData(this, this.downloadImage(i));
					yield return cd.coroutine;
					if (!(bool)cd.result)
					{
						UnityEngine.Debug.LogError("NativeAds: Failded to load image");
					}
				}
			}
			if (!this.SplashLoadCrossPromotion)
			{
				NativeAdsHandler.ItemContainer itemContainer = new NativeAdsHandler.ItemContainer();
				this.randomNumberofApps = UnityEngine.Random.Range(0, this._currentDesc.Items.Count);
				this.id = this._currentDesc.Items[this.randomNumberofApps].Id;
				if (Application.identifier == this.id)
				{
					this.PromoImage.gameObject.SetActive(false);
				}
				else
				{
					this.PromoImage.gameObject.SetActive(true);
					this.PromoImage.sprite = NativeAdsUtils.GetSprite(this.randomNumberofApps);
				}
				itemContainer.btnAction = this.getBtnAction(this.id);
				this.PlayBtn.onClick.RemoveAllListeners();
				this.PlayBtn.onClick.AddListener(this.getBtnAction(this.id));
				if (!forceReplace && !NativeAdsHandler._updatesChecked)
				{
					this.checkForUpdates();
				}
			}
			yield break;
		}
		for (;;)
		{
			for (int j = 0; j < this._currentDesc.AppsCount; j++)
			{
				if (!File.Exists(NativeAdsUtils.GetImagePath(j)) || forceReplace)
				{
					NativeAdsHandler.CoroutineWithData cd2 = new NativeAdsHandler.CoroutineWithData(this, this.downloadImage(j));
					yield return cd2.coroutine;
					if (!(bool)cd2.result)
					{
						UnityEngine.Debug.LogError("NativeAds: Failded to load image");
					}
				}
			}
			if (!this.SplashLoadCrossPromotion)
			{
				NativeAdsHandler.ItemContainer itemContainer2 = new NativeAdsHandler.ItemContainer();
				this.randomNumberofApps = UnityEngine.Random.Range(0, this._currentDesc.Items.Count);
				this.id = this._currentDesc.Items[this.randomNumberofApps].Id;
				if (Application.identifier == this.id)
				{
					this.PromoImage.gameObject.SetActive(false);
				}
				else
				{
					this.PromoImage.gameObject.SetActive(true);
					this.PromoImage.sprite = NativeAdsUtils.GetSprite(this.randomNumberofApps);
				}
				itemContainer2.btnAction = this.getBtnAction(this.id);
				this.PlayBtn.onClick.RemoveAllListeners();
				this.PlayBtn.onClick.AddListener(this.getBtnAction(this.id));
				if (!forceReplace && !NativeAdsHandler._updatesChecked)
				{
					this.checkForUpdates();
				}
			}
			yield return new WaitForSeconds(10f);
		}
	}

	private IEnumerator downloadImage(int index)
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			WWW www = new WWW(this._currentDesc.Items[index].IconUrl);
			yield return www;
			if (www.error != null)
			{
				UnityEngine.Debug.LogError("NativeAds: WWW error: " + www.error);
				yield return false;
			}
			else
			{
				File.WriteAllBytes(NativeAdsUtils.GetImagePath(index), www.bytes);
				yield return true;
			}
		}
		yield break;
	}

	private void Start()
	{

		if (NativeAdsUtils.ConfigFileExists())
		{
			this.loadConfig();
			base.StartCoroutine(this.updateUI(false));
		}
		else if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			this.checkForUpdates();
		}
		if (this.logOn)
		{
			UnityEngine.Debug.Log("NativeAds: data path: " + NativeAdsUtils.GetSettingsFilePath());
		}
	}

	public bool SplashLoadCrossPromotion;

	private const string KEY_VERSION = "Version";

	private const string KEY_APPLIST = "AppsList";

	private const string KEY_IOS = "-iOS";

	private const string KEY_ANDROID = "-Android";

	public bool logOn;

	private string configFileUrl;

	public Image PromoImage;

	public Button PlayBtn;

	private NativeAdsHandler.CrossPromoDescriptor _currentDesc;

	private static bool _updatesChecked;

	private int randomNumberofApps;

	private string id;

	public class ItemContainer
	{
		public Sprite sprite;

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

	private class CrossPromoDescriptor
	{
		public CrossPromoDescriptor()
		{
			this.Items = new List<NativeAdsHandler.AppItem>();
		}

		public int AppsCount { get; set; }

		public int Version { get; set; }

		public List<NativeAdsHandler.AppItem> Items { get; set; }

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
