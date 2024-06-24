// dnSpy decompiler from Assembly-CSharp.dll class: tk2dFontData
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Backend/tk2dFontData")]
public class tk2dFontData : MonoBehaviour
{
	public tk2dFontData inst
	{
		get
		{
			if (this.platformSpecificData == null || this.platformSpecificData.materialInst == null)
			{
				if (this.hasPlatformData)
				{
					string currentPlatform = tk2dSystem.CurrentPlatform;
					string text = string.Empty;
					for (int i = 0; i < this.fontPlatforms.Length; i++)
					{
						if (this.fontPlatforms[i] == currentPlatform)
						{
							text = this.fontPlatformGUIDs[i];
							break;
						}
					}
					if (text.Length == 0)
					{
						text = this.fontPlatformGUIDs[0];
					}
					this.platformSpecificData = tk2dSystem.LoadResourceByGUID<tk2dFontData>(text);
				}
				else
				{
					this.platformSpecificData = this;
				}
				this.platformSpecificData.Init();
			}
			return this.platformSpecificData;
		}
	}

	private void Init()
	{
		if (this.needMaterialInstance)
		{
			if (this.spriteCollection)
			{
				tk2dSpriteCollectionData inst = this.spriteCollection.inst;
				for (int i = 0; i < inst.materials.Length; i++)
				{
					if (inst.materials[i] == this.material)
					{
						this.materialInst = inst.materialInsts[i];
						break;
					}
				}
				if (this.materialInst == null && !this.needMaterialInstance)
				{
					UnityEngine.Debug.LogError("Fatal error - font from sprite collection is has an invalid material");
				}
			}
			else
			{
				this.materialInst = UnityEngine.Object.Instantiate<Material>(this.material);
				this.materialInst.hideFlags = HideFlags.DontSave;
			}
		}
		else
		{
			this.materialInst = this.material;
		}
	}

	public void ResetPlatformData()
	{
		if (this.hasPlatformData && this.platformSpecificData)
		{
			this.platformSpecificData = null;
		}
		this.materialInst = null;
	}

	private void OnDestroy()
	{
		if (this.needMaterialInstance && this.spriteCollection == null)
		{
			UnityEngine.Object.DestroyImmediate(this.materialInst);
		}
	}

	public void InitDictionary()
	{
		if (this.useDictionary && this.charDict == null)
		{
			this.charDict = new Dictionary<int, tk2dFontChar>(this.charDictKeys.Count);
			for (int i = 0; i < this.charDictKeys.Count; i++)
			{
				this.charDict[this.charDictKeys[i]] = this.charDictValues[i];
			}
		}
	}

	public void SetDictionary(Dictionary<int, tk2dFontChar> dict)
	{
		this.charDictKeys = new List<int>(dict.Keys);
		this.charDictValues = new List<tk2dFontChar>();
		for (int i = 0; i < this.charDictKeys.Count; i++)
		{
			this.charDictValues.Add(dict[this.charDictKeys[i]]);
		}
	}

	public const int CURRENT_VERSION = 2;

	[HideInInspector]
	public int version;

	public float lineHeight;

	public tk2dFontChar[] chars;

	[SerializeField]
	private List<int> charDictKeys;

	[SerializeField]
	private List<tk2dFontChar> charDictValues;

	public string[] fontPlatforms;

	public string[] fontPlatformGUIDs;

	private tk2dFontData platformSpecificData;

	public bool hasPlatformData;

	public bool managedFont;

	public bool needMaterialInstance;

	public bool isPacked;

	public bool premultipliedAlpha;

	public tk2dSpriteCollectionData spriteCollection;

	public Dictionary<int, tk2dFontChar> charDict;

	public bool useDictionary;

	public tk2dFontKerning[] kerning;

	public float largestWidth;

	public Material material;

	[NonSerialized]
	public Material materialInst;

	public Texture2D gradientTexture;

	public bool textureGradients;

	public int gradientCount = 1;

	public Vector2 texelSize;

	[HideInInspector]
	public float invOrthoSize = 1f;

	[HideInInspector]
	public float halfTargetHeight = 1f;
}
