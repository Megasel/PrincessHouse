// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteFromTexture
using System;
using tk2dRuntime;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Sprite/tk2dSpriteFromTexture")]
[ExecuteInEditMode]
public class tk2dSpriteFromTexture : MonoBehaviour
{
	private tk2dBaseSprite Sprite
	{
		get
		{
			if (this._sprite == null)
			{
				this._sprite = base.GetComponent<tk2dBaseSprite>();
				if (this._sprite == null)
				{
					UnityEngine.Debug.Log("tk2dSpriteFromTexture - Missing sprite object. Creating.");
					this._sprite = base.gameObject.AddComponent<tk2dSprite>();
				}
			}
			return this._sprite;
		}
	}

	private void Awake()
	{
		this.Create(this.spriteCollectionSize, this.texture, this.anchor);
	}

	public bool HasSpriteCollection
	{
		get
		{
			return this.spriteCollection != null;
		}
	}

	private void OnDestroy()
	{
		this.DestroyInternal();
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			component.material = null;
		}
	}

	public void Create(tk2dSpriteCollectionSize spriteCollectionSize, Texture texture, tk2dBaseSprite.Anchor anchor)
	{
		this.DestroyInternal();
		if (texture != null)
		{
			this.spriteCollectionSize.CopyFrom(spriteCollectionSize);
			this.texture = texture;
			this.anchor = anchor;
			GameObject gameObject = new GameObject("tk2dSpriteFromTexture - " + texture.name);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			gameObject.hideFlags = HideFlags.DontSave;
			Vector2 anchorOffset = tk2dSpriteGeomGen.GetAnchorOffset(anchor, (float)texture.width, (float)texture.height);
			this.spriteCollection = SpriteCollectionGenerator.CreateFromTexture(gameObject, texture, spriteCollectionSize, new Vector2((float)texture.width, (float)texture.height), new string[]
			{
				"unnamed"
			}, new Rect[]
			{
				new Rect(0f, 0f, (float)texture.width, (float)texture.height)
			}, null, new Vector2[]
			{
				anchorOffset
			}, new bool[1]);
			string text = "SpriteFromTexture " + texture.name;
			this.spriteCollection.spriteCollectionName = text;
			this.spriteCollection.spriteDefinitions[0].material.name = text;
			this.spriteCollection.spriteDefinitions[0].material.hideFlags = (HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			this.Sprite.SetSprite(this.spriteCollection, 0);
		}
	}

	public void Clear()
	{
		this.DestroyInternal();
	}

	public void ForceBuild()
	{
		this.DestroyInternal();
		this.Create(this.spriteCollectionSize, this.texture, this.anchor);
	}

	private void DestroyInternal()
	{
		if (this.spriteCollection != null)
		{
			if (this.spriteCollection.spriteDefinitions[0].material != null)
			{
				UnityEngine.Object.DestroyImmediate(this.spriteCollection.spriteDefinitions[0].material);
			}
			UnityEngine.Object.DestroyImmediate(this.spriteCollection.gameObject);
			this.spriteCollection = null;
		}
	}

	public Texture texture;

	public tk2dSpriteCollectionSize spriteCollectionSize = new tk2dSpriteCollectionSize();

	public tk2dBaseSprite.Anchor anchor = tk2dBaseSprite.Anchor.MiddleCenter;

	private tk2dSpriteCollectionData spriteCollection;

	private tk2dBaseSprite _sprite;
}
