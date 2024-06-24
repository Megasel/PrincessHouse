// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIManager
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/Core/tk2dUIManager")]
public class tk2dUIManager : MonoBehaviour
{
	public static tk2dUIManager Instance
	{
		get
		{
			if (tk2dUIManager.instance == null)
			{
				tk2dUIManager.instance = (UnityEngine.Object.FindObjectOfType(typeof(tk2dUIManager)) as tk2dUIManager);
				if (tk2dUIManager.instance == null)
				{
					GameObject gameObject = new GameObject("tk2dUIManager");
					tk2dUIManager.instance = gameObject.AddComponent<tk2dUIManager>();
				}
			}
			return tk2dUIManager.instance;
		}
	}

	public static tk2dUIManager Instance__NoCreate
	{
		get
		{
			return tk2dUIManager.instance;
		}
	}

	public Camera UICamera
	{
		get
		{
			return this.uiCamera;
		}
		set
		{
			this.uiCamera = value;
		}
	}

	public Camera GetUICameraForControl(GameObject go)
	{
		int num = 1 << go.layer;
		int count = tk2dUIManager.allCameras.Count;
		for (int i = 0; i < count; i++)
		{
			tk2dUICamera tk2dUICamera = tk2dUIManager.allCameras[i];
			if ((tk2dUICamera.FilteredMask & num) != 0)
			{
				return tk2dUICamera.HostCamera;
			}
		}
		UnityEngine.Debug.LogError("Unable to find UI camera for " + go.name);
		return null;
	}

	public static void RegisterCamera(tk2dUICamera cam)
	{
		tk2dUIManager.allCameras.Add(cam);
	}

	public static void UnregisterCamera(tk2dUICamera cam)
	{
		tk2dUIManager.allCameras.Remove(cam);
	}

	public bool InputEnabled
	{
		get
		{
			return this.inputEnabled;
		}
		set
		{
			if (this.inputEnabled && !value)
			{
				this.SortCameras();
				this.inputEnabled = value;
				if (this.useMultiTouch)
				{
					this.CheckMultiTouchInputs();
				}
				else
				{
					this.CheckInputs();
				}
			}
			else
			{
				this.inputEnabled = value;
			}
		}
	}

	public tk2dUIItem PressedUIItem
	{
		get
		{
			if (!this.useMultiTouch)
			{
				return this.pressedUIItem;
			}
			if (this.pressedUIItems.Length > 0)
			{
				return this.pressedUIItems[this.pressedUIItems.Length - 1];
			}
			return null;
		}
	}

	public tk2dUIItem[] PressedUIItems
	{
		get
		{
			return this.pressedUIItems;
		}
	}

	public bool UseMultiTouch
	{
		get
		{
			return this.useMultiTouch;
		}
		set
		{
			if (this.useMultiTouch != value && this.inputEnabled)
			{
				this.InputEnabled = false;
				this.useMultiTouch = value;
				this.InputEnabled = true;
			}
			else
			{
				this.useMultiTouch = value;
			}
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnAnyPress;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnInputUpdate;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<float> OnScrollWheelChange;

	private void SortCameras()
	{
		this.sortedCameras.Clear();
		int count = tk2dUIManager.allCameras.Count;
		for (int i = 0; i < count; i++)
		{
			tk2dUICamera tk2dUICamera = tk2dUIManager.allCameras[i];
			if (tk2dUICamera != null)
			{
				this.sortedCameras.Add(tk2dUICamera);
			}
		}
		this.sortedCameras.Sort((tk2dUICamera a, tk2dUICamera b) => b.GetComponent<Camera>().depth.CompareTo(a.GetComponent<Camera>().depth));
	}

	private void Awake()
	{
		if (tk2dUIManager.instance == null)
		{
			tk2dUIManager.instance = this;
			if (tk2dUIManager.instance.transform.childCount != 0)
			{
				UnityEngine.Debug.LogError("You should not attach anything to the tk2dUIManager object. The tk2dUIManager will not get destroyed between scene switches and any children will persist as well.");
			}
			if (Application.isPlaying)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}
		else if (tk2dUIManager.instance != this)
		{
			UnityEngine.Debug.Log("Discarding unnecessary tk2dUIManager instance.");
			if (this.uiCamera != null)
			{
				this.HookUpLegacyCamera(this.uiCamera);
				this.uiCamera = null;
			}
			UnityEngine.Object.Destroy(this);
			return;
		}
		tk2dUITime.Init();
		this.Setup();
	}

	private void HookUpLegacyCamera(Camera cam)
	{
		if (cam.GetComponent<tk2dUICamera>() == null)
		{
			tk2dUICamera tk2dUICamera = cam.gameObject.AddComponent<tk2dUICamera>();
			tk2dUICamera.AssignRaycastLayerMask(this.raycastLayerMask);
		}
	}

	private void Start()
	{
		if (this.uiCamera != null)
		{
			UnityEngine.Debug.Log("It is no longer necessary to hook up a camera to the tk2dUIManager. You can simply attach a tk2dUICamera script to the cameras that interact with UI.");
			this.HookUpLegacyCamera(this.uiCamera);
			this.uiCamera = null;
		}
		if (tk2dUIManager.allCameras.Count == 0)
		{
			UnityEngine.Debug.LogError("Unable to find any tk2dUICameras, and no cameras are connected to the tk2dUIManager. You will not be able to interact with the UI.");
		}
	}

	private void Setup()
	{
		if (!this.areHoverEventsTracked)
		{
			this.checkForHovers = false;
		}
	}

	private void Update()
	{
		tk2dUITime.Update();
		if (this.inputEnabled)
		{
			this.SortCameras();
			if (this.useMultiTouch)
			{
				this.CheckMultiTouchInputs();
			}
			else
			{
				this.CheckInputs();
			}
			if (this.OnInputUpdate != null)
			{
				this.OnInputUpdate();
			}
			if (this.OnScrollWheelChange != null)
			{
				float axis = UnityEngine.Input.GetAxis("Mouse ScrollWheel");
				if (axis != 0f)
				{
					this.OnScrollWheelChange(axis);
				}
			}
		}
	}

	private void CheckInputs()
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		this.primaryTouch = default(tk2dUITouch);
		this.secondaryTouch = default(tk2dUITouch);
		this.resultTouch = default(tk2dUITouch);
		this.hitUIItem = null;
		if (this.inputEnabled)
		{
			int touchCount = UnityEngine.Input.touchCount;
			if (UnityEngine.Input.touchCount > 0)
			{
				for (int i = 0; i < touchCount; i++)
				{
					Touch touch = UnityEngine.Input.GetTouch(i);
					if (touch.phase == TouchPhase.Began)
					{
						this.primaryTouch = new tk2dUITouch(touch);
						flag = true;
						flag3 = true;
					}
					else if (this.pressedUIItem != null && touch.fingerId == this.firstPressedUIItemTouch.fingerId)
					{
						this.secondaryTouch = new tk2dUITouch(touch);
						flag2 = true;
					}
				}
				this.checkForHovers = false;
			}
			else if (Input.GetMouseButtonDown(0))
			{
				this.primaryTouch = new tk2dUITouch(TouchPhase.Began, 9999, UnityEngine.Input.mousePosition, Vector2.zero, 0f);
				flag = true;
				flag3 = true;
			}
			else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
			{
				Vector2 vector = Vector2.zero;
				TouchPhase phase = TouchPhase.Moved;
				if (this.pressedUIItem != null)
				{
					vector = this.firstPressedUIItemTouch.position - new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
				}
				if (Input.GetMouseButtonUp(0))
				{
					phase = TouchPhase.Ended;
				}
				else if (vector == Vector2.zero)
				{
					phase = TouchPhase.Stationary;
				}
				this.secondaryTouch = new tk2dUITouch(phase, 9999, UnityEngine.Input.mousePosition, vector, tk2dUITime.deltaTime);
				flag2 = true;
			}
		}
		if (flag)
		{
			this.resultTouch = this.primaryTouch;
		}
		else if (flag2)
		{
			this.resultTouch = this.secondaryTouch;
		}
		if (flag || flag2)
		{
			this.hitUIItem = this.RaycastForUIItem(this.resultTouch.position);
			if (this.resultTouch.phase == TouchPhase.Began)
			{
				if (this.pressedUIItem != null)
				{
					this.pressedUIItem.CurrentOverUIItem(this.hitUIItem);
					if (this.pressedUIItem != this.hitUIItem)
					{
						this.pressedUIItem.Release();
						this.pressedUIItem = null;
					}
					else
					{
						this.firstPressedUIItemTouch = this.resultTouch;
					}
				}
				if (this.hitUIItem != null)
				{
					this.hitUIItem.Press(this.resultTouch);
				}
				this.pressedUIItem = this.hitUIItem;
				this.firstPressedUIItemTouch = this.resultTouch;
			}
			else if (this.resultTouch.phase == TouchPhase.Ended)
			{
				if (this.pressedUIItem != null)
				{
					this.pressedUIItem.CurrentOverUIItem(this.hitUIItem);
					this.pressedUIItem.UpdateTouch(this.resultTouch);
					this.pressedUIItem.Release();
					this.pressedUIItem = null;
				}
			}
			else if (this.pressedUIItem != null)
			{
				this.pressedUIItem.CurrentOverUIItem(this.hitUIItem);
				this.pressedUIItem.UpdateTouch(this.resultTouch);
			}
		}
		else if (this.pressedUIItem != null)
		{
			this.pressedUIItem.CurrentOverUIItem(null);
			this.pressedUIItem.Release();
			this.pressedUIItem = null;
		}
		if (this.checkForHovers)
		{
			if (this.inputEnabled)
			{
				if (!flag && !flag2 && this.hitUIItem == null && !Input.GetMouseButton(0))
				{
					this.hitUIItem = this.RaycastForUIItem(UnityEngine.Input.mousePosition);
				}
				else if (Input.GetMouseButton(0))
				{
					this.hitUIItem = null;
				}
			}
			if (this.hitUIItem != null)
			{
				if (this.hitUIItem.isHoverEnabled)
				{
					if (!this.hitUIItem.HoverOver(this.overUIItem) && this.overUIItem != null)
					{
						this.overUIItem.HoverOut(this.hitUIItem);
					}
					this.overUIItem = this.hitUIItem;
				}
				else if (this.overUIItem != null)
				{
					this.overUIItem.HoverOut(null);
				}
			}
			else if (this.overUIItem != null)
			{
				this.overUIItem.HoverOut(null);
			}
		}
		if (flag3 && this.OnAnyPress != null)
		{
			this.OnAnyPress();
		}
	}

	private void CheckMultiTouchInputs()
	{
		bool flag = false;
		this.touchCounter = 0;
		if (this.inputEnabled)
		{
			if (UnityEngine.Input.touchCount > 0)
			{
				foreach (Touch touch in Input.touches)
				{
					if (this.touchCounter >= 5)
					{
						break;
					}
					this.allTouches[this.touchCounter] = new tk2dUITouch(touch);
					this.touchCounter++;
				}
			}
			else if (Input.GetMouseButtonDown(0))
			{
				this.allTouches[this.touchCounter] = new tk2dUITouch(TouchPhase.Began, 9999, UnityEngine.Input.mousePosition, Vector2.zero, 0f);
				this.mouseDownFirstPos = UnityEngine.Input.mousePosition;
				this.touchCounter++;
			}
			else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
			{
				Vector2 vector = this.mouseDownFirstPos - new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
				TouchPhase phase = TouchPhase.Moved;
				if (Input.GetMouseButtonUp(0))
				{
					phase = TouchPhase.Ended;
				}
				else if (vector == Vector2.zero)
				{
					phase = TouchPhase.Stationary;
				}
				this.allTouches[this.touchCounter] = new tk2dUITouch(phase, 9999, UnityEngine.Input.mousePosition, vector, tk2dUITime.deltaTime);
				this.touchCounter++;
			}
		}
		for (int j = 0; j < this.touchCounter; j++)
		{
			this.pressedUIItems[j] = this.RaycastForUIItem(this.allTouches[j].position);
		}
		for (int k = 0; k < this.prevPressedUIItemList.Count; k++)
		{
			this.prevPressedItem = this.prevPressedUIItemList[k];
			if (this.prevPressedItem != null)
			{
				int fingerId = this.prevPressedItem.Touch.fingerId;
				bool flag2 = false;
				for (int l = 0; l < this.touchCounter; l++)
				{
					this.currTouch = this.allTouches[l];
					if (this.currTouch.fingerId == fingerId)
					{
						flag2 = true;
						this.currPressedItem = this.pressedUIItems[l];
						if (this.currTouch.phase == TouchPhase.Began)
						{
							this.prevPressedItem.CurrentOverUIItem(this.currPressedItem);
							if (this.prevPressedItem != this.currPressedItem)
							{
								this.prevPressedItem.Release();
								this.prevPressedUIItemList.RemoveAt(k);
								k--;
							}
						}
						else if (this.currTouch.phase == TouchPhase.Ended)
						{
							this.prevPressedItem.CurrentOverUIItem(this.currPressedItem);
							this.prevPressedItem.UpdateTouch(this.currTouch);
							this.prevPressedItem.Release();
							this.prevPressedUIItemList.RemoveAt(k);
							k--;
						}
						else
						{
							this.prevPressedItem.CurrentOverUIItem(this.currPressedItem);
							this.prevPressedItem.UpdateTouch(this.currTouch);
						}
						break;
					}
				}
				if (!flag2)
				{
					this.prevPressedItem.CurrentOverUIItem(null);
					this.prevPressedItem.Release();
					this.prevPressedUIItemList.RemoveAt(k);
					k--;
				}
			}
		}
		for (int m = 0; m < this.touchCounter; m++)
		{
			this.currPressedItem = this.pressedUIItems[m];
			this.currTouch = this.allTouches[m];
			if (this.currTouch.phase == TouchPhase.Began)
			{
				if (this.currPressedItem != null)
				{
					bool flag3 = this.currPressedItem.Press(this.currTouch);
					if (flag3)
					{
						this.prevPressedUIItemList.Add(this.currPressedItem);
					}
				}
				flag = true;
			}
		}
		if (flag && this.OnAnyPress != null)
		{
			this.OnAnyPress();
		}
	}

	private tk2dUIItem RaycastForUIItem(Vector2 screenPos)
	{
		int count = this.sortedCameras.Count;
		for (int i = 0; i < count; i++)
		{
			tk2dUICamera tk2dUICamera = this.sortedCameras[i];
			if (tk2dUICamera.RaycastType == tk2dUICamera.tk2dRaycastType.Physics3D)
			{
				this.ray = tk2dUICamera.HostCamera.ScreenPointToRay(screenPos);
				if (Physics.Raycast(this.ray, out this.hit, tk2dUICamera.HostCamera.farClipPlane - tk2dUICamera.HostCamera.nearClipPlane, tk2dUICamera.FilteredMask))
				{
					return this.hit.collider.GetComponent<tk2dUIItem>();
				}
			}
			else if (tk2dUICamera.RaycastType == tk2dUICamera.tk2dRaycastType.Physics2D)
			{
				Collider2D collider2D = Physics2D.OverlapPoint(tk2dUICamera.HostCamera.ScreenToWorldPoint(screenPos), tk2dUICamera.FilteredMask);
				if (collider2D != null)
				{
					return collider2D.GetComponent<tk2dUIItem>();
				}
			}
		}
		return null;
	}

	public void OverrideClearAllChildrenPresses(tk2dUIItem item)
	{
		if (this.useMultiTouch)
		{
			for (int i = 0; i < this.pressedUIItems.Length; i++)
			{
				tk2dUIItem tk2dUIItem = this.pressedUIItems[i];
				if (tk2dUIItem != null && item.CheckIsUIItemChildOfMe(tk2dUIItem))
				{
					tk2dUIItem.CurrentOverUIItem(item);
				}
			}
		}
		else if (this.pressedUIItem != null && item.CheckIsUIItemChildOfMe(this.pressedUIItem))
		{
			this.pressedUIItem.CurrentOverUIItem(item);
		}
	}

	public static double version = 1.0;

	public static int releaseId = 0;

	private static tk2dUIManager instance;

	[SerializeField]
	private Camera uiCamera;

	private static List<tk2dUICamera> allCameras = new List<tk2dUICamera>();

	private List<tk2dUICamera> sortedCameras = new List<tk2dUICamera>();

	public LayerMask raycastLayerMask = -1;

	private bool inputEnabled = true;

	public bool areHoverEventsTracked = true;

	private tk2dUIItem pressedUIItem;

	private tk2dUIItem overUIItem;

	private tk2dUITouch firstPressedUIItemTouch;

	private bool checkForHovers = true;

	[SerializeField]
	private bool useMultiTouch;

	private const int MAX_MULTI_TOUCH_COUNT = 5;

	private tk2dUITouch[] allTouches = new tk2dUITouch[5];

	private List<tk2dUIItem> prevPressedUIItemList = new List<tk2dUIItem>();

	private tk2dUIItem[] pressedUIItems = new tk2dUIItem[5];

	private int touchCounter;

	private Vector2 mouseDownFirstPos = Vector2.zero;

	private const string MOUSE_WHEEL_AXES_NAME = "Mouse ScrollWheel";

	private tk2dUITouch primaryTouch = default(tk2dUITouch);

	private tk2dUITouch secondaryTouch = default(tk2dUITouch);

	private tk2dUITouch resultTouch = default(tk2dUITouch);

	private tk2dUIItem hitUIItem;

	private RaycastHit hit;

	private Ray ray;

	private tk2dUITouch currTouch;

	private tk2dUIItem currPressedItem;

	private tk2dUIItem prevPressedItem;
}
