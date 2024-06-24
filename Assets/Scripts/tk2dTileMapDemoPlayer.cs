// dnSpy decompiler from Assembly-CSharp.dll class: tk2dTileMapDemoPlayer
using System;
using UnityEngine;

public class tk2dTileMapDemoPlayer : MonoBehaviour
{
	private bool AllowAddForce
	{
		get
		{
			return this.forceWait < 0f;
		}
	}

	private void Awake()
	{
		this.sprite = base.GetComponent<tk2dSprite>();
		if (this.textMesh == null || this.textMesh.transform.parent != base.transform)
		{
			UnityEngine.Debug.LogError("Text mesh must be assigned and parented to player.");
			base.enabled = false;
		}
		this.textMeshOffset = this.textMesh.transform.position - base.transform.position;
		this.textMesh.transform.parent = null;
		this.textMeshLabel.text = "instructions";
		this.textMeshLabel.Commit();
		bool flag = false;
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer || flag)
		{
			this.textMesh.text = "LEFT ARROW / RIGHT ARROW";
		}
		else
		{
			this.textMesh.text = "TAP LEFT / RIGHT SIDE OF SCREEN";
		}
		this.textMesh.Commit();
		Application.targetFrameRate = 60;
	}

	private void Update()
	{
		this.forceWait -= Time.deltaTime;
		string b = (!this.AllowAddForce) ? "player_disabled" : "player";
		if (this.sprite.CurrentSprite.name != b)
		{
			this.sprite.SetSprite(b);
		}
		if (this.AllowAddForce)
		{
			float num = 0f;
			if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
			{
				num = 1f;
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
			{
				num = -1f;
			}
			for (int i = 0; i < UnityEngine.Input.touchCount; i++)
			{
				if (Input.touches[i].phase == TouchPhase.Began)
				{
					num = Mathf.Sign(Input.touches[i].position.x - (float)Screen.width * 0.5f);
					break;
				}
			}
			if (num != 0f)
			{
				if (!this.textInitialized)
				{
					this.textMeshLabel.text = "score";
					this.textMeshLabel.Commit();
					this.textMesh.text = this.score.ToString();
					this.textMesh.Commit();
					this.textInitialized = true;
				}
				this.moveX = num;
			}
		}
		this.textMesh.transform.position = base.transform.position + this.textMeshOffset;
	}

	private void FixedUpdate()
	{
		if (this.AllowAddForce && this.moveX != 0f)
		{
			this.forceWait = this.addForceLimit;
			Rigidbody component = base.GetComponent<Rigidbody>();
			Rigidbody2D component2 = base.GetComponent<Rigidbody2D>();
			if (component != null)
			{
				component.AddForce(new Vector3(this.moveX * this.amount, this.amount, 0f) * Time.deltaTime, ForceMode.Impulse);
				component.AddTorque(new Vector3(0f, 0f, -this.moveX * this.torque) * Time.deltaTime, ForceMode.Impulse);
			}
			else if (component2 != null)
			{
				component2.AddForce(new Vector2(this.moveX * this.amount, this.amount) * Time.deltaTime * 50f);
				component2.AddTorque(-this.moveX * this.torque * Time.deltaTime * 20f);
			}
			this.moveX = 0f;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		UnityEngine.Object.Destroy(other.gameObject);
		this.score++;
		this.textMesh.text = this.score.ToString();
		this.textMesh.Commit();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		UnityEngine.Object.Destroy(other.gameObject);
		this.score++;
		this.textMesh.text = this.score.ToString();
		this.textMesh.Commit();
	}

	public tk2dTextMesh textMesh;

	public tk2dTextMesh textMeshLabel;

	private Vector3 textMeshOffset;

	private bool textInitialized;

	public float addForceLimit = 1f;

	public float amount = 500f;

	public float torque = 50f;

	private tk2dSprite sprite;

	private int score;

	private float forceWait;

	private float moveX;
}
