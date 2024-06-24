// dnSpy decompiler from Assembly-CSharp.dll class: tk2dButton
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Deprecated/GUI/tk2dButton")]
public class tk2dButton : MonoBehaviour
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event tk2dButton.ButtonHandlerDelegate ButtonPressedEvent;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event tk2dButton.ButtonHandlerDelegate ButtonAutoFireEvent;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event tk2dButton.ButtonHandlerDelegate ButtonDownEvent;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event tk2dButton.ButtonHandlerDelegate ButtonUpEvent;

	private void OnEnable()
	{
		this.buttonDown = false;
	}

	private void Start()
	{
		if (this.viewCamera == null)
		{
			Transform transform = base.transform;
			while (transform && transform.GetComponent<Camera>() == null)
			{
				transform = transform.parent;
			}
			if (transform && transform.GetComponent<Camera>() != null)
			{
				this.viewCamera = transform.GetComponent<Camera>();
			}
			if (this.viewCamera == null && tk2dCamera.Instance)
			{
				this.viewCamera = tk2dCamera.Instance.GetComponent<Camera>();
			}
			if (this.viewCamera == null)
			{
				this.viewCamera = Camera.main;
			}
		}
		this.sprite = base.GetComponent<tk2dBaseSprite>();
		if (this.sprite)
		{
		}
		if (base.GetComponent<Collider>() == null)
		{
			BoxCollider boxCollider = base.gameObject.AddComponent<BoxCollider>();
			Vector3 size = boxCollider.size;
			size.z = 0.2f;
			boxCollider.size = size;
		}
		if ((this.buttonDownSound != null || this.buttonPressedSound != null || this.buttonUpSound != null) && base.GetComponent<AudioSource>() == null)
		{
			AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
		}
	}

	public void UpdateSpriteIds()
	{
		this.buttonDownSpriteId = ((this.buttonDownSprite.Length <= 0) ? -1 : this.sprite.GetSpriteIdByName(this.buttonDownSprite));
		this.buttonUpSpriteId = ((this.buttonUpSprite.Length <= 0) ? -1 : this.sprite.GetSpriteIdByName(this.buttonUpSprite));
		this.buttonPressedSpriteId = ((this.buttonPressedSprite.Length <= 0) ? -1 : this.sprite.GetSpriteIdByName(this.buttonPressedSprite));
	}

	private void PlaySound(AudioClip source)
	{
		if (base.GetComponent<AudioSource>() && source)
		{
			base.GetComponent<AudioSource>().PlayOneShot(source);
		}
	}

	private IEnumerator coScale(Vector3 defaultScale, float startScale, float endScale)
	{
		float t0 = Time.realtimeSinceStartup;
		Vector3 scale = defaultScale;
		for (float s = 0f; s < this.scaleTime; s = Time.realtimeSinceStartup - t0)
		{
			float t = Mathf.Clamp01(s / this.scaleTime);
			float scl = Mathf.Lerp(startScale, endScale, t);
			scale = defaultScale * scl;
			base.transform.localScale = scale;
			yield return 0;
		}
		base.transform.localScale = defaultScale * endScale;
		yield break;
	}

	private IEnumerator LocalWaitForSeconds(float seconds)
	{
		float t0 = Time.realtimeSinceStartup;
		for (float s = 0f; s < seconds; s = Time.realtimeSinceStartup - t0)
		{
			yield return 0;
		}
		yield break;
	}

	private IEnumerator coHandleButtonPress(int fingerId)
	{
		this.buttonDown = true;
		bool buttonPressed = true;
		Vector3 defaultScale = base.transform.localScale;
		if (this.targetScale != 1f)
		{
			yield return base.StartCoroutine(this.coScale(defaultScale, 1f, this.targetScale));
		}
		this.PlaySound(this.buttonDownSound);
		if (this.buttonDownSpriteId != -1)
		{
			this.sprite.spriteId = this.buttonDownSpriteId;
		}
		if (this.targetObject && !this.ActionDown.Equals(string.Empty))
		{
			this.targetObject.SendMessage(this.ActionDown);
		}
		if (this.ButtonDownEvent != null)
		{
			this.ButtonDownEvent(this);
		}
		for (;;)
		{
			Vector3 cursorPosition = Vector3.zero;
			bool cursorActive = true;
			if (fingerId != -1)
			{
				bool flag = false;
				for (int i = 0; i < UnityEngine.Input.touchCount; i++)
				{
					Touch touch = UnityEngine.Input.GetTouch(i);
					if (touch.fingerId == fingerId)
					{
						if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
						{
							break;
						}
						cursorPosition = touch.position;
						flag = true;
					}
				}
				if (!flag)
				{
					cursorActive = false;
				}
			}
			else
			{
				if (!Input.GetMouseButton(0))
				{
					cursorActive = false;
				}
				cursorPosition = UnityEngine.Input.mousePosition;
			}
			if (!cursorActive)
			{
				break;
			}
			Ray ray = this.viewCamera.ScreenPointToRay(cursorPosition);
			RaycastHit hitInfo;
			bool colliderHit = base.GetComponent<Collider>().Raycast(ray, out hitInfo, float.PositiveInfinity);
			if (buttonPressed && !colliderHit)
			{
				if (this.targetScale != 1f)
				{
					yield return base.StartCoroutine(this.coScale(defaultScale, this.targetScale, 1f));
				}
				this.PlaySound(this.buttonUpSound);
				if (this.buttonUpSpriteId != -1)
				{
					this.sprite.spriteId = this.buttonUpSpriteId;
				}
				if (this.ButtonUpEvent != null)
				{
					this.ButtonUpEvent(this);
				}
				buttonPressed = false;
			}
			else if (!buttonPressed && colliderHit)
			{
				if (this.targetScale != 1f)
				{
					yield return base.StartCoroutine(this.coScale(defaultScale, 1f, this.targetScale));
				}
				this.PlaySound(this.buttonDownSound);
				if (this.buttonDownSpriteId != -1)
				{
					this.sprite.spriteId = this.buttonDownSpriteId;
				}
				if (this.ButtonDownEvent != null)
				{
					this.ButtonDownEvent(this);
				}
				buttonPressed = true;
			}
			if (buttonPressed && this.ButtonAutoFireEvent != null)
			{
				this.ButtonAutoFireEvent(this);
			}
			yield return 0;
		}
		if (buttonPressed)
		{
			if (this.targetScale != 1f)
			{
				yield return base.StartCoroutine(this.coScale(defaultScale, this.targetScale, 1f));
			}
			this.PlaySound(this.buttonPressedSound);
			if (this.buttonPressedSpriteId != -1)
			{
				this.sprite.spriteId = this.buttonPressedSpriteId;
			}
			if (this.targetObject)
			{
				if (this.itemNumber == 0)
				{
					if (!this.ActionUp.Equals(string.Empty))
					{
						this.targetObject.SendMessage(this.ActionUp);
					}
				}
				else if (!this.ActionUp.Equals(string.Empty))
				{
					this.targetObject.SendMessage(this.ActionUp, this.itemNumber);
				}
			}
			if (this.ButtonUpEvent != null)
			{
				this.ButtonUpEvent(this);
			}
			if (this.ButtonPressedEvent != null)
			{
				this.ButtonPressedEvent(this);
			}
			if (base.gameObject.activeInHierarchy)
			{
				yield return base.StartCoroutine(this.LocalWaitForSeconds(this.pressedWaitTime));
			}
			if (this.buttonUpSpriteId != -1)
			{
				this.sprite.spriteId = this.buttonUpSpriteId;
			}
		}
		this.buttonDown = false;
		yield break;
	}

	private void Update()
	{
		if (this.buttonDown)
		{
			return;
		}
		bool flag = false;
		if (Input.multiTouchEnabled)
		{
			for (int i = 0; i < UnityEngine.Input.touchCount; i++)
			{
				Touch touch = UnityEngine.Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began)
				{
					Ray ray = this.viewCamera.ScreenPointToRay(touch.position);
					RaycastHit raycastHit;
					if (base.GetComponent<Collider>().Raycast(ray, out raycastHit, 1E+08f) && !Physics.Raycast(ray, raycastHit.distance - 0.01f))
					{
						base.StartCoroutine(this.coHandleButtonPress(touch.fingerId));
						flag = true;
						break;
					}
				}
			}
		}
		if (!flag && Input.GetMouseButtonDown(0))
		{
			Ray ray2 = this.viewCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
			RaycastHit raycastHit2;
			if (base.GetComponent<Collider>().Raycast(ray2, out raycastHit2, 1E+08f) && !Physics.Raycast(ray2, raycastHit2.distance - 0.01f))
			{
				base.StartCoroutine(this.coHandleButtonPress(-1));
			}
		}
	}

	public Camera viewCamera;

	public string buttonDownSprite = "button_down";

	public string buttonUpSprite = "button_up";

	public string buttonPressedSprite = "button_up";

	public int itemNumber;

	private int buttonDownSpriteId = -1;

	private int buttonUpSpriteId = -1;

	private int buttonPressedSpriteId = -1;

	public AudioClip buttonDownSound;

	public AudioClip buttonUpSound;

	public AudioClip buttonPressedSound;

	public GameObject targetObject;

	public string ActionDown = string.Empty;

	public string ActionUp = string.Empty;

	private tk2dBaseSprite sprite;

	private bool buttonDown;

	public float targetScale = 1.1f;

	public float scaleTime = 0.05f;

	public float pressedWaitTime = 0.3f;

	public delegate void ButtonHandlerDelegate(tk2dButton source);
}
