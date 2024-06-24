// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUITextInput
using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("2D Toolkit/UI/tk2dUITextInput")]
public class tk2dUITextInput : MonoBehaviour
{
	public tk2dUILayout LayoutItem
	{
		get
		{
			return this.layoutItem;
		}
		set
		{
			if (this.layoutItem != value)
			{
				if (this.layoutItem != null)
				{
					this.layoutItem.OnReshape -= this.LayoutReshaped;
				}
				this.layoutItem = value;
				if (this.layoutItem != null)
				{
					this.layoutItem.OnReshape += this.LayoutReshaped;
				}
			}
		}
	}

	public GameObject SendMessageTarget
	{
		get
		{
			if (this.selectionBtn != null)
			{
				return this.selectionBtn.sendMessageTarget;
			}
			return null;
		}
		set
		{
			if (this.selectionBtn != null && this.selectionBtn.sendMessageTarget != value)
			{
				this.selectionBtn.sendMessageTarget = value;
			}
		}
	}

	public bool IsFocus
	{
		get
		{
			return this.isSelected;
		}
	}

	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			if (this.text != value)
			{
				this.text = value;
				if (this.text.Length > this.maxCharacterLength)
				{
					this.text = this.text.Substring(0, this.maxCharacterLength);
				}
				this.FormatTextForDisplay(this.text);
				if (this.isSelected)
				{
					this.SetCursorPosition();
				}
			}
		}
	}

	private void Awake()
	{
		this.useTouchScreenKeyboard = (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android);
		this.SetState();
		this.ShowDisplayText();
	}

	private void Start()
	{
		this.wasStartedCalled = true;
		if (tk2dUIManager.Instance__NoCreate != null)
		{
			tk2dUIManager.Instance.OnAnyPress += this.AnyPress;
		}
		this.wasOnAnyPressEventAttached = true;
	}

	private void OnEnable()
	{
		if (this.wasStartedCalled && !this.wasOnAnyPressEventAttached && tk2dUIManager.Instance__NoCreate != null)
		{
			tk2dUIManager.Instance.OnAnyPress += this.AnyPress;
		}
		if (this.layoutItem != null)
		{
			this.layoutItem.OnReshape += this.LayoutReshaped;
		}
		this.selectionBtn.OnClick += this.InputSelected;
	}

	private void OnDisable()
	{
		if (tk2dUIManager.Instance__NoCreate != null)
		{
			tk2dUIManager.Instance.OnAnyPress -= this.AnyPress;
			if (!this.useTouchScreenKeyboard && this.listenForKeyboardText)
			{
				tk2dUIManager.Instance.OnInputUpdate -= this.ListenForKeyboardTextUpdate;
			}
		}
		this.wasOnAnyPressEventAttached = false;
		this.selectionBtn.OnClick -= this.InputSelected;
		this.listenForKeyboardText = false;
		if (this.layoutItem != null)
		{
			this.layoutItem.OnReshape -= this.LayoutReshaped;
		}
	}

	public void SetFocus()
	{
		this.SetFocus(true);
	}

	public void SetFocus(bool focus)
	{
		if (!this.IsFocus && focus)
		{
			this.InputSelected();
		}
		else if (this.IsFocus && !focus)
		{
			this.InputDeselected();
		}
	}

	private void FormatTextForDisplay(string modifiedText)
	{
		if (this.isPasswordField)
		{
			int length = modifiedText.Length;
			char paddingChar = (this.passwordChar.Length <= 0) ? '*' : this.passwordChar[0];
			modifiedText = string.Empty;
			modifiedText = modifiedText.PadRight(length, paddingChar);
		}
		this.inputLabel.text = modifiedText;
		this.inputLabel.Commit();
		for (float num = this.inputLabel.GetComponent<Renderer>().bounds.size.x / this.inputLabel.transform.lossyScale.x; num > this.fieldLength; num = this.inputLabel.GetComponent<Renderer>().bounds.size.x / this.inputLabel.transform.lossyScale.x)
		{
			modifiedText = modifiedText.Substring(1, modifiedText.Length - 1);
			this.inputLabel.text = modifiedText;
			this.inputLabel.Commit();
		}
		if (modifiedText.Length == 0 && !this.listenForKeyboardText)
		{
			this.ShowDisplayText();
		}
		else
		{
			this.HideDisplayText();
		}
	}

	private void ListenForKeyboardTextUpdate()
	{
		bool flag = false;
		string arg = this.text;
		foreach (char c in Input.inputString)
		{
			if (c == "\b"[0])
			{
				if (this.text.Length != 0)
				{
					arg = this.text.Substring(0, this.text.Length - 1);
					flag = true;
				}
			}
			else if (c != "\n"[0] && c != "\r"[0])
			{
				if (c != '\t' && c != '\u001b')
				{
					arg += c;
					flag = true;
				}
			}
		}
		string inputString = this.keyboard.text;
		if (!inputString.Equals(this.text))
		{
			arg = inputString;
			flag = true;
		}
		if (flag)
		{
			this.Text = arg;
			this.NotifyTextChange();
		}
	}

	private void NotifyTextChange()
	{
		if (this.OnTextChange != null)
		{
			this.OnTextChange(this);
		}
		if (this.SendMessageTarget != null && this.SendMessageOnTextChangeMethodName.Length > 0)
		{
			this.SendMessageTarget.SendMessage(this.SendMessageOnTextChangeMethodName, this, SendMessageOptions.RequireReceiver);
		}
	}

	private void InputSelected()
	{
		if (this.text.Length == 0)
		{
			this.HideDisplayText();
		}
		this.isSelected = true;
		if (!this.useTouchScreenKeyboard && !this.listenForKeyboardText)
		{
			tk2dUIManager.Instance.OnInputUpdate += this.ListenForKeyboardTextUpdate;
		}
		this.listenForKeyboardText = true;
		this.SetState();
		this.SetCursorPosition();
		if (Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.OSXEditor)
		{
			TouchScreenKeyboard.hideInput = false;
			this.keyboard = TouchScreenKeyboard.Open(this.text, TouchScreenKeyboardType.Default, false, false, this.isPasswordField, false);
			base.StartCoroutine(this.TouchScreenKeyboardLoop());
		}
	}

	private IEnumerator TouchScreenKeyboardLoop()
	{
		while (this.keyboard != null && !this.keyboard.done && this.keyboard.active)
		{
			bool needChange = this.Text != this.keyboard.text;
			if (needChange)
			{
				this.Text = this.keyboard.text;
				this.NotifyTextChange();
			}
			yield return null;
		}
		if (this.keyboard != null)
		{
			bool flag = this.Text != this.keyboard.text;
			if (flag)
			{
				this.Text = this.keyboard.text;
				this.NotifyTextChange();
			}
		}
		if (this.isSelected)
		{
			this.InputDeselected();
		}
		yield break;
	}

	private void InputDeselected()
	{
		if (this.text.Length == 0)
		{
			this.ShowDisplayText();
		}
		this.isSelected = false;
		if (!this.useTouchScreenKeyboard && this.listenForKeyboardText)
		{
			tk2dUIManager.Instance.OnInputUpdate -= this.ListenForKeyboardTextUpdate;
		}
		this.listenForKeyboardText = false;
		this.SetState();
		if (this.keyboard != null && !this.keyboard.done)
		{
			this.keyboard.active = false;
		}
		this.keyboard = null;
	}

	private void AnyPress()
	{
		if (this.isSelected && tk2dUIManager.Instance.PressedUIItem != this.selectionBtn)
		{
			this.InputDeselected();
		}
	}

	private void SetState()
	{
		tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.unSelectedStateGO, !this.isSelected);
		tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.selectedStateGO, this.isSelected);
		tk2dUIBaseItemControl.ChangeGameObjectActiveState(this.cursor, this.isSelected);
	}

	private void SetCursorPosition()
	{
		float num = 1f;
		float num2 = 0.002f;
		if (this.inputLabel.anchor == TextAnchor.MiddleLeft || this.inputLabel.anchor == TextAnchor.LowerLeft || this.inputLabel.anchor == TextAnchor.UpperLeft)
		{
			num = 2f;
		}
		else if (this.inputLabel.anchor == TextAnchor.MiddleRight || this.inputLabel.anchor == TextAnchor.LowerRight || this.inputLabel.anchor == TextAnchor.UpperRight)
		{
			num = -2f;
			num2 = 0.012f;
		}
		if (this.text.EndsWith(" "))
		{
			tk2dFontChar tk2dFontChar;
			if (this.inputLabel.font.inst.useDictionary)
			{
				tk2dFontChar = this.inputLabel.font.inst.charDict[32];
			}
			else
			{
				tk2dFontChar = this.inputLabel.font.inst.chars[32];
			}
			num2 += tk2dFontChar.advance * this.inputLabel.scale.x / 2f;
		}
		float num3 = this.inputLabel.GetComponent<Renderer>().bounds.extents.x / base.gameObject.transform.lossyScale.x;
		this.cursor.transform.localPosition = new Vector3(this.inputLabel.transform.localPosition.x + (num3 + num2) * num, this.cursor.transform.localPosition.y, this.cursor.transform.localPosition.z);
	}

	private void ShowDisplayText()
	{
		if (!this.isDisplayTextShown)
		{
			this.isDisplayTextShown = true;
			if (this.emptyDisplayLabel != null)
			{
				this.emptyDisplayLabel.text = this.emptyDisplayText;
				this.emptyDisplayLabel.Commit();
				tk2dUIBaseItemControl.ChangeGameObjectActiveState(this.emptyDisplayLabel.gameObject, true);
			}
			tk2dUIBaseItemControl.ChangeGameObjectActiveState(this.inputLabel.gameObject, false);
		}
	}

	private void HideDisplayText()
	{
		if (this.isDisplayTextShown)
		{
			this.isDisplayTextShown = false;
			tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(this.emptyDisplayLabel.gameObject, false);
			tk2dUIBaseItemControl.ChangeGameObjectActiveState(this.inputLabel.gameObject, true);
		}
	}

	private void LayoutReshaped(Vector3 dMin, Vector3 dMax)
	{
		this.fieldLength += dMax.x - dMin.x;
		string text = this.text;
		this.text = string.Empty;
		this.Text = text;
	}

	public tk2dUIItem selectionBtn;

	public tk2dTextMesh inputLabel;

	public tk2dTextMesh emptyDisplayLabel;

	public GameObject unSelectedStateGO;

	public GameObject selectedStateGO;

	public GameObject cursor;

	public float fieldLength = 1f;

	public int maxCharacterLength = 30;

	public string emptyDisplayText;

	public bool isPasswordField;

	public string passwordChar = "*";

	[SerializeField]
	[HideInInspector]
	private tk2dUILayout layoutItem;

	private bool isSelected;

	private bool wasStartedCalled;

	private bool wasOnAnyPressEventAttached;

	private TouchScreenKeyboard keyboard;

	private bool useTouchScreenKeyboard;

	private bool listenForKeyboardText;

	private bool isDisplayTextShown;

	public Action<tk2dUITextInput> OnTextChange;

	public string SendMessageOnTextChangeMethodName = string.Empty;

	private string text = string.Empty;
}
