//// dnSpy decompiler from Assembly-CSharp.dll class: SelfCrossPromotionHandler
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using Area730.SelfCrossPromo;
//using SimpleJSON;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//public class SelfCrossPromotionHandler : MonoBehaviour
//{
//	private static SelfCrossPromotionHandler.AppItem parseItem(JSONNode node)
//	{
//		return new SelfCrossPromotionHandler.AppItem
//		{
//			AppTitle = node["AppTitle"],
//			IconUrl = node["IconUrl"],
//			Id = node["Id"]
//		};
//	}

//	private static SelfCrossPromotionHandler.CrossPromoDescriptor parseResponse(string json)
//	{
//		string str = "-Android";
//		try
//		{
//			SelfCrossPromotionHandler.CrossPromoDescriptor crossPromoDescriptor = new SelfCrossPromotionHandler.CrossPromoDescriptor();
//			JSONNode jsonnode = JSON.Parse(json);
//			crossPromoDescriptor.Version = jsonnode["Version"].AsInt;
//			JSONArray asArray = jsonnode["AppsList" + str].AsArray;
//			crossPromoDescriptor.AppsCount = asArray.Count;
//			for (int i = 0; i < asArray.Count; i++)
//			{
//				SelfCrossPromotionHandler.AppItem appItem = SelfCrossPromotionHandler.parseItem(asArray[i]);
//				if (Application.identifier != appItem.Id)
//				{
//					crossPromoDescriptor.Items.Add(appItem);
//				}
//			}
//			return crossPromoDescriptor;
//		}
//		catch (Exception ex)
//		{
//			UnityEngine.Debug.LogError("SelfCrossPromo: Json parse error");
//		}
//		return null;
//	}

//	private IEnumerator DownloadJsonDescriptor()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			WWW www = new WWW(this.configFileUrl);
//			yield return www;
//			if (www.error != null)
//			{
//				UnityEngine.Debug.LogError("SelfCrossPromo: WWW error: " + www.error);
//			}
//			else
//			{
//				if (this.logOn)
//				{
//					UnityEngine.Debug.Log("SelfCrossPromo: Descriptor downloaded");
//				}
//				yield return base.StartCoroutine(this.updateMoreAppsPage(www.text));
//			}
//		}
//		yield break;
//	}

//	private UnityAction getBtnAction(string id)
//	{
//		return delegate()
//		{
//			string url = "https://play.google.com/store/apps/details?id=" + id;
//			Application.OpenURL(url);
//		};
//	}

//	private void checkForUpdates()
//	{
//		SelfCrossPromotionHandler._updatesChecked = true;
//		base.StartCoroutine(this.DownloadJsonDescriptor());
//	}

//	private void loadConfig()
//	{
//		string json = File.ReadAllText(SelfCrossPromoUtils.GetSettingsFilePath());
//		this._currentDesc = SelfCrossPromotionHandler.parseResponse(json);
//	}

//	private IEnumerator updateMoreAppsPage(string responseJson)
//	{
//		SelfCrossPromotionHandler.CrossPromoDescriptor desc = SelfCrossPromotionHandler.parseResponse(responseJson);
//		if (desc == null)
//		{
//			UnityEngine.Debug.LogError("SelfCrossPromo: Error parsing downloaded descriptor file");
//			yield break;
//		}
//		if (SelfCrossPromoUtils.ConfigFileExists())
//		{
//			if (desc.Version > this._currentDesc.Version)
//			{
//				for (int i = desc.AppsCount; i < this._currentDesc.AppsCount; i++)
//				{
//					string imagePath = SelfCrossPromoUtils.GetImagePath(i);
//					if (File.Exists(imagePath))
//					{
//						File.Delete(imagePath);
//					}
//				}
//				File.WriteAllText(SelfCrossPromoUtils.GetSettingsFilePath(), responseJson);
//				this._currentDesc = desc;
//				yield return base.StartCoroutine(this.updateUI(true));
//			}
//			else if (this.logOn)
//			{
//				UnityEngine.Debug.Log("SelfCrossPromo: Downloaded version is not new.");
//			}
//		}
//		else
//		{
//			if (this.logOn)
//			{
//				UnityEngine.Debug.Log("SelfCrossPromo: First time file load");
//			}
//			File.WriteAllText(SelfCrossPromoUtils.GetSettingsFilePath(), responseJson);
//			this._currentDesc = desc;
//			yield return base.StartCoroutine(this.updateUI(false));
//		}
//		yield break;
//	}

//	private IEnumerator updateUI(bool forceReplace = false)
//	{
//		if (this._currentDesc == null)
//		{
//			UnityEngine.Debug.LogError("SelfCrossPromo: desciptor is NULL");
//			yield break;
//		}
//		for (int i = 0; i < this._currentDesc.AppsCount; i++)
//		{
//			if (!File.Exists(SelfCrossPromoUtils.GetImagePath(i)) || forceReplace)
//			{
//				SelfCrossPromotionHandler.CoroutineWithData cd = new SelfCrossPromotionHandler.CoroutineWithData(this, this.downloadImage(i));
//				yield return cd.coroutine;
//				if (!(bool)cd.result)
//				{
//					UnityEngine.Debug.LogError("SelfCrossPromo: Failded to load image");
//				}
//			}
//		}
//		if (!this.SplashLoadCrossPromotion)
//		{
//			SelfCrossPromotionHandler.ItemContainer itemContainer = new SelfCrossPromotionHandler.ItemContainer();
//			this.randomNumberofApps = UnityEngine.Random.Range(0, this._currentDesc.Items.Count);
//			this.id = this._currentDesc.Items[this.randomNumberofApps].Id;
//			if (Application.identifier == this.id)
//			{
//				this.PromoImage.enabled = false;
//			}
//			else
//			{
//				this.PromoImage.enabled = true;
//				this.PromoImage.sprite = SelfCrossPromoUtils.GetSprite(this.randomNumberofApps);
//			}
//			itemContainer.btnAction = this.getBtnAction(this.id);
//			this.PlayBtn.onClick.RemoveAllListeners();
//			this.OkBtn.onClick.RemoveAllListeners();
//			this.PlayBtn.onClick.AddListener(this.getBtnAction(this.id));
//			this.OkBtn.onClick.AddListener(this.getBtnAction(this.id));
//			if (!forceReplace && !SelfCrossPromotionHandler._updatesChecked)
//			{
//				this.checkForUpdates();
//			}
//		}
//		yield break;
//	}

//	private IEnumerator downloadImage(int index)
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			WWW www = new WWW(this._currentDesc.Items[index].IconUrl);
//			yield return www;
//			if (www.error != null)
//			{
//				UnityEngine.Debug.LogError("SelfCrossPromo: WWW error: " + www.error);
//				yield return false;
//			}
//			else
//			{
//				File.WriteAllBytes(SelfCrossPromoUtils.GetImagePath(index), www.bytes);
//				yield return true;
//			}
//		}
//		yield break;
//	}

//	private void Start()
//	{
//		this.configFileUrl = AssignAdIds.instance.loadingScreenLink;
//		if (SelfCrossPromoUtils.ConfigFileExists())
//		{
//			this.loadConfig();
//			base.StartCoroutine(this.updateUI(false));
//		}
//		else if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			this.checkForUpdates();
//		}
//		if (this.logOn)
//		{
//			UnityEngine.Debug.Log("SelfCrossPromo: data path: " + SelfCrossPromoUtils.GetSettingsFilePath());
//		}
//	}

//	public bool SplashLoadCrossPromotion;

//	private const string KEY_VERSION = "Version";

//	private const string KEY_APPLIST = "AppsList";

//	private const string KEY_IOS = "-iOS";

//	private const string KEY_ANDROID = "-Android";

//	public bool logOn;

//	private string configFileUrl;

//	public Image PromoImage;

//	public Button PlayBtn;

//	public Button OkBtn;

//	public GameObject PromoWindow;

//	private SelfCrossPromotionHandler.CrossPromoDescriptor _currentDesc;

//	private static bool _updatesChecked;

//	private int randomNumberofApps;

//	private string id;

//	public class ItemContainer
//	{
//		public Sprite sprite;

//		public UnityAction btnAction;
//	}

//	private class AppItem
//	{
//		public string AppTitle { get; set; }

//		public string IconUrl { get; set; }

//		public string Id { get; set; }

//		public void Print()
//		{
//			UnityEngine.Debug.Log(string.Concat(new string[]
//			{
//				"Title: ",
//				this.AppTitle,
//				", icon url: ",
//				this.IconUrl,
//				", id: ",
//				this.Id
//			}));
//		}
//	}

//	private class CrossPromoDescriptor
//	{
//		public CrossPromoDescriptor()
//		{
//			this.Items = new List<SelfCrossPromotionHandler.AppItem>();
//		}

//		public int AppsCount { get; set; }

//		public int Version { get; set; }

//		public List<SelfCrossPromotionHandler.AppItem> Items { get; set; }

//		public void Print()
//		{
//			UnityEngine.Debug.Log(string.Concat(new object[]
//			{
//				"Version: ",
//				this.Version,
//				", AppsCount: ",
//				this.AppsCount
//			}));
//		}
//	}

//	private class CoroutineWithData
//	{
//		public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
//		{
//			this.target = target;
//			this.coroutine = owner.StartCoroutine(this.Run());
//		}

//		public Coroutine coroutine { get; private set; }

//		private IEnumerator Run()
//		{
//			while (this.target.MoveNext())
//			{
//				this.result = this.target.Current;
//				yield return this.result;
//			}
//			yield break;
//		}

//		public object result;

//		private IEnumerator target;
//	}
//}
