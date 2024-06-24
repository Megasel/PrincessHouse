// dnSpy decompiler from Assembly-CSharp.dll class: tk2dDemoRuntimeSpriteController
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Demo/tk2dDemoRuntimeSpriteController")]
public class tk2dDemoRuntimeSpriteController : MonoBehaviour
{
	private void Start()
	{
		if (this.destroyOnStart != null)
		{
			UnityEngine.Object.Destroy(this.destroyOnStart);
		}
	}

	private void Update()
	{
	}

	private void DestroyData()
	{
		if (this.spriteInstance != null)
		{
			UnityEngine.Object.Destroy(this.spriteInstance.gameObject);
		}
		if (this.spriteCollectionInstance != null)
		{
			UnityEngine.Object.Destroy(this.spriteCollectionInstance.gameObject);
		}
	}

	private void DoDemoTexturePacker(tk2dSpriteCollectionSize spriteCollectionSize)
	{
		if (GUILayout.Button("Import", new GUILayoutOption[0]))
		{
			this.DestroyData();
			this.spriteCollectionInstance = tk2dSpriteCollectionData.CreateFromTexturePacker(spriteCollectionSize, this.texturePackerExportFile.text, this.texturePackerTexture);
			this.spriteInstance = new GameObject("sprite")
			{
				transform = 
				{
					localPosition = new Vector3(-1f, 0f, 0f)
				}
			}.AddComponent<tk2dSprite>();
			this.spriteInstance.SetSprite(this.spriteCollectionInstance, "sun");
			tk2dSprite tk2dSprite = new GameObject("sprite2")
			{
				transform = 
				{
					parent = this.spriteInstance.transform,
					localPosition = new Vector3(2f, 0f, 0f)
				}
			}.AddComponent<tk2dSprite>();
			tk2dSprite.SetSprite(this.spriteCollectionInstance, "2dtoolkit_logo");
			tk2dSprite = new GameObject("sprite3")
			{
				transform = 
				{
					parent = this.spriteInstance.transform,
					localPosition = new Vector3(1f, 1f, 0f)
				}
			}.AddComponent<tk2dSprite>();
			tk2dSprite.SetSprite(this.spriteCollectionInstance, "button_up");
			tk2dSprite = new GameObject("sprite4")
			{
				transform = 
				{
					parent = this.spriteInstance.transform,
					localPosition = new Vector3(1f, -1f, 0f)
				}
			}.AddComponent<tk2dSprite>();
			tk2dSprite.SetSprite(this.spriteCollectionInstance, "Rock");
		}
	}

	private void DoDemoRuntimeSpriteCollection(tk2dSpriteCollectionSize spriteCollectionSize)
	{
		if (GUILayout.Button("Use Full Texture", new GUILayoutOption[0]))
		{
			this.DestroyData();
			Rect region = new Rect(0f, 0f, (float)this.runtimeTexture.width, (float)this.runtimeTexture.height);
			Vector2 anchor = new Vector2(region.width / 2f, region.height / 2f);
			GameObject gameObject = tk2dSprite.CreateFromTexture(this.runtimeTexture, spriteCollectionSize, region, anchor);
			this.spriteInstance = gameObject.GetComponent<tk2dSprite>();
			this.spriteCollectionInstance = this.spriteInstance.Collection;
		}
		if (GUILayout.Button("Extract Region)", new GUILayoutOption[0]))
		{
			this.DestroyData();
			Rect region2 = new Rect(79f, 243f, 215f, 200f);
			Vector2 anchor2 = new Vector2(region2.width / 2f, region2.height / 2f);
			GameObject gameObject2 = tk2dSprite.CreateFromTexture(this.runtimeTexture, spriteCollectionSize, region2, anchor2);
			this.spriteInstance = gameObject2.GetComponent<tk2dSprite>();
			this.spriteCollectionInstance = this.spriteInstance.Collection;
		}
		if (GUILayout.Button("Extract multiple Sprites", new GUILayoutOption[0]))
		{
			this.DestroyData();
			string[] names = new string[]
			{
				"Extracted region",
				"Another region",
				"Full sprite"
			};
			Rect[] array = new Rect[]
			{
				new Rect(79f, 243f, 215f, 200f),
				new Rect(256f, 0f, 64f, 64f),
				new Rect(0f, 0f, (float)this.runtimeTexture.width, (float)this.runtimeTexture.height)
			};
			Vector2[] anchors = new Vector2[]
			{
				new Vector2(array[0].width / 2f, array[0].height / 2f),
				new Vector2(0f, array[1].height),
				new Vector2(0f, array[1].height)
			};
			this.spriteCollectionInstance = tk2dSpriteCollectionData.CreateFromTexture(this.runtimeTexture, spriteCollectionSize, names, array, anchors);
			this.spriteInstance = new GameObject("sprite")
			{
				transform = 
				{
					localPosition = new Vector3(-1f, 0f, 0f)
				}
			}.AddComponent<tk2dSprite>();
			this.spriteInstance.SetSprite(this.spriteCollectionInstance, 0);
			tk2dSprite tk2dSprite = new GameObject("sprite2")
			{
				transform = 
				{
					parent = this.spriteInstance.transform,
					localPosition = new Vector3(2f, 0f, 0f)
				}
			}.AddComponent<tk2dSprite>();
			tk2dSprite.SetSprite(this.spriteCollectionInstance, "Another region");
		}
	}

	private void OnGUI()
	{
		tk2dSpriteCollectionSize spriteCollectionSize = tk2dSpriteCollectionSize.Explicit(5f, 640f);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.BeginVertical("box", new GUILayoutOption[0]);
		GUILayout.Label("Runtime Sprite Collection", new GUILayoutOption[0]);
		this.DoDemoRuntimeSpriteCollection(spriteCollectionSize);
		GUILayout.EndVertical();
		GUILayout.BeginVertical("box", new GUILayoutOption[0]);
		GUILayout.Label("Texture Packer Import", new GUILayoutOption[0]);
		this.DoDemoTexturePacker(spriteCollectionSize);
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}

	public Texture2D runtimeTexture;

	public Texture2D texturePackerTexture;

	public TextAsset texturePackerExportFile;

	public GameObject destroyOnStart;

	private tk2dBaseSprite spriteInstance;

	private tk2dSpriteCollectionData spriteCollectionInstance;
}
