// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSystem
using System;
using UnityEngine;

public class tk2dSystem : ScriptableObject
{
	private tk2dSystem()
	{
	}

	public static tk2dSystem inst
	{
		get
		{
			if (tk2dSystem._inst == null)
			{
				tk2dSystem._inst = (Resources.Load("tk2d/tk2dSystem", typeof(tk2dSystem)) as tk2dSystem);
				if (tk2dSystem._inst == null)
				{
					tk2dSystem._inst = ScriptableObject.CreateInstance<tk2dSystem>();
				}
				UnityEngine.Object.DontDestroyOnLoad(tk2dSystem._inst);
			}
			return tk2dSystem._inst;
		}
	}

	public static tk2dSystem inst_NoCreate
	{
		get
		{
			if (tk2dSystem._inst == null)
			{
				tk2dSystem._inst = (Resources.Load("tk2d/tk2dSystem", typeof(tk2dSystem)) as tk2dSystem);
			}
			return tk2dSystem._inst;
		}
	}

	public static string CurrentPlatform
	{
		get
		{
			return tk2dSystem.currentPlatform;
		}
		set
		{
			if (value != tk2dSystem.currentPlatform)
			{
				tk2dSystem.currentPlatform = value;
			}
		}
	}

	public static bool OverrideBuildMaterial
	{
		get
		{
			return false;
		}
	}

	public static tk2dAssetPlatform GetAssetPlatform(string platform)
	{
		tk2dSystem inst_NoCreate = tk2dSystem.inst_NoCreate;
		if (inst_NoCreate == null)
		{
			return null;
		}
		for (int i = 0; i < inst_NoCreate.assetPlatforms.Length; i++)
		{
			if (inst_NoCreate.assetPlatforms[i].name == platform)
			{
				return inst_NoCreate.assetPlatforms[i];
			}
		}
		return null;
	}

	private T LoadResourceByGUIDImpl<T>(string guid) where T : UnityEngine.Object
	{
		tk2dResource tk2dResource = Resources.Load("tk2d/tk2d_" + guid, typeof(tk2dResource)) as tk2dResource;
		if (tk2dResource != null)
		{
			return tk2dResource.objectReference as T;
		}
		return (T)((object)null);
	}

	private T LoadResourceByNameImpl<T>(string name) where T : UnityEngine.Object
	{
		for (int i = 0; i < this.allResourceEntries.Length; i++)
		{
			if (this.allResourceEntries[i] != null && this.allResourceEntries[i].assetName == name)
			{
				return this.LoadResourceByGUIDImpl<T>(this.allResourceEntries[i].assetGUID);
			}
		}
		return (T)((object)null);
	}

	public static T LoadResourceByGUID<T>(string guid) where T : UnityEngine.Object
	{
		return tk2dSystem.inst.LoadResourceByGUIDImpl<T>(guid);
	}

	public static T LoadResourceByName<T>(string guid) where T : UnityEngine.Object
	{
		return tk2dSystem.inst.LoadResourceByNameImpl<T>(guid);
	}

	public const string guidPrefix = "tk2d/tk2d_";

	public const string assetName = "tk2d/tk2dSystem";

	public const string assetFileName = "tk2dSystem.asset";

	[NonSerialized]
	public tk2dAssetPlatform[] assetPlatforms = new tk2dAssetPlatform[]
	{
		new tk2dAssetPlatform("1x", 1f),
		new tk2dAssetPlatform("2x", 2f),
		new tk2dAssetPlatform("4x", 4f)
	};

	private static tk2dSystem _inst;

	private static string currentPlatform = string.Empty;

	[SerializeField]
	private tk2dResourceTocEntry[] allResourceEntries = new tk2dResourceTocEntry[0];
}
