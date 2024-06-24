// dnSpy decompiler from Assembly-CSharp.dll class: tk2dDemoCameraController
using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Demo/tk2dDemoCameraController")]
public class tk2dDemoCameraController : MonoBehaviour
{
	private void Start()
	{
		this.listTopPos = this.listItems.localPosition;
		this.listBottomPos = this.listTopPos - this.endOfListItems.localPosition;
	}

	private IEnumerator MoveListTo(Vector3 from, Vector3 to)
	{
		this.transitioning = true;
		float time = 0.5f;
		for (float t = 0f; t < time; t += Time.deltaTime)
		{
			float nt = Mathf.Clamp01(t / time);
			nt = Mathf.SmoothStep(0f, 1f, nt);
			this.listItems.localPosition = Vector3.Lerp(from, to, nt);
			yield return 0;
		}
		this.listItems.localPosition = to;
		this.transitioning = false;
		yield break;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !this.transitioning && !Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition)))
		{
			if (this.listAtTop)
			{
				base.StartCoroutine(this.MoveListTo(this.listTopPos, this.listBottomPos));
			}
			else
			{
				base.StartCoroutine(this.MoveListTo(this.listBottomPos, this.listTopPos));
			}
			this.listAtTop = !this.listAtTop;
		}
		foreach (Transform transform in this.rotatingObjects)
		{
			transform.Rotate(UnityEngine.Random.insideUnitSphere, Time.deltaTime * 360f);
		}
	}

	public Transform listItems;

	public Transform endOfListItems;

	private Vector3 listTopPos = Vector3.zero;

	private Vector3 listBottomPos = Vector3.zero;

	private bool listAtTop = true;

	private bool transitioning;

	public Transform[] rotatingObjects = new Transform[0];
}
