// dnSpy decompiler from Assembly-CSharp.dll class: tk2dSpriteAttachPoint
using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("2D Toolkit/Sprite/tk2dSpriteAttachPoint")]
public class tk2dSpriteAttachPoint : MonoBehaviour
{
	private void Awake()
	{
		if (this.sprite == null)
		{
			this.sprite = base.GetComponent<tk2dBaseSprite>();
			if (this.sprite != null)
			{
				this.HandleSpriteChanged(this.sprite);
			}
		}
	}

	private void OnEnable()
	{
		if (this.sprite != null)
		{
			this.sprite.SpriteChanged += this.HandleSpriteChanged;
		}
	}

	private void OnDisable()
	{
		if (this.sprite != null)
		{
			this.sprite.SpriteChanged -= this.HandleSpriteChanged;
		}
	}

	private void UpdateAttachPointTransform(tk2dSpriteDefinition.AttachPoint attachPoint, Transform t)
	{
		t.localPosition = Vector3.Scale(attachPoint.position, this.sprite.scale);
		t.localScale = this.sprite.scale;
		float num = Mathf.Sign(this.sprite.scale.x) * Mathf.Sign(this.sprite.scale.y);
		t.localEulerAngles = new Vector3(0f, 0f, attachPoint.angle * num);
	}

	private string GetInstanceName(Transform t)
	{
		string empty = string.Empty;
		if (this.cachedInstanceNames.TryGetValue(t, out empty))
		{
			return empty;
		}
		this.cachedInstanceNames[t] = t.name;
		return t.name;
	}

	private void HandleSpriteChanged(tk2dBaseSprite spr)
	{
		tk2dSpriteDefinition currentSprite = spr.CurrentSprite;
		int num = Mathf.Max(currentSprite.attachPoints.Length, this.attachPoints.Count);
		if (num > tk2dSpriteAttachPoint.attachPointUpdated.Length)
		{
			tk2dSpriteAttachPoint.attachPointUpdated = new bool[num];
		}
		foreach (tk2dSpriteDefinition.AttachPoint attachPoint in currentSprite.attachPoints)
		{
			bool flag = false;
			int num2 = 0;
			for (int j = 0; j < this.attachPoints.Count; j++)
			{
				Transform transform = this.attachPoints[j];
				if (transform != null && this.GetInstanceName(transform) == attachPoint.name)
				{
					tk2dSpriteAttachPoint.attachPointUpdated[num2] = true;
					this.UpdateAttachPointTransform(attachPoint, transform);
					flag = true;
				}
				num2++;
			}
			if (!flag)
			{
				GameObject gameObject = new GameObject(attachPoint.name);
				Transform transform2 = gameObject.transform;
				transform2.parent = base.transform;
				this.UpdateAttachPointTransform(attachPoint, transform2);
				tk2dSpriteAttachPoint.attachPointUpdated[this.attachPoints.Count] = true;
				this.attachPoints.Add(transform2);
			}
		}
		if (this.deactivateUnusedAttachPoints)
		{
			for (int k = 0; k < this.attachPoints.Count; k++)
			{
				if (this.attachPoints[k] != null)
				{
					GameObject gameObject2 = this.attachPoints[k].gameObject;
					if (tk2dSpriteAttachPoint.attachPointUpdated[k] && !gameObject2.activeSelf)
					{
						gameObject2.SetActive(true);
					}
					else if (!tk2dSpriteAttachPoint.attachPointUpdated[k] && gameObject2.activeSelf)
					{
						gameObject2.SetActive(false);
					}
				}
				tk2dSpriteAttachPoint.attachPointUpdated[k] = false;
			}
		}
	}

	private tk2dBaseSprite sprite;

	public List<Transform> attachPoints = new List<Transform>();

	private static bool[] attachPointUpdated = new bool[32];

	public bool deactivateUnusedAttachPoints;

	private Dictionary<Transform, string> cachedInstanceNames = new Dictionary<Transform, string>();
}
