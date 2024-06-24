// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUICamera
using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/Core/tk2dUICamera")]
public class tk2dUICamera : MonoBehaviour
{
	public tk2dUICamera.tk2dRaycastType RaycastType
	{
		get
		{
			return this.raycastType;
		}
	}

	public void AssignRaycastLayerMask(LayerMask mask)
	{
		this.raycastLayerMask = mask;
	}

	public LayerMask FilteredMask
	{
		get
		{
			return this.raycastLayerMask & base.GetComponent<Camera>().cullingMask;
		}
	}

	public Camera HostCamera
	{
		get
		{
			return base.GetComponent<Camera>();
		}
	}

	private void OnEnable()
	{
		if (base.GetComponent<Camera>() == null)
		{
			UnityEngine.Debug.LogError("tk2dUICamera should only be attached to a camera.");
			base.enabled = false;
			return;
		}
		if (!base.GetComponent<Camera>().orthographic && this.raycastType == tk2dUICamera.tk2dRaycastType.Physics2D)
		{
			UnityEngine.Debug.LogError("tk2dUICamera - Physics2D raycast only works with orthographic cameras.");
			base.enabled = false;
			return;
		}
		tk2dUIManager.RegisterCamera(this);
	}

	private void OnDisable()
	{
		tk2dUIManager.UnregisterCamera(this);
	}

	[SerializeField]
	private LayerMask raycastLayerMask = -1;

	[SerializeField]
	private tk2dUICamera.tk2dRaycastType raycastType;

	public enum tk2dRaycastType
	{
		Physics3D,
		Physics2D
	}
}
