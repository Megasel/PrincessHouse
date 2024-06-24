// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteCollectionData
using System;
using System.Collections.Generic;
using tk2dRuntime;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Backend/tk2dSpriteCollectionData")]
public class tk2dSpriteCollectionData : MonoBehaviour
{
	public bool Transient { get; set; }

	public int Count
	{
		get
		{
			return this.inst.spriteDefinitions.Length;
		}
	}

	public int GetSpriteIdByName(string name)
	{
		return this.GetSpriteIdByName(name, 0);
	}

	public int GetSpriteIdByName(string name, int defaultValue)
	{
		this.inst.InitDictionary();
		int result = defaultValue;
		if (!this.inst.spriteNameLookupDict.TryGetValue(name, out result))
		{
			return defaultValue;
		}
		return result;
	}

	public void ClearDictionary()
	{
		this.spriteNameLookupDict = null;
	}

	public tk2dSpriteDefinition GetSpriteDefinition(string name)
	{
		int spriteIdByName = this.GetSpriteIdByName(name, -1);
		if (spriteIdByName == -1)
		{
			return null;
		}
		return this.spriteDefinitions[spriteIdByName];
	}

	public void InitDictionary()
	{
		if (this.spriteNameLookupDict == null)
		{
			this.spriteNameLookupDict = new Dictionary<string, int>(this.spriteDefinitions.Length);
			for (int i = 0; i < this.spriteDefinitions.Length; i++)
			{
				this.spriteNameLookupDict[this.spriteDefinitions[i].name] = i;
			}
		}
	}

	public tk2dSpriteDefinition FirstValidDefinition
	{
		get
		{
			foreach (tk2dSpriteDefinition tk2dSpriteDefinition in this.inst.spriteDefinitions)
			{
				if (tk2dSpriteDefinition.Valid)
				{
					return tk2dSpriteDefinition;
				}
			}
			return null;
		}
	}

	public bool IsValidSpriteId(int id)
	{
		return id >= 0 && id < this.inst.spriteDefinitions.Length && this.inst.spriteDefinitions[id].Valid;
	}

	public int FirstValidDefinitionIndex
	{
		get
		{
			tk2dSpriteCollectionData inst = this.inst;
			for (int i = 0; i < inst.spriteDefinitions.Length; i++)
			{
				if (inst.spriteDefinitions[i].Valid)
				{
					return i;
				}
			}
			return -1;
		}
	}

	public void InitMaterialIds()
	{
		if (this.inst.materialIdsValid)
		{
			return;
		}
		int num = -1;
		Dictionary<Material, int> dictionary = new Dictionary<Material, int>();
		for (int i = 0; i < this.inst.materials.Length; i++)
		{
			if (num == -1 && this.inst.materials[i] != null)
			{
				num = i;
			}
			dictionary[this.materials[i]] = i;
		}
		if (num == -1)
		{
			UnityEngine.Debug.LogError("Init material ids failed.");
		}
		else
		{
			foreach (tk2dSpriteDefinition tk2dSpriteDefinition in this.inst.spriteDefinitions)
			{
				if (!dictionary.TryGetValue(tk2dSpriteDefinition.material, out tk2dSpriteDefinition.materialId))
				{
					tk2dSpriteDefinition.materialId = num;
				}
			}
			this.inst.materialIdsValid = true;
		}
	}

	public tk2dSpriteCollectionData inst
	{
		get
		{
			if (this.platformSpecificData == null)
			{
				if (this.hasPlatformData)
				{
					string currentPlatform = tk2dSystem.CurrentPlatform;
					string text = string.Empty;
					for (int i = 0; i < this.spriteCollectionPlatforms.Length; i++)
					{
						if (this.spriteCollectionPlatforms[i] == currentPlatform)
						{
							text = this.spriteCollectionPlatformGUIDs[i];
							break;
						}
					}
					if (text.Length == 0)
					{
						text = this.spriteCollectionPlatformGUIDs[0];
					}
					this.platformSpecificData = tk2dSystem.LoadResourceByGUID<tk2dSpriteCollectionData>(text);
				}
				else
				{
					this.platformSpecificData = this;
				}
			}
			this.platformSpecificData.Init();
			return this.platformSpecificData;
		}
	}

	private void Init()
	{
		if (this.materialInsts != null)
		{
			return;
		}
		if (this.spriteDefinitions == null)
		{
			this.spriteDefinitions = new tk2dSpriteDefinition[0];
		}
		if (this.materials == null)
		{
			this.materials = new Material[0];
		}
		this.materialInsts = new Material[this.materials.Length];
		if (this.needMaterialInstance)
		{
			if (tk2dSystem.OverrideBuildMaterial)
			{
				for (int i = 0; i < this.materials.Length; i++)
				{
					this.materialInsts[i] = new Material(Shader.Find("tk2d/BlendVertexColor"));
				}
			}
			else
			{
				bool flag = false;
				if (this.pngTextures.Length > 0)
				{
					flag = true;
					this.textureInsts = new Texture2D[this.pngTextures.Length];
					for (int j = 0; j < this.pngTextures.Length; j++)
					{
						Texture2D texture2D = new Texture2D(4, 4, TextureFormat.ARGB32, this.textureMipMaps);
						texture2D.LoadImage(this.pngTextures[j].bytes);
						this.textureInsts[j] = texture2D;
						texture2D.filterMode = this.textureFilterMode;
						texture2D.Apply(this.textureMipMaps, true);
					}
				}
				for (int k = 0; k < this.materials.Length; k++)
				{
					this.materialInsts[k] = UnityEngine.Object.Instantiate<Material>(this.materials[k]);
					if (flag)
					{
						int num = (this.materialPngTextureId.Length != 0) ? this.materialPngTextureId[k] : 0;
						this.materialInsts[k].mainTexture = this.textureInsts[num];
					}
				}
			}
			for (int l = 0; l < this.spriteDefinitions.Length; l++)
			{
				tk2dSpriteDefinition tk2dSpriteDefinition = this.spriteDefinitions[l];
				tk2dSpriteDefinition.materialInst = this.materialInsts[tk2dSpriteDefinition.materialId];
			}
		}
		else
		{
			for (int m = 0; m < this.materials.Length; m++)
			{
				this.materialInsts[m] = this.materials[m];
			}
			for (int n = 0; n < this.spriteDefinitions.Length; n++)
			{
				tk2dSpriteDefinition tk2dSpriteDefinition2 = this.spriteDefinitions[n];
				tk2dSpriteDefinition2.materialInst = tk2dSpriteDefinition2.material;
			}
		}
		tk2dEditorSpriteDataUnloader.Register(this);
	}

	public static tk2dSpriteCollectionData CreateFromTexture(Texture texture, tk2dSpriteCollectionSize size, string[] names, Rect[] regions, Vector2[] anchors)
	{
		return SpriteCollectionGenerator.CreateFromTexture(texture, size, names, regions, anchors);
	}

	public static tk2dSpriteCollectionData CreateFromTexturePacker(tk2dSpriteCollectionSize size, string texturePackerData, Texture texture)
	{
		return SpriteCollectionGenerator.CreateFromTexturePacker(size, texturePackerData, texture);
	}

	public void ResetPlatformData()
	{
		tk2dEditorSpriteDataUnloader.Unregister(this);
		if (this.platformSpecificData != null)
		{
			this.platformSpecificData.DestroyTextureInsts();
		}
		this.DestroyTextureInsts();
		if (this.platformSpecificData)
		{
			this.platformSpecificData = null;
		}
		this.materialInsts = null;
	}

	private void DestroyTextureInsts()
	{
		foreach (Texture2D obj in this.textureInsts)
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
		this.textureInsts = new Texture2D[0];
	}

	public void UnloadTextures()
	{
		tk2dSpriteCollectionData inst = this.inst;
		foreach (Texture2D assetToUnload in inst.textures)
		{
			Resources.UnloadAsset(assetToUnload);
		}
		inst.DestroyMaterialInsts();
		inst.DestroyTextureInsts();
	}

	private void DestroyMaterialInsts()
	{
		if (this.needMaterialInstance)
		{
			foreach (Material obj in this.materialInsts)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
		this.materialInsts = null;
	}

	private void OnDestroy()
	{
		if (this.Transient)
		{
			foreach (Material obj in this.materials)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
		else if (this.needMaterialInstance)
		{
			foreach (Material obj2 in this.materialInsts)
			{
				UnityEngine.Object.DestroyImmediate(obj2);
			}
			this.materialInsts = new Material[0];
			foreach (Texture2D obj3 in this.textureInsts)
			{
				UnityEngine.Object.DestroyImmediate(obj3);
			}
			this.textureInsts = new Texture2D[0];
		}
		this.ResetPlatformData();
	}

	public const int CURRENT_VERSION = 3;

	public int version;

	public bool materialIdsValid;

	public bool needMaterialInstance;

	public tk2dSpriteDefinition[] spriteDefinitions;

	private Dictionary<string, int> spriteNameLookupDict;

	public bool premultipliedAlpha;

	public Material material;

	public Material[] materials;

	[NonSerialized]
	public Material[] materialInsts;

	[NonSerialized]
	public Texture2D[] textureInsts = new Texture2D[0];

	public Texture[] textures;

	public TextAsset[] pngTextures = new TextAsset[0];

	public int[] materialPngTextureId = new int[0];

	public FilterMode textureFilterMode = FilterMode.Bilinear;

	public bool textureMipMaps;

	public bool allowMultipleAtlases;

	public string spriteCollectionGUID;

	public string spriteCollectionName;

	public string assetName = string.Empty;

	public bool loadable;

	public float invOrthoSize = 1f;

	public float halfTargetHeight = 1f;

	public int buildKey;

	public string dataGuid = string.Empty;

	public bool managedSpriteCollection;

	public bool hasPlatformData;

	public string[] spriteCollectionPlatforms;

	public string[] spriteCollectionPlatformGUIDs;

	private tk2dSpriteCollectionData platformSpecificData;

	public static readonly string internalResourcePrefix = "tk2dInternal$.";
}
